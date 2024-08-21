using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�

public class PlayerManager : MonoBehaviour
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private Button jumpButton; // ���� ��ư�� ���� ���� �߰�

        public float jumpPower = 5f; // ���� ��
        public bool isJumping = false;
        public float slideDuration = 1f; // �����̵� �ð�

        public Sprite jumpSprite; // ������ �� ����� ��������Ʈ
        public Sprite slideSprite; // �����̵��� �� ����� ��������Ʈ
        public Sprite defaultSprite; // �⺻ ��������Ʈ

        // public HeartManager heartManager;
        // public int heartIndex = 0;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            // ���� ��ư�� onClick �̺�Ʈ ����
            jumpButton.onClick.AddListener(Jump);
        }

        public void Jump()
        {
            if (!isJumping)
            {
                isJumping = true;
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                Debug.Log("jumping " + isJumping);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // �ٴڿ� ����� �� isJumping�� false�� ����
            if (other.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
                Debug.Log("Landed");
            }
        }
        /*public void Slide()
        {
            Debug.Log("sliding");
            isSliding = true;
            spriteRenderer.sprite = slideSprite; // �����̵� ��������Ʈ�� ����

            // �����̵尡 ������ �⺻ ��������Ʈ�� �ǵ����� Coroutine ����
            if (resetSpriteCoroutine != null)
            {
                StopCoroutine(resetSpriteCoroutine);
            }
            resetSpriteCoroutine = StartCoroutine(ResetSpriteAfterDelay(slideDuration));
        }*/

        /*IEnumerator ResetSpriteAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            spriteRenderer.sprite = defaultSprite;
            isSliding = false; // �����̵� ���� ����
        }*/

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Coin"))
            {
                Debug.Log("Coin");
                Destroy(other.gameObject); // ���� ����
            }
            else if (other.CompareTag("Enemy") || other.CompareTag("Attack"))
            {
                Debug.Log("Enemy");
                Destroy(other.gameObject);
            }

        }
    }
}


