using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGem : MonoBehaviour
{
    [SerializeField] float speed; // �������� �̵� �ӵ�

    PlayerController playerController; // PlayerController.cs�� �����ϱ�
    [SerializeField] float playerHp;

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        playerHp = playerController.playerHp;
        if(playerHp > 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        }
        else if(playerHp <= 0)
        {
            return;
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
