using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb; // Rigidbody ����
    [SerializeField] Animator animator; // Animation ����
    [SerializeField] SpriteRenderer spriteRenderer; // SpriteRenderer ����
    [SerializeField] AudioSource gemSound; // Gem sound ����
    [SerializeField] AudioSource ItemSound; // Gem sound ����

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
         animator.SetBool("isStart", false); // ���ӽ��� �� �⺻ �ڼ�

         if (GameManager.instance.isGameover == false) // ���� ���� �� - �÷��̾��� ������
         {
             playerHp -= Time.deltaTime;
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
             case "Pattern": // ��ֹ��� �浹 ��, ü�� ����
                 playerHp -= damage;
                 StartCoroutine(PlayerFlash()); // �����̴� �ڷ�ƾ ����
                 break;
             case "Item_Hp": // Item_Hp�� �浹 ��, Player�� ü�� ����
                 playerHp += maxPlayerHp * 0.3f;
                 ItemSound.Play();
                 collision.gameObject.SetActive(false);
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
     /// �÷��̾� ������ ���� �ӵ� ����
     /// </summary>
      private void SetScore()
      {
          if (score > 10000)
          {
            animator.SetFloat("DeadSpeed", 0.2f);
              Time.timeScale = 2f;
          }
          else if (score > 8000)
          {
            animator.SetFloat("DeadSpeed", 0.4f);
            Time.timeScale = 1.8f;
          }
          else if (score > 5000)
          {
            animator.SetFloat("DeadSpeed", 0.6f);
            Time.timeScale = 1.5f;
          }
          else if (score > 2000)
          {
            animator.SetFloat("DeadSpeed", 0.8f);
            Time.timeScale = 1.2f;
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
    
}
