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
            case "UnderGround": // 바닥과 충돌 시, 2단 점프 카운트 리셋
                jumpCount = 0;
                break;
            case "Hurdle": // 장애물과 충돌 시, 체력 감소
                playerHp -= damage;
                break;
        }
    }

    /// <summary>
    /// 플레이어의 점프 구현
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 플레이어의 슬라이딩 구현
    /// </summary>
    private void Sliding()
    {
        // 게임오브젝트 90도 회전
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    /// <summary>
    /// 플레이어의 기본 상태로 되돌리기 구현
    /// </summary>
    private void Standing()
    {
        // 게임오브젝트 원상 복귀
        transform.position = new Vector2(-4, -2.3f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// 플레이어가 물체와 충돌했을 때
    /// </summary>
    /// <param name="collision"></param>
}
