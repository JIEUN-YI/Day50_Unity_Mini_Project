using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] float width; // ����� ���� x��

    // ������Ʈ�� ���α��̸� ����
    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;

    }

    // Ư�� ���ΰ� �̻� �̵��ϸ� ���� ������Ʈ ��Ȱ��ȭ
    private void Update()
    {
        if (transform.position.x < -width)
        {
            gameObject.SetActive(false);
        }
    }
}
