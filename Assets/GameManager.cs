using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int bestScore; // 최고점수
    [SerializeField] public float speed; // 패턴, 바닥, 배경의 속도
    public int BestScore { get { return bestScore; } }

    // 전역변수 선언
    public bool isGameover = true; // 게임 진행 - false / 게임 종료 - true

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
    /// 게임의 최고점수를 저장
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
