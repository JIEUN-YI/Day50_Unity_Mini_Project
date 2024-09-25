using System.Collections.Generic;
using UnityEngine;

// ObjectPool ������ ����Ͽ� �������� ��ֹ��� �����ϰ� ��ȯ�ϵ��� ����
public class HurdleController : MonoBehaviour
{
    private HurdlePool hurdlePool = new HurdlePool(); // HurdlePoop.cs ����
    float HurdleSpeed = 3;

    [Header("HurdlePool.cs")]
    // ��ֹ��� �����Ǵ� ��ġ
    [SerializeField] public Transform makePoint;
    [Header("HurdleObj.cs")]
    public HurdleObj nowMakedHurdle; // ���� ������ ��ֹ�
    public List<HurdleObj> nowMakedHurdlePool; // ���� ������ ��ֹ��� ������Ʈ Ǯ

    [Header("Pool List")]
    [SerializeField] public List<HurdleObj> SmallJumpHurdlePool;
    [SerializeField] public List<HurdleObj> BigJumpHurdlePool;
    [SerializeField] public List<HurdleObj> TopHurdlePool;

    // start() ���� ������Ʈ ����Ʈ �������� + PlayerController���� Player�� ü���� ��������
    private void Start()
    {
        SmallJumpHurdlePool = GameObject.Find("SmallJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        BigJumpHurdlePool = GameObject.Find("BigJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        TopHurdlePool = GameObject.Find("TopHurdlePool").GetComponent<HurdlePool>().hurdlePools;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int num = Random.Range(0, 3);
            switch (num)
            {
                case 0:
                    nowMakedHurdle = ChoiceHurdle(SmallJumpHurdlePool, 0);
                    break;
                case 1:
                    nowMakedHurdle = ChoiceHurdle(BigJumpHurdlePool, 1);
                    break;
                case 2:
                    nowMakedHurdle = ChoiceHurdle(TopHurdlePool, 2);
                    break;
                default:

                    break;
            }
            MakeHurdle(nowMakedHurdle, nowMakedHurdlePool);
        }
    }


    // Player�� ü���� 0�� �ƴϸ� ��� ��ֹ� �������� �����ϴ� �Լ�
    // Player�� ü���� 0�̸� ��ֹ� ���� ����

    // ��ֹ��� ������ �����ϴ� �Լ�
    private HurdleObj ChoiceHurdle(List<HurdleObj> hurdlePools, int num)
    {
        HurdleObj hurdle;
        switch (num) // ��ֹ��� �������� y���� �ٸ��� �����Ǿ�� ��
        {
            case 0:
                hurdle = hurdlePool.MakeHurdlePool(new Vector2(makePoint.position.x, -2.8f), hurdlePools);
                nowMakedHurdlePool = hurdlePools;
                return hurdle;
            case 1:
                hurdle = hurdlePool.MakeHurdlePool(new Vector2(makePoint.position.x, -2.21f), hurdlePools);
                nowMakedHurdlePool = hurdlePools;
                return hurdle;
            case 2:
                hurdle = hurdlePool.MakeHurdlePool(new Vector2(makePoint.position.x, 1.7f), hurdlePools);
                nowMakedHurdlePool = hurdlePools;
                return hurdle;
            default:
                return null;


        }
    }

    // ��ֹ��� �����ϴ� �Լ�
    private void MakeHurdle(HurdleObj hurdleObj, List<HurdleObj> hurdlePools)
    {
        hurdleObj.gameObject.SetActive(true);
        hurdlePools.RemoveAt(hurdlePools.Count - 1);
        Hurdle hurdle = hurdleObj.GetComponent<Hurdle>();
        hurdle.SetSpeed(HurdleSpeed);
    }


}
