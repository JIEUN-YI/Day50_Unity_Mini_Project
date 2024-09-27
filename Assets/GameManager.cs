using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameover = false; // 게임 진행 - false / 게임 종료 - true

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

    private void Update()
    {
        // 게임 종료 상태 + R키 입력
        if(isGameover == true && Input.GetKeyDown(KeyCode.R))
        {
            // Scene 재시작
            SceneManager.LoadScene("RunningForever");
            isGameover = false;
        }
    }
}
