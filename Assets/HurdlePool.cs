using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hurdle���� ObjectPool�� �����ϴ� ���
public class HurdlePool : MonoBehaviour
{
    // ��ֹ��� ������ �������� �� 10���� ������ HurdleObj Ÿ���� ����Ʈ�� ����
    [SerializeField] public List<HurdleObj> hurdlePools = new List<HurdleObj>();
    private float size = 10;
    [SerializeField] HurdleObj prefab; // ������ ������

    [Header("HurdleController.cs")]
    [SerializeField] Transform makePoint; // ���� ��ġ

    // ������ �����ϸ� list�� ��ȸ�ϸ鼭 ���������� �ν��Ͻ��� ������
    private void Awake()
    {
        for(int i = 0; i<size; i++)
        {
            HurdleObj hurdle = Instantiate(prefab);
            hurdle.gameObject.SetActive(false); // ���� �ν��Ͻ� ��Ȱ��ȭ
            hurdle.transform.parent = transform;
            hurdlePools.Add(hurdle); // ����Ʈ�� �ν��Ͻ� ����
        }
        makePoint = GameObject.FindGameObjectWithTag("HurdleController").GetComponent<HurdleController>().makePoint; // HurdleController�� makePoint �ҷ�����
    }

    public HurdleObj MakeHurdlePool(Vector2 position, List<HurdleObj> hurdlePool)
    {
    // ����Ʈ�� �뷮�� 0 �̻��϶� ����Ʈ�� ������ ��ֹ��� ���
        if(hurdlePool.Count > 0)
        {
            HurdleObj makedHurdle = hurdlePool[hurdlePool.Count - 1];
            makedHurdle.transform.position = position;
            makedHurdle.transform.parent = null;
            makedHurdle.hurdlePool = this;
            return makedHurdle;
        }
    // ����Ʈ�� �뷮�� 0 ������ ��� �ν��Ͻ� �����Ͽ� ��ȯ - ������ ����Ʈ���� ����� �� �ֵ��� ����Ʈ�� �뷮�� ������ ��
        else
        {
            return Instantiate(prefab);
        }
    }

    // ����Ʈ�� ��ȯ �Լ�
    public void ReturnHurdlePool(HurdleObj hurdle, List<HurdleObj> returnPool)
    {
        hurdle.gameObject.SetActive (false);
        hurdle.transform.parent = makePoint;
        returnPool.Add(hurdle);
    }

}
