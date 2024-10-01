using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] float width; // 배경의 가로 x값

    // 오브젝트의 가로길이를 측정
    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;

    }

    // 특정 가로값 이상 이동하면 위치를 재배치
    private void Update()
    {
        if (transform.position.x < -width)
        {
            Reposition();
        }
    }

    /// <summary>
    /// 위치를 재배치하는 함수
    /// </summary>
    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 1.99f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
