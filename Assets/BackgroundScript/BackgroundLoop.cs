using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] float width; // ����� ���� x��

    // ������Ʈ�� ���α��̸� ����
    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;

    }

    // Ư�� ���ΰ� �̻� �̵��ϸ� ��ġ�� ���ġ
    private void Update()
    {
        if (transform.position.x < -width)
        {
            Reposition();
        }
    }

    /// <summary>
    /// ��ġ�� ���ġ�ϴ� �Լ�
    /// </summary>
    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 1.99f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
