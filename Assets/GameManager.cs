using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임의 상태를 구분하여 진행
    public enum GameState { Ready, Running, Gameover, Pause }
    private GameState curState;
    public GameState CurState { get { return curState; } set { curState = value; } }

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } set { instance = value; } }

    private int bestScore; // 최고점수
    public int BestScore { get { return bestScore; } set { bestScore = value; } }

    private float speed = 4; // 패턴, 바닥, 배경의 속도
    public float Speed { get { return speed; } }

    private int curScore;
    public int CurScore { get { return curScore; } set { curScore = value; } }
    private float curPlayerHp;
    public float CurPlayerHp { get { return curPlayerHp; } set { curPlayerHp = value; } }
    private int maxScore;
    public int MaxScore { get { return maxScore; } set { maxScore = value; } }
    private float maxPlayerHp;
    public float MaxPlayerHp { get { return maxPlayerHp; } set { maxPlayerHp = value; } }


    private bool isGameOver = true;// 게임 진행 - false / 게임 종료 - true
    public bool IsGameOver { get { return isGameOver; } set { isGameOver = value; } }

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

    private void Start()
    {
        curState = GameState.Ready;
    }

    /// <summary>
    /// 게임의 최고점수를 저장
    /// </summary>
    /// <param name="curScore"></param>
    /// <returns></returns>
    public int SetBestScore(int curScore)
    {
        if (GameManager.Instance.BestScore < curScore)
        {
            GameManager.Instance.BestScore = curScore;
            return GameManager.Instance.BestScore;
        }
        return GameManager.Instance.BestScore;
    }
}
