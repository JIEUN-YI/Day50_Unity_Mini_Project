using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // 배경의 이동 속도

    private void Update()
    {
        if (GameManager.instance.isGameover == true) // 게임 종료 중에는 이동하지 않음
        {
            return;
        }
        // 일정한 속도로 배경을 왼쪽으로 이동
        if (GameManager.instance.isGameover == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

}
