using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int bestScore; // �ְ�����
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

    /**/
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
