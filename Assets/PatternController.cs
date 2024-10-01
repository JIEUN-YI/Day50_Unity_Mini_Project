using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    // �����ϴ� cs ���ϰ� ���� ����
    private PatternPool patternPool = new PatternPool();
    private PlayerController playerController;
    [SerializeField] private float curPlayerHp;
    [SerializeField] private float maxPlayerHp;
    [SerializeField] private float checkHp;

    [Header("PatternPool.cs")] // PatternPool.cs���� ����� ����
    [SerializeField] public Transform makePoint; // ������ �����Ǵ� ��ġ

    [Header("PatternObj.cs")]
    public PatternObj nowMakePattern;
    public List<PatternObj> nowMakePatternPool;

    [Header("Pattern Pool List")]
    [SerializeField] public List<PatternObj> pattern0Pool;
    [SerializeField] public List<PatternObj> pattern1Pool;
    [SerializeField] public List<PatternObj> pattern2Pool;
    [SerializeField] public List<PatternObj> pattern3Pool;
    [SerializeField] public List<PatternObj> pattern4Pool;

    [Header("Status")] // ����ϴ� ����
    Coroutine MakePatternRoutin; // ������ �����ϴ� �ڷ�ƾ
    [SerializeField] float makingTime; // ������ �����ϴ� �ӵ�
    int makeNum; // ������ �����ϴ� ���� ����
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pattern0Pool = GameObject.Find("Pattern0Pool").GetComponent<PatternPool>().patternPools;
        pattern1Pool = GameObject.Find("Pattern1Pool").GetComponent<PatternPool>().patternPools;
        pattern2Pool = GameObject.Find("Pattern2Pool").GetComponent<PatternPool>().patternPools;
        pattern3Pool = GameObject.Find("Pattern3Pool").GetComponent<PatternPool>().patternPools;
        pattern4Pool = GameObject.Find("Pattern4Pool").GetComponent<PatternPool>().patternPools;
    }

    private void Start()
    {
        makingTime = GameManager.instance.speed;
        curPlayerHp = playerController.playerHp;
        maxPlayerHp = curPlayerHp;
        if (GameManager.instance.isGameover == false)
        {
            // �ڷ�ƾ ����
            MakePatternRoutin = StartCoroutine(MakePatternR());
        }
    }
    private void Update()
    {
        curPlayerHp = playerController.playerHp;
        curPlayerHp = Mathf.Min(curPlayerHp, maxPlayerHp);
        checkHp = curPlayerHp / maxPlayerHp;
        if (checkHp >= 0.6f) // ��ü ü���� 60���� �̻��� ���� ���
        {
            makeNum = 4;
        }
        else if (checkHp < 0.6f) // ��ü ü���� 60���� ���ϰ� ���� ���
        {
            makeNum = 5;
        }
        if (GameManager.instance.isGameover == true) // ������ ����� ���
        {
            // �ڷ�ƾ ����
            StopCoroutine(MakePatternRoutin);
        }
    }

    // �ڷ�ƾ ����
    IEnumerator MakePatternR()
    {
        while (GameManager.instance.isGameover == false)
        {
            // ������ ������ ������ �������� ����
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
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// ������ ����ϴ� �Լ�
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
