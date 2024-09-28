using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternObj : MonoBehaviour
{
    public PatternPool patternPool; // PatternPool.cs ����

    [Header ("Pattern Pool")] // �� Pattern���� Object Pool�� ����
    [SerializeField] public List<PatternObj> pattern0Pool;
    [SerializeField] public List<PatternObj> pattern1Pool;
    [SerializeField] public List<PatternObj> pattern2Pool;
    [SerializeField] public List<PatternObj> pattern3Pool;

    // PatternController���� ���� ����ϴ� PatternObj�� �� List<PatternObj>�� ��ȯ�ϱ� ���ؼ� return�� list�� object�� �޾ƿ����� ����
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
    /// Object�� ȸ���ϴ� �Լ�
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
