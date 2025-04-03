using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIViewer : UIBinder
{
    private void Update()
    {
        switch (GameManager.Instance.CurState)
        {
            case GameManager.GameState.Ready:
                ReadyUI();
                break;
            case GameManager.GameState.Running:
                RunningUI();
                break;
            case GameManager.GameState.Gameover:
                GameOverUI();
                break;
            case GameManager.GameState.Pause:
                PauseUI();
                break;
            default:
                break;
        }
    }
    private void ReadyUI()
    {
        GetUI("TitleText").SetActive(true);
        GetUI("StartText").SetActive(true);
        GetUI("GameOverText").SetActive(false);
        GetUI("RestartText").SetActive(false);
        GetUI("ScoreText").SetActive(false);
        GetUI("PlayerHp").SetActive(false);
        GetUI("PauseText").SetActive(false);
        GetUI("UnpauseText").SetActive(false);
        ShowMaxScore();
    }

    private void RunningUI()
    {
        GetUI("TitleText").SetActive(false);
        GetUI("StartText").SetActive(false);
        GetUI("GameOverText").SetActive(false);
        GetUI("RestartText").SetActive(false);
        GetUI("ScoreText").SetActive(true);
        GetUI<TextMeshProUGUI>("ScoreText").SetText($"현재 점수 : " + GameManager.Instance.CurScore.ToString());
        ShowMaxScore();
        GetUI("PlayerHp").SetActive(true);
        ChangeSliderHp(GameManager.Instance.CurPlayerHp);
        GetUI("PauseText").SetActive(false);
        GetUI("UnpauseText").SetActive(false);
    }
    private void GameOverUI()
    {
        GetUI("TitleText").SetActive(false);
        GetUI("StartText").SetActive(false);
        GetUI("GameOverText").SetActive(true);
        GetUI("RestartText").SetActive(true);
        GetUI("ScoreText").SetActive(true);
        GetUI<TextMeshProUGUI>("ScoreText").SetText($"현재 점수 : " + GameManager.Instance.CurScore.ToString());
        ShowMaxScore();
        GetUI("PlayerHp").SetActive(false);
        GetUI("PauseText").SetActive(false);
        GetUI("UnpauseText").SetActive(false);
    }
    private void PauseUI()
    {
        GetUI("PauseText").SetActive(true);
        GetUI("UnpauseText").SetActive(true);
    }

    /// <summary>
    /// 최고 점수 UI를 출력하는 함수
    /// </summary>
    private void ShowMaxScore()
    {
        GameManager.Instance.MaxScore = GameManager.Instance.SetBestScore(GameManager.Instance.CurScore); // 최고점수를 저장
        GetUI("MaxScoreText").SetActive(true);
        GetUI<TextMeshProUGUI>("MaxScoreText").text = $"최고 점수 : {GameManager.Instance.MaxScore.ToString()}";
    }
    /// <summary>
    /// Hp Slider의 값을 변경하는 함수
    /// </summary>
    /// <param name="curPlayerHp"></param>
    private void ChangeSliderHp(float curPlayerHp)
    {
        GetUI<Slider>("PlayerHp").value = curPlayerHp / GameManager.Instance.MaxPlayerHp;
    }
}
