using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // 배경의 이동 속도
    [SerializeField] private float playerHp;

    private void Update()
    {
        playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerHp;
        // 일정한 속도로 배경을 왼쪽으로 이동
        if(playerHp > 0)
        {
        transform.Translate(Vector2.left * speed *Time.deltaTime);
        }
        if (playerHp <= 0)
        {
            Debug.Log(" 배경 정지 ");
            return;
        }
    }

}
