using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] public bool checkHurdle; // 장애물과 겹침 여부 확인 - true : 중복 / false : 미중복
    [SerializeField] public bool checkGem; // Gem과 겹침 여부 확인 - true : 중복 / false : 미중복

     private void OnCollisionStay2D(Collision2D collision)
     {
         switch (collision.gameObject.tag)
         {
             case "Hurdle":
                 checkHurdle = true;
                break;
            case "Gem":
                checkGem = true;
                break;
         }
     }
     private void OnCollisionExit2D(Collision2D collision)
     {
         switch (collision.gameObject.tag)
         {
             case "Hurdle":
                checkHurdle = false;
                 break;
            case "Gem":
                checkGem = false;
                break;
        }
     }
}
