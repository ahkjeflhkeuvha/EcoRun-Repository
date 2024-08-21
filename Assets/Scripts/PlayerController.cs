using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Button jumpButton; // 점프 버튼을 위한 변수 추가

    public float jumpPower = 5f; // 점프 힘
    public bool isJumping = false;

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
}
