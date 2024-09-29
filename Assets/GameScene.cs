using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    // 게임의 상태를 구분하여 진행
    public enum GameState { Ready, Running, Gameover }
    public GameState curState;

    [SerializeField] private GameObject patternController; // PatternController를 활성화 하기 위한 Object 저장
    
    [Header("UI")]
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject restartText;

    private void Start()
    {
        curState = GameState.Ready;
    }
    private void Update()
    {
        switch (curState)
        {
            case GameState.Ready:
                Ready();
                break;
            case GameState.Running:
                Running();
                break;
            case GameState.Gameover:
                Gameover();
                break;
            default:
                break;
        }
    }

    private void Ready()
    {
        GameManager.instance.isGameover = true; // GameManager의 isGameover를 게임 종료
        titleText.SetActive(true);
        startText.SetActive(true);
        gameoverText.SetActive(false);
        restartText.SetActive(false);

        if (Input.anyKeyDown) // 아무키나 누르면
        {
            GameManager.instance.isGameover = false; // 게임 시작
            patternController.SetActive(true); // PatternController Object를 활성화
            curState = GameState.Running; // 현재 상태를 게임 중으로 변경
        } 
    }

    private void Running()
    {
        titleText.SetActive(false);
        startText.SetActive(false);
        gameoverText.SetActive(false);
        restartText.SetActive(false);

        if( GameManager.instance.isGameover == true ) // 게임이 종료되면
        {
            curState = GameState.Gameover; // 게임 종료 상태로 변경
        }
    }

    private void Gameover()
    {
        titleText.SetActive(false);
        startText.SetActive(false);
        gameoverText.SetActive(true);
        restartText.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R)) // 게임종료 중 R키를 누르면
        {
            GameManager.instance.isGameover = false; // 게임 상태를 시작으로 변경
            SceneManager.LoadScene("RunningForever"); // Scene을 재시작
        }
    }
}
