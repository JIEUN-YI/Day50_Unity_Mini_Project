using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] public bool checkHurdle; // ��ֹ��� ��ħ ���� Ȯ�� - true : �ߺ� / false : ���ߺ�
    [SerializeField] public bool checkGem; // Gem�� ��ħ ���� Ȯ�� - true : �ߺ� / false : ���ߺ�

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
