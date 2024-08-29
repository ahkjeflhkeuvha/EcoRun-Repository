using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slideButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button retryButton;

    [SerializeField] private Button GameovercancelButton;
    [SerializeField] private Button GameoverretryButton;
    [SerializeField] private Button GameoverhomeButton;

    public float jumpPower = 5f;
    public bool isJumping = false;

    public Image[] ecoRunImages; // UI Image 배열
    public Sprite[] filledSprites; // 채워진 스프라이트 배열

    private Dictionary<string, Image> ecoRunDict = new Dictionary<string, Image>();
    private Dictionary<string, Sprite> filledSpriteDict = new Dictionary<string, Sprite>();

    public HeartManager heartManager;
    public int index;

    public Sprite attackButtonImg;
    public Sprite jumpButtonImg;
    public Sprite slideButtonImg;

    public GameObject gameOverUI;
    public TextMeshProUGUI scoreText;
    public GameObject[] stars;
    public int score = 0;
    public int gauge = 0;
    public int scoreCount = 0;

    public Sprite BigEmptyStar;
    public Sprite SmallEmptyStar;

    public Button pauseButton;
    public Sprite stopSprite;
    public Sprite playSprite;
    public GameObject pausePanel;
    public GameObject gameoverPanel;

    private bool isPaused = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        // Dictionary 초기화
        InitializeDictionaries();
    }

    private void Start()
    {
        index = 0;
        jumpButton.onClick.AddListener(Jump);
        homeButton.onClick.AddListener(ReturnHome); // 홈 버튼에 리스너 추가
        continueButton.onClick.AddListener(ContinueGame);
        retryButton.onClick.AddListener(RetryGame);
        // GameovercancelButton.onClick.AddListener(ReturnStage);
        GameoverhomeButton.onClick.AddListener(ReturnHome);
        GameoverretryButton.onClick.AddListener(RetryGame);

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // 초기에는 패널을 숨김
        }

        if (gameoverPanel != null)
        {
            gameoverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        scoreText.text = string.Format("{0:D} M", scoreCount);
    }

    private void InitializeDictionaries()
    {
        // 이미지와 스프라이트 배열의 길이가 일치하는지 확인
        if (ecoRunImages.Length != filledSprites.Length)
        {
            Debug.LogError("ecoRunImages and filledSprites arrays must be of the same length.");
            return;
        }

        // Dictionary에 데이터 추가
        for (int i = 0; i < ecoRunImages.Length; i++)
        {
            string key = ecoRunImages[i].name; // 이미지 이름을 키로 사용
            ecoRunDict[key] = ecoRunImages[i];
            filledSpriteDict[key] = filledSprites[i];
        }
    }

    public void Jump()
    {
        if (!isJumping)
        {
            Debug.Log("Jump");
            isJumping = true;
            slideButton.GetComponent<Image>().sprite = attackButtonImg;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            slideButton.GetComponent<Image>().sprite = slideButtonImg;
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin");
            scoreCount++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Attack"))
        {
            Debug.Log("Enemy");
            Destroy(other.gameObject);
            if (index == 3) // index가 3이 되면 마지막 하트 지우고 게임 오버
            {
                heartManager.DestroyHeart(index);
                GameOver();
            }
            else
            {
                heartManager.DestroyHeart(index++);
            }
        }
        else if (other.gameObject.CompareTag("Bonus"))
        {
            Debug.Log("Collided with: " + other.gameObject.name);

            // 객체 이름에서 "(Clone)"을 제거
            string itemName = other.gameObject.name.Replace("(Clone)", "");
            Debug.Log(itemName);

            if (ecoRunDict.ContainsKey(itemName) && filledSpriteDict.ContainsKey(itemName))
            {
                ecoRunDict[itemName].sprite = filledSpriteDict[itemName];
            }
            else
            {
                Debug.LogWarning("Unhandled item: " + itemName);
            }

            Destroy(other.gameObject);
        }
    }

    public void GameStart()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // 게임 일시정지
        // scoreText.text = GaugeManager.scoreCount + "m";

        Debug.Log("endGame start");
        if (index > 1)
        {
            SpriteRenderer sr = stars[0].GetComponent<SpriteRenderer>();
            sr.sprite = BigEmptyStar;
        }
        if (score < 100)
        {
            SpriteRenderer sr = stars[1].GetComponent<SpriteRenderer>();
            sr.sprite = SmallEmptyStar;
        }
        if (scoreCount < 100)
        {
            SpriteRenderer sr = stars[2].GetComponent<SpriteRenderer>();
            sr.sprite = SmallEmptyStar;
        }


    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        if (pauseButton != null)
        {
            pauseButton.GetComponent<Image>().sprite = playSprite;
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        Time.timeScale = 0f; // 게임 일시정지
        Debug.Log("Game Paused");
    }

    private void ResumeGame()
    {
        if (pauseButton != null)
        {
            pauseButton.GetComponent<Image>().sprite = stopSprite;
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Time.timeScale = 1f; // 게임 재개
        Debug.Log("Game Resumed");
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.GetComponent<Image>().sprite = stopSprite;
    }

    private void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 재로드
        Time.timeScale = 1f;
    }

    private void ReturnHome()
    {
        SceneManager.LoadScene("MainScene"); // MainScene으로 이동
    }
}
