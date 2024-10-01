using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    // 연동하는 cs 파일과 변수 정리
    private PatternPool patternPool = new PatternPool();
    private PlayerController playerController;
    [SerializeField] private float curPlayerHp;
    [SerializeField] private float maxPlayerHp;
    [SerializeField] private float checkHp;

    [Header("PatternPool.cs")] // PatternPool.cs에서 사용할 변수
    [SerializeField] public Transform makePoint; // 패턴이 생성되는 위치

    [Header("PatternObj.cs")]
    public PatternObj nowMakePattern;
    public List<PatternObj> nowMakePatternPool;

    [Header("Pattern Pool List")]
    [SerializeField] public List<PatternObj> pattern0Pool;
    [SerializeField] public List<PatternObj> pattern1Pool;
    [SerializeField] public List<PatternObj> pattern2Pool;
    [SerializeField] public List<PatternObj> pattern3Pool;
    [SerializeField] public List<PatternObj> pattern4Pool;
    [SerializeField] public List<PatternObj> pattern5Pool;
    [SerializeField] public List<PatternObj> pattern6Pool;
    [SerializeField] public List<PatternObj> pattern7Pool;
    [SerializeField] public List<PatternObj> pattern8Pool;
    [SerializeField] public List<PatternObj> pattern9Pool;

    [Header("Status")] // 사용하는 변수
    Coroutine MakePatternRoutin; // 패턴을 생성하는 코루틴
    float makingTime; // 패턴을 생성하는 속도
    int makeNum; // 패턴을 생성하는 랜덤 변수
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pattern0Pool = GameObject.Find("Pattern0Pool").GetComponent<PatternPool>().patternPools;
        pattern1Pool = GameObject.Find("Pattern1Pool").GetComponent<PatternPool>().patternPools;
        pattern2Pool = GameObject.Find("Pattern2Pool").GetComponent<PatternPool>().patternPools;
        pattern3Pool = GameObject.Find("Pattern3Pool").GetComponent<PatternPool>().patternPools;
        pattern4Pool = GameObject.Find("Pattern4Pool").GetComponent<PatternPool>().patternPools;
        pattern5Pool = GameObject.Find("Pattern5Pool").GetComponent<PatternPool>().patternPools;
        pattern6Pool = GameObject.Find("Pattern6Pool").GetComponent<PatternPool>().patternPools;
        pattern7Pool = GameObject.Find("Pattern7Pool").GetComponent<PatternPool>().patternPools;
        pattern8Pool = GameObject.Find("Pattern8Pool").GetComponent<PatternPool>().patternPools;
        pattern9Pool = GameObject.Find("Pattern9Pool").GetComponent<PatternPool>().patternPools;
    }

    private void Start()
    {
        makingTime = 2.72f;
        curPlayerHp = playerController.playerHp;
        maxPlayerHp = curPlayerHp;
        if (GameManager.instance.isGameover == false)
        {
            // 코루틴 시작
            MakePatternRoutin = StartCoroutine(MakePatternR());
        }
    }
    private void Update()
    {
        curPlayerHp = playerController.playerHp;
        curPlayerHp = Mathf.Min(curPlayerHp, maxPlayerHp);
        checkHp = curPlayerHp / maxPlayerHp;
        if (checkHp >= 0.6f) // 전체 체력의 60프로 이상이 남은 경우
        {
            makeNum = 7;
        }
        else if (checkHp < 0.6f) // 전체 체력의 60프로 이하가 남은 경우
        {
            makeNum = 10; // 체력포션 랜덤 발생
        }
        if (GameManager.instance.isGameover == true) // 게임이 종료된 경우
        {
            // 코루틴 종료
            StopCoroutine(MakePatternRoutin);
        }
    }

    // 코루틴 제작
    IEnumerator MakePatternR()
    {
        while (GameManager.instance.isGameover == false)
        {
            // 생성할 패턴의 종류를 랜덤으로 선정
            int num = Random.Range(0, makeNum);
            switch (num)
            {
                case 0:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern0Pool);
                    nowMakePatternPool = pattern0Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 1:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern1Pool);
                    nowMakePatternPool = pattern1Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 2:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern2Pool);
                    nowMakePatternPool = pattern2Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 3:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern3Pool);
                    nowMakePatternPool = pattern3Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 4:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern4Pool);
                    nowMakePatternPool = pattern4Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 5:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern5Pool);
                    nowMakePatternPool = pattern5Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 6:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern6Pool);
                    nowMakePatternPool = pattern6Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 7:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern7Pool);
                    nowMakePatternPool = pattern7Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 8:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern8Pool);
                    nowMakePatternPool = pattern8Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                case 9:
                    nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern9Pool);
                    nowMakePatternPool = pattern9Pool;
                    MakePattern(nowMakePattern, nowMakePatternPool);
                    yield return new WaitForSeconds(makingTime);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 패턴을 출력하는 함수
    /// </summary>
    /// <param name="patternObj"></param>
    /// <param name="patternPool"></param>
    private void MakePattern(PatternObj patternObj, List<PatternObj> patternPool)
    {
        patternObj.gameObject.SetActive(true);
        patternPool.RemoveAt(patternPool.Count - 1);
        Pattern pattern = patternObj.GetComponent<Pattern>();
        pattern.SetSpeed(GameManager.instance.speed);
    }
}
