using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장애물의 기본 이동을 지정
public class Hurdle : MonoBehaviour
{
    [SerializeField] float speed; // 모든 장애물의 이동 속도


    private void Update()
    {

        if(GameManager.instance.isGameover == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
        }
        else if(GameManager.instance.isGameover == true)
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
