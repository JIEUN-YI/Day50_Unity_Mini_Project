using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameover = false; // ���� ���� - false / ���� ���� - true

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
        // ���� ���� ���� + RŰ �Է�
        if(isGameover == true && Input.GetKeyDown(KeyCode.R))
        {
            // Scene �����
            SceneManager.LoadScene("RunningForever");
            isGameover = false;
        }
    }
}
