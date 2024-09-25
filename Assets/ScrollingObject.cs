using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // 배경의 이동 속도

    private void Update()
    {
        // 일정한 속도로 배경을 왼쪽으로 이동
        transform.Translate(Vector2.left * speed *Time.deltaTime);
    }

}
