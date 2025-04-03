using UnityEngine;
using System.Collections;

public class PlayerUIViewer : MonoBehaviour
{
    Rigidbody2D rb; // Rigidbody 제어
    Animator animator; // Animation 제어
    SpriteRenderer spriteRenderer; // SpriteRenderer 제어

    [SerializeField] AudioSource gemSound; // Gem sound 제어
    [SerializeField] AudioSource itemSound; // Gem sound 제어
    [SerializeField] GameObject invinciblityBarrier;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver == true)
        {
            animator.SetBool("isStart", false); // 게임시작 전 기본 자세
        }
        else
        {
            PlayerSetAnimation(rb.velocity.y);
        }
        
    }

    /// <summary>
    /// 플레이어의 기본 애니메이션 설정
    /// </summary>
    private void PlayerSetAnimation(float velocity)
    {
        animator.SetBool("isGameover", false);
        animator.SetBool("isStart", true);
        animator.SetFloat("isJump", velocity);
    }

    public void JumpAnimation(float velocity)
    {
        animator.SetFloat("isJump", velocity);
    }

    public void SlidingAnimation(bool isTrue)
    {
        animator.SetBool("isSliding", isTrue);
    }

    public void RunAnimation(bool isTrue)
    {
        animator.SetBool("isRun", isTrue);
    }

    public void DiedAnimation()
    {
        animator.SetBool("isStart", false);
        animator.SetBool("isGameover", true);
    }

    public void GemSoundPlay()
    {
        gemSound.Play();
    }

    public void ItemSoundPlay()
    {
        itemSound.Play();
    }

    public void SetinvinciblityBarrier(bool isTrue)
    {
        invinciblityBarrier.SetActive(isTrue);
    }

    public void SetSpriteColor(int color1, int color2, int color3, int color4)
    {
        spriteRenderer.color = new Color(color1, color2, color3, color4);
    }
    /// <summary>
    /// 플레이어 점수에 따른 
    /// 애니메이션 속도 변경
    /// </summary>
    public void SetSpeed(float speed)
    {
        animator.SetFloat("DeadSpeed", speed);
    }

    /// <summary>
    /// 플레이어의 충돌 시 깜빡임을 구현하는 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayerFlash()
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
    public IEnumerator PlayerFloatUp()
    {
        gameObject.transform.Translate(0, 5, 0, Space.World);
        yield return new WaitForSeconds(3f);
    }
    /// <summary>
    /// 플레이어의 무적 상태 코루틴
    /// </summary>
    /// <returns></returns>
    public IEnumerator Invinciblity()
    {
        this.gameObject.layer = 9;
        invinciblityBarrier.SetActive(true);
        yield return new WaitForSeconds(5f);
        this.gameObject.layer = 3;
        invinciblityBarrier.SetActive(false);
    }
}
