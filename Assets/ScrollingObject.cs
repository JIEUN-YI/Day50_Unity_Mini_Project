using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] public float speed; // ����� �̵� �ӵ�

    private void Update()
    {
        // ������ �ӵ��� ����� �������� �̵�
        transform.Translate(Vector2.left * speed *Time.deltaTime);
    }

}
