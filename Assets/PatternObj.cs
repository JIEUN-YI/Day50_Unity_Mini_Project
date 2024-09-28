using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternObj : MonoBehaviour
{
    public PatternPool patternPool; // PatternPool.cs 연동

    [Header ("Pattern Pool")] // 각 Pattern별로 Object Pool을 선언
    [SerializeField] public List<PatternObj> pattern0Pool;
    [SerializeField] public List<PatternObj> pattern1Pool;
    [SerializeField] public List<PatternObj> pattern2Pool;
    [SerializeField] public List<PatternObj> pattern3Pool;

    // PatternController에서 현재 사용하는 PatternObj와 그 List<PatternObj>를 반환하기 위해서 return할 list와 object로 받아오도록 선언
    [Header("PatternController.cs")]
    PatternObj returnPatternObj;
    List<PatternObj> returnPatternPool;

    private void OnEnable()
    {
        returnPatternObj = GameObject.FindGameObjectWithTag("PatternController").GetComponent<PatternController>().nowMakePattern;
        returnPatternPool = GameObject.FindGameObjectWithTag("PatternController").GetComponent<PatternController>().nowMakePatternPool;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "DeleteZone")
        {
            ReturnPattern(returnPatternObj, returnPatternPool);
        }
    }

    /// <summary>
    /// Object를 회수하는 함수
    /// </summary>
    /// <param name="returnPatternObj"></param>
    /// <param name="returnPatternPool"></param>
    public void ReturnPattern(PatternObj returnPatternObj, List<PatternObj> returnPatternPool)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        patternPool.ReturnPatternPool(returnPatternObj, returnPatternPool);
    }
}
