using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controller)")]
    [SerializeField] Rigidbody2D rb; // Rigidbody ����
    [SerializeField] Animator animator; // Animation ����
    [SerializeField] SpriteRenderer spriteRenderer; // SpriteRenderer ����
    [SerializeField] AudioSource gemSound; // Gem sound ����
    [SerializeField] AudioSource ItemSound; // Gem sound ����
    [SerializeField] GameObject invinciblityBarrier;

    [Header("Status")]
    [SerializeField] public float playerHp;
    private float maxPlayerHp;
    private float jumpPower = 8;
    private int jumpCount = 0;
    [SerializeField] private int clashCount = 0; // ���� ��ֹ��� ���� �浹���� �ʵ��� ī���� - ��ֹ����� ����� ī��Ʈ 0
    private float damage = 10;
    [SerializeField] public float score;
    private float hpReduceSpeed; // ü�� ���� �ӵ�

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
        animator.SetBool("isStart", false); // ���ӽ��� �� �⺻ �ڼ�

        if (GameManager.instance.isGameover == false) // ���� ���� �� - �÷��̾��� ������
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
    /// �÷��̾���� trigger �浹�� Ȯ��
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Gem": // Gem�� �浹 ��, Player�� ���� ����
                gemSound.Play();
                score += 100;
                collision.gameObject.SetActive(false);
                break;
            case "Hurdle": // ��ֹ��� �浹 ��, ü�� ����
                if (clashCount <= 0)
                {
                    playerHp -= damage; // �� ������Ʈ�� 2�� �̻� ���� �浹 x
                    clashCount++;
                }
                StartCoroutine(PlayerFlash()); // �����̴� �ڷ�ƾ ����
                break;
            case "Item_Hp": // Item_Hp�� �浹 ��, Player�� ü�� ����
                playerHp += maxPlayerHp * 0.3f;
                playerHp = Mathf.Min(playerHp, maxPlayerHp); // �ִ�ü���� �Ѿ�� �ʵ���
                ItemSound.Play();
                collision.gameObject.SetActive(false);
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Hurdle": // ��ֹ��� �浹���� ��������
                clashCount = 0; // ī��Ʈ �ʱ�ȭ
                break;
        }
    }

    /// <summary>
    /// �÷��̾���� �浹�� Ȯ��
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "UnderGround": // �ٴڰ� �浹 ��, 2�� ���� ī��Ʈ ����
                animator.SetBool("isRun", true);
                jumpCount = 0;
                break;
            case "DeadZone": // �߶��Ͽ� DeadZone�� �浹 ��, �÷��̾� ü�� 0
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
    /// �÷��̾���� �浹���� �������������� Ȯ��
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
    /// �÷��̾��� �⺻ �ִϸ��̼� ����
    /// </summary>
    private void PlayerSetAnimation()
    {
        animator.SetBool("isGameover", false);
        animator.SetBool("isStart", true);
        animator.SetFloat("isJump", rb.velocity.y);
    }

    /// <summary>
    /// �÷��̾��� ���� ����
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
    /// �÷��̾��� �����̵� ����
    /// </summary>
    private void Sliding()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, -4.05f);
        animator.SetBool("isSliding", true);
    }

    /// <summary>
    /// �÷��̾��� �⺻ ���·� �ǵ����� ����
    /// </summary>
    private void Standing()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, -3.96f);
        animator.SetBool("isSliding", false);
    }

    /// <summary>
    /// �÷��̾� ��� �Լ�
    /// </summary>
    private void PlayerDied()
    {
        GameManager.instance.isGameover = true;
        animator.SetBool("isStart", false);
        animator.SetBool("isGameover", true);
    }

    /// <summary>
    /// �÷��̾� ������ ���� 
    /// �ӵ� ����
    /// ü�� ������ ����
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
    /// �÷��̾��� �浹 �� �������� �����ϴ� �ڷ�ƾ
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
    /// �ٴ� �߶� �� ���� �ö�Դٰ� �ٽ� �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerFloatUp()
    {
        gameObject.transform.Translate(0, 5, 0, Space.World);
        yield return new WaitForSeconds(3f);
    }
    /// <summary>
    /// �÷��̾��� ���� ���� �ڷ�ƾ
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
