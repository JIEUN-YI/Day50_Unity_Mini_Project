using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb; // Rigidbody 제어
    [SerializeField] Animator animator; // Animation 제어
    [SerializeField] SpriteRenderer spriteRenderer; // SpriteRenderer 제어
    [SerializeField] AudioSource gemSound; // Gem sound 제어
    [SerializeField] AudioSource ItemSound; // Gem sound 제어

    [SerializeField] public float playerHp;
    private float maxPlayerHp;
    [SerializeField] float jumpPower;
    [SerializeField] int jumpCount = 0;
    [SerializeField] float damage;
    [SerializeField] public float score;

    private void Awake()
    {
        maxPlayerHp = playerHp;
    }
    private void Update()
    {
        animator.SetBool("isStart", false); // 게임시작 전 기본 자세

        if (GameManager.instance.isGameover == false) // 게임 시작 중 - 플레이어의 움직임
        {
            playerHp -= Time.deltaTime;
            score += Time.deltaTime;

            animator.SetBool("isGameover", false);
            animator.SetBool("isStart", true);
            animator.SetFloat("isJump", rb.velocity.y);

            SetScore();

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
            if (playerHp < 0)
            {
                GameManager.instance.isGameover = true;
                animator.SetBool("isStart", false);
                animator.SetBool("isGameover", true);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gem": // Gem과 충돌 시, Player의 점수 증가
                gemSound.Play();
                score += 100;
                collision.gameObject.SetActive(false);
                break;
            case "Pattern": // 장애물과 충돌 시, 체력 감소
                playerHp -= damage;
                StartCoroutine(PlayerFlash()); // 깜빡이는 코루틴 실행
                break;
            case "Item_Hp": // Item_Hp와 충돌 시, Player의 체력 증가
                playerHp += maxPlayerHp * 0.3f;
                ItemSound.Play();
                collision.gameObject.SetActive(false);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "UnderGround": // 바닥과 충돌 시, 2단 점프 카운트 리셋
                animator.SetBool("isRun", true);
                jumpCount = 0;
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "UnderGround":
                animator.SetBool("isRun", false);
                break;
        }
    }

    /// <summary>
    /// 플레이어의 점프 구현
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        animator.SetFloat("isJump", rb.velocity.y);
    }

    /// <summary>
    /// 플레이어의 슬라이딩 구현
    /// </summary>
    private void Sliding()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, -4.03f);
        animator.SetBool("isSliding", true);
    }

    /// <summary>
    /// 플레이어의 기본 상태로 되돌리기 구현
    /// </summary>
    private void Standing()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, -3.96f);
        animator.SetBool("isSliding", false);
    }

    /// <summary>
    /// 플레이어 점수에 따른 속도 증가
    /// </summary>
    private void SetScore()
    {
        if (score > 35000)
        {
            Time.timeScale = 2f;
        }
        else if (score > 20000)
        {
            Time.timeScale = 1.8f;
        }
        else if (score > 15000)
        {
            Time.timeScale = 1.5f;
        }
        else if (score > 8000)
        {
            Time.timeScale = 1.2f;
        }
    }

    /// <summary>
    /// 플레이어의 깜빡임을 구현하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerFlash()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
