using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // ����� �̵� �ӵ�
    [SerializeField] private float playerHp;

    PlayerController playerController; // PlayerController.cs�� �����ϱ�
    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        playerHp = playerHp = playerController.playerHp;
        // ������ �ӵ��� ����� �������� �̵�
        if (playerHp > 0)
        {
        transform.Translate(Vector2.left * speed *Time.deltaTime);
        }
        if (playerHp <= 0)
        {
            return;
        }
    }

}
