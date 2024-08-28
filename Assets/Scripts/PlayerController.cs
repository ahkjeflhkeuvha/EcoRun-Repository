using System;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button slideButton;

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
}
