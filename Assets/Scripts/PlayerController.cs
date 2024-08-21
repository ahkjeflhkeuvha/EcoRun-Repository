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
            Debug.Log("jump");
            isJumping = true;
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            Debug.Log("jumping " + isJumping);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject);
        // �ٴڿ� ����� �� isJumping�� false�� ����
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            Debug.Log("Landed");
        }
        
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin");
            Destroy(other.gameObject); // ���� ����
        }
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Attack"))
        {
            Debug.Log("Enemy");
            Destroy(other.gameObject);
        }
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