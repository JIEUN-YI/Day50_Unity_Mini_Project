using System.Collections;
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

    Coroutine MakeHurdleRoutin; // update�Լ� ���� ��ֹ��� �����ϴ� �ڷ�ƾ
    [SerializeField] float makeingTime;
    [SerializeField] float playerHp;
    [SerializeField] bool checkHurdle; // ��ֹ��� �ߺ����� Ȯ�� - true : �ߺ� / false : ���ߺ�

    // start() ���� ������Ʈ ����Ʈ �������� + PlayerController���� Player�� ü���� ��������

    private void Awake()
    {
        SmallJumpHurdlePool = GameObject.Find("SmallJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        BigJumpHurdlePool = GameObject.Find("BigJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        TopHurdlePool = GameObject.Find("TopHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        
    }
    private void Start()
    {
       MakeHurdleRoutin = StartCoroutine(MakeHurdleR());
    }

    private void Update()
    {
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerHp;
        // �÷��̾��� ü���� 0�̻��� ����
        // ������ ����(����)���� ��ֹ��� �������� ����
        // �÷��̾��� ü���� 0 ����
        if (playerHp <= 0)
        {
            Debug.Log("��ƾ����");
            StopCoroutine(MakeHurdleRoutin); // ��ֹ� ���� ����
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Hurdle":
                checkHurdle = true;
            break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Hurdle":
                checkHurdle = false;
                break;
        }
    }
   
    // Player�� ü���� 0�� �ƴϸ� ��� ��ֹ� �������� �����ϴ� �ڷ�ƾ
    IEnumerator MakeHurdleR()
    {
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerHp;
        while (playerHp > 0)
        {
            int num = Random.Range(0, 3); // ��ֹ��� ������ �������� ����
            if (checkHurdle == false)
            {

            switch (num)
            {
                case 0:
                    nowMakedHurdle = ChoiceHurdle(SmallJumpHurdlePool, 0);
                    Debug.Log("0�� ��ֹ� ����");
                    yield return new WaitForSeconds(makeingTime);
                    break;
                case 1:
                    nowMakedHurdle = ChoiceHurdle(BigJumpHurdlePool, 1);
                    Debug.Log("1�� ��ֹ� ����");
                    yield return new WaitForSeconds(makeingTime);
                    break;
                case 2:
                    nowMakedHurdle = ChoiceHurdle(TopHurdlePool, 2);
                    Debug.Log("2�� ��ֹ� ����");
                    yield return new WaitForSeconds(makeingTime);
                    break;
                default:
                    break;
            }
            MakeHurdle(nowMakedHurdle, nowMakedHurdlePool);
            Debug.Log("������ֹ� ���");
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
