using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGem : MonoBehaviour
{
    [SerializeField] float speed; // �������� �̵� �ӵ�

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

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
