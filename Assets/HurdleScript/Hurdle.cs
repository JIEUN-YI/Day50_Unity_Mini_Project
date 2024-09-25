using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ֹ��� �⺻ �̵��� ����
public class Hurdle : MonoBehaviour
{
    [SerializeField] float speed; // ��� ��ֹ��� �̵� �ӵ�

    private void Update()
    {
    // 2D �����̹Ƿ� Vector2.left �������� �ӵ���ŭ �̵�
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
    }

    // �ӵ��� ��ֹ��� �ӵ��� ����
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
