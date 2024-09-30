using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    // Pattern들의 ObjectPool을 설정
public class PatternPool : MonoBehaviour
{
    // Pattern의 종류별로 5개씩 가지는 PatternObj 타입의 list
    [SerializeField] public List<PatternObj> patternPools = new List<PatternObj>();
    private int size = 20;
    [SerializeField] PatternObj prefab; // 생성할 패턴 프리팹

    [Header("PatternController.cs")]
    [SerializeField] Transform makePoint; // PatternController.cs의 생성위치를 받아서 사용

    // List 순회하며 인스턴스 생성
    private void Awake()
    {
        for(int i = 0; i < size; i++)
        {
            PatternObj pattern = Instantiate(prefab);
            pattern.gameObject.SetActive(false);
            pattern.transform.parent = transform;
            patternPools.Add(pattern);
        }
        // PatternController.cs의 makePoint 사용
        makePoint = GameObject.FindGameObjectWithTag("PatternController").GetComponent<PatternController>().makePoint;
    }

    /// <summary>
    /// ObjectPool List에서 사용한 PatterObj를 불러오는 함수
    /// 용량이 0이면 새로 제작하여 불러오도록 함
    /// 이때 패턴의 모든 Gem 아이템을 활성화 상태로 불러오도록 함 - 추가 예정
    /// </summary>
    /// <param name="position"></param>
    /// <param name="patternPool"></param>
    /// <returns></returns>
    public PatternObj MakePatternPool(Vector2 position, List<PatternObj> patternPool)
    {
        if(patternPool.Count > 0)
        {
            PatternObj makePattern = patternPool[patternPool.Count - 1];
            makePattern.transform.position = position;
            makePattern.transform.parent = null;
            makePattern.patternPool = this;
            return makePattern;
        }
        else
        {
            return Instantiate(prefab);
        }
    }

    /// <summary>
    /// List에 Pattern Object 반환 함수
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="returnPatternPool"></param>
    public void ReturnPatternPool(PatternObj pattern, List<PatternObj> returnPatternPool)
    {
        pattern.gameObject.SetActive (false);
        pattern.transform.parent = makePoint;
        returnPatternPool.Add(pattern);
    }

}
