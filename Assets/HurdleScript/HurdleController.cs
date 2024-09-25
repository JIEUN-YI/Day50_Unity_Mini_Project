using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ObjectPool 패턴을 사용하여 랜덤으로 장애물을 생성하고 반환하도록 관리
public class HurdleController : MonoBehaviour
{
    private HurdlePool hurdlePool = new HurdlePool(); // HurdlePoop.cs 연동
    float HurdleSpeed = 3;

    [Header("HurdlePool.cs")]
    // 장애물이 생성되는 위치
    [SerializeField] public Transform makePoint;
    [Header("HurdleObj.cs")]
    public HurdleObj nowMakedHurdle; // 지금 생성된 장애물
    public List<HurdleObj> nowMakedHurdlePool; // 지금 생성된 장애물의 오브젝트 풀

    [Header("Pool List")]
    [SerializeField] public List<HurdleObj> SmallJumpHurdlePool;
    [SerializeField] public List<HurdleObj> BigJumpHurdlePool;
    [SerializeField] public List<HurdleObj> TopHurdlePool;

    Coroutine MakeHurdleRoutin; // update함수 동안 장애물을 생성하는 코루틴
    [SerializeField] float makeingTime;
    [SerializeField] float playerHp;

    // start() 에서 오브젝트 리스트 가져오기 + PlayerController에서 Player의 체력을 가져오기

    private void Awake()
    {
        SmallJumpHurdlePool = GameObject.Find("SmallJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        BigJumpHurdlePool = GameObject.Find("BigJumpHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        TopHurdlePool = GameObject.Find("TopHurdlePool").GetComponent<HurdlePool>().hurdlePools;
        playerHp = GameObject.FindWithTag("Player").GetComponent<PlayerController>().playerHp;
        
    }
    private void Start()
    {
        MakeHurdleRoutin = StartCoroutine(MakeHurdle());
    }

    private void Update()
    {
        // 플레이어의 체력이 0이상인 동안
        // 일정한 간격(동기)으로 장애물을 랜덤으로 생성

        // 플레이어의 체력이 0 이하
        if (playerHp <= 0)
        {
            StopCoroutine(MakeHurdleRoutin); // 장애물 생성 중지
        }
    }

    // Player의 체력이 0이 아니면 계속 장애물 랜덤으로 생성하는 코루틴
    IEnumerator MakeHurdle()
    {
        while (playerHp > 0)
        {
            int num = Random.Range(0, 3); // 장애물의 종류를 랜덤으로 생성
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
            playerHp--;
        }

    }

    // 장애물의 종류를 선택하는 함수
    private HurdleObj ChoiceHurdle(List<HurdleObj> hurdlePools, int num)
    {
        HurdleObj hurdle;
        switch (num) // 장애물의 종류별로 y값이 다르게 생성되어야 함
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

    // 장애물을 생성하는 함수
    private void MakeHurdle(HurdleObj hurdleObj, List<HurdleObj> hurdlePools)
    {
        hurdleObj.gameObject.SetActive(true);
        hurdlePools.RemoveAt(hurdlePools.Count - 1);
        Hurdle hurdle = hurdleObj.GetComponent<Hurdle>();
        hurdle.SetSpeed(HurdleSpeed);
    }

}
