using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controller)")]
    [SerializeField] Rigidbody2D rb; // Rigidbody 제어
    [SerializeField] Animator animator; // Animation 제어
    [SerializeField] SpriteRenderer spriteRenderer; // SpriteRenderer 제어
    [SerializeField] AudioSource gemSound; // Gem sound 제어
    [SerializeField] AudioSource ItemSound; // Gem sound 제어
    [SerializeField] GameObject invinciblityBarrier;

    [Header("Status")]
    [SerializeField] public float playerHp;
    private float maxPlayerHp;
    private float jumpPower = 8;
    private int jumpCount = 0;
    [SerializeField] private int clashCount = 0; // 같은 장애물에 동시 충돌하지 않도록 카운팅 - 장애물에서 벗어나면 카운트 0
    private float damage = 10;
    [SerializeField] public float score;
    private float hpReduceSpeed; // 체력 감소 속도

    [Header("Status")]
    Coroutine PlayerFlashR;
    Coroutine PlayerFloatUpR;
    Coroutine InvinciblityR;

    private void Awake()
    {
        maxPlayerHp = playerHp;
        hpReduceSpeed = 1;
    }
    private void Update()
    {
        animator.SetBool("isStart", false); // 게임시작 전 기본 자세

        if (GameManager.instance.isGameover == false) // 게임 시작 중 - 플레이어의 움직임
        {
            playerHp -= hpReduceSpeed * Time.deltaTime;
            score += Time.deltaTime;
            PlayerSetAnimation();
            SetScore();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckJumpCount();
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
                PlayerDied();
            }

        }
    }

    /// <summary>
    /// 플레이어와의 trigger 충돌을 확인
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gem": // Gem과 충돌 시, Player의 점수 증가
                gemSound.Play();
                score += 100;
                collision.gameObject.SetActive(false);
                break;
            case "Hurdle": // 장애물과 충돌 시, 체력 감소
                if (clashCount <= 0)
                {
                    playerHp -= damage; // 한 오브젝트와 2번 이상 동시 충돌 x
                    clashCount++;
                }
                StartCoroutine(PlayerFlash()); // 깜빡이는 코루틴 실행
                break;
            case "Item_Hp": // Item_Hp와 충돌 시, Player의 체력 증가
                playerHp += maxPlayerHp * 0.3f;
                playerHp = Mathf.Min(playerHp, maxPlayerHp); // 최대체력을 넘어서지 않도록
                ItemSound.Play();
                collision.gameObject.SetActive(false);
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Hurdle": // 장애물과 충돌에서 빠져나와
                clashCount = 0; // 카운트 초기화
                break;
        }
    }

    /// <summary>
    /// 플레이어와의 충돌을 확인
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "UnderGround": // 바닥과 충돌 시, 2단 점프 카운트 리셋
                animator.SetBool("isRun", true);
                jumpCount = 0;
                break;
            case "DeadZone": // 추락하여 DeadZone에 충돌 시, 플레이어 체력 0
                // playerHp = 0;
                playerHp -= maxPlayerHp * 0.3f;
                PlayerFloatUpR = StartCoroutine(PlayerFloatUp());
                PlayerFlashR = StartCoroutine(PlayerFlash());
                InvinciblityR = StartCoroutine(Invinciblity());

                if (playerHp <= 0)
                {
                    StopCoroutine(PlayerFloatUpR);
                    StopCoroutine(PlayerFlashR);
                    StopCoroutine(InvinciblityR); 
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, -4);
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                    invinciblityBarrier.SetActive(false);
                }
                break;
        }
    }

    /// <summary>
    /// 플레이어와의 충돌에서 빠져나오는지를 확인
    /// </summary>
    /// <param name="collision"></param>
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
    /// 플레이어의 기본 애니메이션 설정
    /// </summary>
    private void PlayerSetAnimation()
    {
        animator.SetBool("isGameover", false);
        animator.SetBool("isStart", true);
        animator.SetFloat("isJump", rb.velocity.y);
    }

    /// <summary>
    /// 플레이어의 점프 구현
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        animator.SetFloat("isJump", rb.velocity.y);
    }

    private void CheckJumpCount()
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

    /// <summary>
    /// 플레이어의 슬라이딩 구현
    /// </summary>
    private void Sliding()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, -4.05f);
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
    /// 플레이어 사망 함수
    /// </summary>
    private void PlayerDied()
    {
        GameManager.instance.isGameover = true;
        animator.SetBool("isStart", false);
        animator.SetBool("isGameover", true);
    }

    /// <summary>
    /// 플레이어 점수에 따른 
    /// 속도 증가
    /// 체력 감소폭 증가
    /// </summary>
    private void SetScore()
    {
        if (score > 5000)
        {
            animator.SetFloat("DeadSpeed", 0.2f);
            Time.timeScale = 2f;
            hpReduceSpeed = 2f;
        }
        else if (score > 4000)
        {
            animator.SetFloat("DeadSpeed", 0.4f);
            Time.timeScale = 1.8f;
            hpReduceSpeed = 1.8f;
        }
        else if (score > 3000)
        {
            animator.SetFloat("DeadSpeed", 0.6f);
            Time.timeScale = 1.5f;
            hpReduceSpeed = 1.4f;
        }
        else if (score > 2000)
        {
            animator.SetFloat("DeadSpeed", 0.8f);
            Time.timeScale = 1.2f;
            hpReduceSpeed = 1.2f;
        }
    }
    /// <summary>
    /// 플레이어의 충돌 시 깜빡임을 구현하는 코루틴
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
    /// <summary>
    /// 바닥 추락 시 위로 올라왔다가 다시 진행하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerFloatUp()
    {
        gameObject.transform.Translate(0, 5, 0, Space.World);
        yield return new WaitForSeconds(3f);
    }
    /// <summary>
    /// 플레이어의 무적 상태 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Invinciblity()
    {
        this.gameObject.layer = 9;
        invinciblityBarrier.SetActive(true);
        yield return new WaitForSeconds(5f);
        this.gameObject.layer = 3;
        invinciblityBarrier.SetActive(false);
    }
}
