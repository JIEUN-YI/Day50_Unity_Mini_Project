using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject patternController; // PatternController를 활성화 하기 위한 Object 저장
    [SerializeField] private PlayerController playerController;

    private void Update()
    {
        switch (GameManager.Instance.CurState)
        {
            case GameManager.GameState.Ready:
                Ready();
                break;
            case GameManager.GameState.Running:
                Running();
                break;
            case GameManager.GameState.Gameover:
                Gameover();
                break;
            case GameManager.GameState.Pause:
                Pause();
                break;
            default:
                break;
        }
    }

    private void Ready()
    {
        GameManager.Instance.IsGameOver = true; // GameManager의 isGameover를 게임 종료
        SetPlayerHp(playerController.PlayerHp); // PlayerController.cs에서 playerHp를 사용 설정

        if (Input.anyKeyDown) // 아무키나 누르면
        {
            GameManager.Instance.IsGameOver = false; // 게임 시작
            patternController.SetActive(true); // PatternController Object를 활성화
            GameManager.Instance.CurState = GameManager.GameState.Running; // 현재 상태를 게임 중으로 변경
        }
    }

    private void Running()
    {
        GameManager.Instance.CurScore = (int)playerController.Score;
        GameManager.Instance.CurPlayerHp = playerController.PlayerHp;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.CurState = GameManager.GameState.Pause;
        }

        if (GameManager.Instance.IsGameOver == true) // 게임이 종료되면
        {
            GameManager.Instance.CurState = GameManager.GameState.Gameover; // 게임 종료 상태로 변경
        }
    }

    private void Gameover()
    {
        if (Input.GetKeyDown(KeyCode.R)) // 게임종료 중 R키를 누르면
        {
            GameManager.Instance.IsGameOver = false; // 게임 상태를 시작으로 변경
            Time.timeScale = 1f;
            GameManager.Instance.CurState = GameState.Ready;
            SceneManager.LoadScene("RunningForever"); // Scene을 재시작
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void Pause()
    {
        Time.timeScale = 0; // 일시정지
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            GameManager.Instance.CurState = GameManager.GameState.Running;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    /// <summary>
    /// PlayerController.cs의 playerHp를 가져와서 최대 체력으로 설정하는 함수
    /// </summary>
    /// <param name="curPlayerHp"></param>
    private void SetPlayerHp(float curPlayerHp)
    {
        GameManager.Instance.MaxPlayerHp = curPlayerHp;
    }

}
