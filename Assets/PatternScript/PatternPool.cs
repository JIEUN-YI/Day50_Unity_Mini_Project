using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    // Pattern���� ObjectPool�� ����
public class PatternPool : MonoBehaviour
{
    // Pattern�� �������� 5���� ������ PatternObj Ÿ���� list
    [SerializeField] public List<PatternObj> patternPools = new List<PatternObj>();
    private int size = 20;
    [SerializeField] PatternObj prefab; // ������ ���� ������

    [Header("PatternController.cs")]
    [SerializeField] Transform makePoint; // PatternController.cs�� ������ġ�� �޾Ƽ� ���

    // List ��ȸ�ϸ� �ν��Ͻ� ����
    private void Awake()
    {
        for(int i = 0; i < size; i++)
        {
            PatternObj pattern = Instantiate(prefab);
            pattern.gameObject.SetActive(false);
            pattern.transform.parent = transform;
            patternPools.Add(pattern);
        }
        // PatternController.cs�� makePoint ���
        makePoint = GameObject.FindGameObjectWithTag("PatternController").GetComponent<PatternController>().makePoint;
    }

    /// <summary>
    /// ObjectPool List���� ����� PatterObj�� �ҷ����� �Լ�
    /// �뷮�� 0�̸� ���� �����Ͽ� �ҷ������� ��
    /// �̶� ������ ��� Gem �������� Ȱ��ȭ ���·� �ҷ������� �� - �߰� ����
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
    /// List�� Pattern Object ��ȯ �Լ�
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
