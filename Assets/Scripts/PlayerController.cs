using System;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slideButton;

    public float jumpPower = 5f;
    public bool isJumping = false;

    public Image[] ecoRunImages; // UI Image �迭
    public Sprite[] filledSprites; // ä���� ��������Ʈ �迭

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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        // Dictionary �ʱ�ȭ
        InitializeDictionaries();
    }

    private void Start()
    {
        index = 0;
        jumpButton.onClick.AddListener(Jump);
    }

    private void InitializeDictionaries()
    {
        // �̹����� ��������Ʈ �迭�� ���̰� ��ġ�ϴ��� Ȯ��
        if (ecoRunImages.Length != filledSprites.Length)
        {
            Debug.LogError("ecoRunImages and filledSprites arrays must be of the same length.");
            return;
        }

        // Dictionary�� ������ �߰�
        for (int i = 0; i < ecoRunImages.Length; i++)
        {
            string key = ecoRunImages[i].name; // �̹��� �̸��� Ű�� ���
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
            heartManager.DestroyHeart(index++);
        }
        else if (other.gameObject.CompareTag("Bonus"))
        {
            Debug.Log("Collided with: " + other.gameObject.name);

            // ��ü �̸����� "(Clone)"�� ����
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
    public void GamePause()
    {
        gameOverUI.SetActive(true);
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

        Time.timeScale = 0f; // ���� �Ͻ�����
    }
}
