using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장애물의 기본 이동을 지정
public class Hurdle : MonoBehaviour
{
    [SerializeField] float speed; // 모든 장애물의 이동 속도
    [SerializeField] float playerHp; 
    PlayerController playerController; // PlayerController.cs를 연동하기

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

    // 속도를 장애물의 속도로 설정
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
