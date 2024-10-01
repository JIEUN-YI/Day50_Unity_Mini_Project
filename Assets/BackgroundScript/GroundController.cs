using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] float width; // 배경의 가로 x값

    // 오브젝트의 가로길이를 측정
    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;

    }

    // 특정 가로값 이상 이동하면 현재 오브젝트 비활성화
    private void Update()
    {
        if (transform.position.x < -width)
        {
            gameObject.SetActive(false);
        }
    }
}
