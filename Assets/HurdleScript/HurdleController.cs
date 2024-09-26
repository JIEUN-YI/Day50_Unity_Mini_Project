using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ObjectPool ������ ����Ͽ� �������� ��ֹ��� �����ϰ� ��ȯ�ϵ��� ����
public class HurdleController : MonoBehaviour
{
    private HurdlePool hurdlePool = new HurdlePool(); // HurdlePoop.cs ����
    [SerializeField] float HurdleSpeed;

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

    Coroutine MakeHurdleRoutin; // update�Լ� ���� ��ֹ��� �����ϴ� �ڷ�ƾ
    [SerializeField] float makeingTime;
    [SerializeField] float playerHp;
    PlayerController playerController; // PlayerController.cs�� �����ϱ�

    [Header("CheckCollision.cs")]
    CheckCollision checkCollision; // CheckCollision.cs ����
    [SerializeField] bool checkHurdle;


    private void Awake()
    {
        SmallJumpHurdlePool = GameObject.Find("SmallJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        BigJumpHurdlePool = GameObject.Find("BigJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        TopHurdlePool = GameObject.Find("TopHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        checkCollision = GameObject.FindGameObjectWithTag("MakingBoundary").GetComponent<CheckCollision>();
    }
    private void Start()
    {
       MakeHurdleRoutin = StartCoroutine(MakeHurdleR());
    }

    private void Update()
    {
        playerHp = playerController.playerHp;
        // �÷��̾��� ü���� 0�̻��� ����
        // ������ ����(����)���� ��ֹ��� �������� ����
        // �÷��̾��� ü���� 0 ����
        if (playerHp <= 0)
        {
            StopCoroutine(MakeHurdleRoutin); // ��ֹ� ���� ����
        }
    }
   
    // Player�� ü���� 0�� �ƴϸ� ��� ��ֹ� �������� �����ϴ� �ڷ�ƾ
    IEnumerator MakeHurdleR()
    {
        playerHp = playerController.playerHp;
        checkHurdle = checkCollision.checkHurdle;
        while (playerHp > 0)
        {
            int num = Random.Range(0, 3); // ��ֹ��� ������ �������� ����
            if (checkHurdle == false)
            {
                switch (num)
            {
                case 0:
                    nowMakedHurdle = ChoiceHurdle(SmallJumpHurdlePool, 0);
                    yield return new WaitForSeconds(makeingTime);
                    break;
                case 1:
                    nowMakedHurdle = ChoiceHurdle(BigJumpHurdlePool, 1);
                    yield return new WaitForSeconds(makeingTime);
                    break;
                case 2:
                    nowMakedHurdle = ChoiceHurdle(TopHurdlePool, 2);
                    yield return new WaitForSeconds(makeingTime);
                    break;
                default:
                    break;
            }
            MakeHurdle(nowMakedHurdle, nowMakedHurdlePool);
            }
            else if(checkHurdle == true)
            {
                yield return new WaitUntil(() => false);
            }
        }

    }


    /// <summary>
    /// ��ֹ��� ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="hurdlePools"></param>
    /// <param name="num"></param>
    /// <returns></returns>
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

    /// <summary>
    /// ��ֹ��� �����ϴ� �Լ�
    /// </summary>
    /// <param name="hurdleObj"></param>
    /// <param name="hurdlePools"></param>
    private void MakeHurdle(HurdleObj hurdleObj, List<HurdleObj> hurdlePools)
    {
        hurdleObj.gameObject.SetActive(true);
        hurdlePools.RemoveAt(hurdlePools.Count - 1);
        Hurdle hurdle = hurdleObj.GetComponent<Hurdle>();
        hurdle.SetSpeed(HurdleSpeed);
    }

}
