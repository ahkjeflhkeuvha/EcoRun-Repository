using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Button jumpButton; // ���� ��ư�� ���� ���� �߰�

    public float jumpPower = 5f; // ���� ��
    public bool isJumping = false;

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
}
