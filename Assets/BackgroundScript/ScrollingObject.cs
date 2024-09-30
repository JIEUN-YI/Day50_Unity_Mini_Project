using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.instance.isGameover == true) // ���� ���� �߿��� �̵����� ����
        {
            return;
        }
        // ������ �ӵ��� ����� �������� �̵�
        if (GameManager.instance.isGameover == false)
        {
            transform.Translate(Vector2.left * GameManager.instance.speed * Time.deltaTime);
        }
    }

}
