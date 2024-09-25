using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerHp;

    [SerializeField] float jumpPower;
    [SerializeField] int jumpCount = 0;

    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
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
            Debug.Log("Sliding");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Standing();
            Debug.Log("Standing");
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void Sliding()
    {
        // 게임오브젝트 90도 회전
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void Standing()
    {
        // 게임오브젝트 원상 복귀
        transform.position = new Vector2(-4, -2.3f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "UnderGround":
                jumpCount = 0;
                break;
            case "Hurdle":
                playerHp -= 3;
                break;
        }
    }
}
