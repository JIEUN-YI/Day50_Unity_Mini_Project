using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // 배경의 이동 속도

    private void Update()
    {
        // 일정한 속도로 배경을 왼쪽으로 이동
        if (GameManager.instance.isGameover == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

}
