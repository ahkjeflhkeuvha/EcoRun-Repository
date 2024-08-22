using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Button jumpButton;

    public float jumpPower = 5f;
    public bool isJumping = false;

    public Image[] EcoRunObjects; // UI Image 배열
    public Sprite[] FilledEcoRun; // 채워진 스프라이트 배열

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpButton.onClick.AddListener(Jump);
        Debug.Log(EcoRunObjects.Length);
    }

    public void Jump()
    {
        if (!isJumping)
        {
            Debug.Log("Jump");
            isJumping = true;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            Debug.Log("Jumping " + isJumping);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin");
            Destroy(other.gameObject); // 코인 제거
        }
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Attack"))
        {
            Debug.Log("Enemy");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bonus"))
        {
            Debug.Log("Collided with: " + other.gameObject.name);

            // 객체 이름에서 "(Clone)"을 제거하고 비교합니다.
            string itemName = other.gameObject.name.Replace("(Clone)", "");
            Debug.Log(itemName);

            switch (itemName)
            {
                case "E":
                    EcoRunObjects[0].sprite = FilledEcoRun[0];
                    break;
                case "C":
                    EcoRunObjects[1].sprite = FilledEcoRun[1];
                    break;
                case "O":
                    EcoRunObjects[2].sprite = FilledEcoRun[2];
                    break;
                case "R":
                    EcoRunObjects[3].sprite = FilledEcoRun[3];
                    break;
                case "U":
                    EcoRunObjects[4].sprite = FilledEcoRun[4];
                    break;
                case "N":
                    EcoRunObjects[5].sprite = FilledEcoRun[5];
                    break;
                default:
                    Debug.LogWarning("Unhandled item: " + itemName);
                    break;
            }

            Destroy(other.gameObject);
        }
    }
}
