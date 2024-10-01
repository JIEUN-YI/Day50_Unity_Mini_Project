using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int bestScore; // �ְ�����
    [SerializeField] public float speed; // ����, �ٴ�, ����� �ӵ�
    public int BestScore { get { return bestScore; } }

    // �������� ����
    public bool isGameover = true; // ���� ���� - false / ���� ���� - true

    private void Awake()
    {
        if(instance == null)   
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }
    }

    /// <summary>
    /// ������ �ְ������� ����
    /// </summary>
    /// <param name="curScore"></param>
    /// <returns></returns>
    public int SetBestScore(int curScore)
    {
        if(bestScore < curScore)
        {
            bestScore = curScore;
            return bestScore;
        }
        return bestScore;
    }
}
