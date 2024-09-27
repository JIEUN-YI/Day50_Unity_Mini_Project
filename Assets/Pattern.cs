using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] float patternSpeed; // ������ �̵��ӵ�

    private void Update()
    {
        // ���� ���� ��
        if(GameManager.instance.isGameover == false)
        {
            transform.Translate(Vector2.left * patternSpeed * Time.deltaTime, Space.World);
        }
        // ���� ���� ��
        else if(GameManager.instance.isGameover == true)
        {
            return;
        }
    }

    // �ӵ� ����
    public void SetSpeed(float speed)
    {
        this.patternSpeed = speed;
    }
}
