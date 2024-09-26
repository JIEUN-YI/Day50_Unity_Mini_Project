using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // ����� �̵� �ӵ�
    [SerializeField] private float playerHp;

    private void Update()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerHp;
        // ������ �ӵ��� ����� �������� �̵�
        if(playerHp > 0)
        {
        transform.Translate(Vector2.left * speed *Time.deltaTime);
        }
        if (playerHp <= 0)
        {
            Debug.Log(" ��� ���� ");
            return;
        }
    }

}
