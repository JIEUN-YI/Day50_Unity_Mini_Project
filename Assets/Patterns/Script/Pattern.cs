using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] float patternSpeed; // 패턴의 이동속도

    private void Update()
    {
        // 게임 진행 시
        if(GameManager.instance.isGameover == false)
        {
            transform.Translate(Vector2.left * patternSpeed * Time.deltaTime, Space.World);
        }
        // 게임 종료 시
        else if(GameManager.instance.isGameover == true)
        {
            return;
        }
    }

    // 속도 설정
    public void SetSpeed(float speed)
    {
        this.patternSpeed = speed;
    }
}
