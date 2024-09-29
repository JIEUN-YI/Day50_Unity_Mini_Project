using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    // 연동하는 cs 파일과 변수 정리
    private PatternPool patternPool = new PatternPool();

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

    [Header("Status")] // 사용하는 변수
    [SerializeField] float setSpeed; // 패턴의 이동 속도
    Coroutine MakePatternRoutin; // 패턴을 생성하는 코루틴
    [SerializeField] float makingTime; // 패턴을 생성하는 속도

     private void Awake()
     {
         pattern0Pool = GameObject.Find("Pattern0Pool").GetComponent<PatternPool>().patternPools;
         pattern1Pool = GameObject.Find("Pattern1Pool").GetComponent<PatternPool>().patternPools;
         pattern2Pool = GameObject.Find("Pattern2Pool").GetComponent<PatternPool>().patternPools;
         pattern3Pool = GameObject.Find("Pattern3Pool").GetComponent<PatternPool>().patternPools;
     }

     private void OnEnable()
     {
         if (GameManager.instance.isGameover == false)
         {
             // 코루틴 시작
             MakePatternRoutin = StartCoroutine(MakePatternR());
         }
     }

     private void Update()
     {

         if (GameManager.instance.isGameover == true)
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
             int num = Random.Range(0, 4);
             switch (num)
             {
                 case 0:
                     nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern0Pool);
                     nowMakePatternPool = pattern0Pool;
                     yield return new WaitForSeconds(makingTime);
                     break;
                 case 1:
                     nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern1Pool);
                     nowMakePatternPool = pattern1Pool;
                     yield return new WaitForSeconds(makingTime);
                     break;
                 case 2:
                     nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern2Pool);
                     nowMakePatternPool = pattern2Pool;
                     yield return new WaitForSeconds(makingTime);
                     break;
                 case 3:
                     nowMakePattern = patternPool.MakePatternPool(new Vector2(makePoint.position.x, 0), pattern3Pool);
                     nowMakePatternPool = pattern3Pool;
                     yield return new WaitForSeconds(makingTime);
                     break;
                 default:
                     break;
             }
             MakePattern(nowMakePattern, nowMakePatternPool);
         }
     }

     private void MakePattern(PatternObj patternObj, List<PatternObj> patternPool)
     {
         patternObj.gameObject.SetActive(true);
         patternPool.RemoveAt(patternPool.Count - 1);
         Pattern pattern = patternObj.GetComponent<Pattern>();
         pattern.SetSpeed(setSpeed);
     }
}
