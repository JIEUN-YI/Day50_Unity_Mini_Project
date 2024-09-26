using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerHp;

    [SerializeField] float playSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] int jumpCount = 0;
    [SerializeField] float damage;

    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        playerHp -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpCount < 2)
                {
                    jumpCount++;
                    Jump();
                }
                else if (jumpCount >= 2)
                {
                    return;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sliding();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Standing();
            }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "UnderGround": // �ٴڰ� �浹 ��, 2�� ���� ī��Ʈ ����
                jumpCount = 0;
                break;
            case "Hurdle": // ��ֹ��� �浹 ��, ü�� ����
                playerHp -= damage;
                break;
        }
    }

    /// <summary>
    /// �÷��̾��� ���� ����
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    /// <summary>
    /// �÷��̾��� �����̵� ����
    /// </summary>
    private void Sliding()
    {
        // ���ӿ�����Ʈ 90�� ȸ��
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    /// <summary>
    /// �÷��̾��� �⺻ ���·� �ǵ����� ����
    /// </summary>
    private void Standing()
    {
        // ���ӿ�����Ʈ ���� ����
        transform.position = new Vector2(-4, -2.3f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// �÷��̾ ��ü�� �浹���� ��
    /// </summary>
    /// <param name="collision"></param>
}
