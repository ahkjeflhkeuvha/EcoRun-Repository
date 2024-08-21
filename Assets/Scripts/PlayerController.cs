using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI 관련 네임스페이스 추가

public class PlayerManager : MonoBehaviour
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private Button jumpButton; // 점프 버튼을 위한 변수 추가

        public float jumpPower = 5f; // 점프 힘
        public bool isJumping = false;
        public float slideDuration = 1f; // 슬라이드 시간

        public Sprite jumpSprite; // 점프할 때 사용할 스프라이트
        public Sprite slideSprite; // 슬라이드할 때 사용할 스프라이트
        public Sprite defaultSprite; // 기본 스프라이트

        // public HeartManager heartManager;
        // public int heartIndex = 0;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            // 점프 버튼에 onClick 이벤트 연결
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
            // 바닥에 닿았을 때 isJumping을 false로 설정
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
            spriteRenderer.sprite = slideSprite; // 슬라이드 스프라이트로 변경

            // 슬라이드가 끝나면 기본 스프라이트로 되돌리는 Coroutine 시작
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
            isSliding = false; // 슬라이드 상태 종료
        }*/

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Coin"))
            {
                Debug.Log("Coin");
                Destroy(other.gameObject); // 코인 제거
            }
            else if (other.CompareTag("Enemy") || other.CompareTag("Attack"))
            {
                Debug.Log("Enemy");
                Destroy(other.gameObject);
            }

        }
    }
}


