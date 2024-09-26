using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGem : MonoBehaviour
{
    [SerializeField] float speed; // 아이템의 이동 속도

    PlayerController playerController; // PlayerController.cs를 연동하기
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
