using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object Pool���� ������ Hurdle�� ���� ��ȯ�� ����
public class HurdleObj : MonoBehaviour
{
    public HurdlePool hurdlePool; // HurdlePool.cs ����

    // �� ��ֹ� ������ ���� Object Pool�� ����
    [SerializeField] public List<HurdleObj> SmallJumpHurdlePool;
    [SerializeField] public List<HurdleObj> BigJumpHurdlePool;
    [SerializeField] public List<HurdleObj> TopHurdlePool;

    [Header("HurdleController.cs")]
    List<HurdleObj> returnPool; // ��ȯ�� ����Ʈ
    HurdleObj returnObj; // ��ȯ�� ��ֹ�


    // ��ü Ȱ��ȭ�� HerdleController���� ������ HurdleObj�� list�� �����ͼ� ����
    private void OnEnable()
    {
        returnObj = GameObject.FindGameObjectWithTag("HurdleController").GetComponent<HurdleController>().nowMakedHurdle;
        returnPool = GameObject.FindGameObjectWithTag("HurdleController").GetComponent<HurdleController>().nowMakedHurdlePool;
    }
    // ������ Hurdle��ü�� Ground�� �浹���� �ʴ� ��� ȸ�� �Լ� ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            ReturnHurdle(returnObj, returnPool);
        }
    }

    // ȸ�� �Լ� �ۼ�
    public void ReturnHurdle(HurdleObj returnHurdleObj, List<HurdleObj> returnHurdlePool)
    {
        hurdlePool.ReturnHurdlePool(returnHurdleObj, returnHurdlePool);
    }
}
