using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ֹ��� �⺻ �̵��� ����
public class Hurdle : MonoBehaviour
{
    [SerializeField] float speed; // ��� ��ֹ��� �̵� �ӵ�
    [SerializeField] float playerHp;

    private void Update()
    {
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerHp;
        // 2D �����̹Ƿ� Vector2.left �������� �ӵ���ŭ �̵�
        if(playerHp > 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        }
        if(playerHp <= 0)
        {
            return;
        }
    }

    // �ӵ��� ��ֹ��� �ӵ��� ����
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
