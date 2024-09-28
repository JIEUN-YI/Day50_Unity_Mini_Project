using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // ����� �̵� �ӵ�

    private void Update()
    {
        // ������ �ӵ��� ����� �������� �̵�
        if (GameManager.instance.isGameover == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

}
