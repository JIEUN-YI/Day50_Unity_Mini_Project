using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameScene : MonoBehaviour
{
    // 게임의 상태를 구분하여 진행
    public enum GameState { Ready, Running, Gameover }
    public GameState curState;

    [SerializeField] private GameObject patternController; // PatternController를 활성화 하기 위한 Object 저장
    private PlayerController playerController;

    [Header("UI")]
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject restartText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject playerHpUI;
    [SerializeField] private Slider playerHpSlider;
    [SerializeField] private GameObject maxScoreText;
    [SerializeField] private TextMeshProUGUI maxScoreUI;
    private int maxScore;

    [Header("Status")]
    [SerializeField] int curScore;
    [SerializeField] float curPlayerHp;
    private float maxPlayerHp;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
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
        scoreText.SetActive(false);
        SetPlayerHp(playerController.playerHp); // PlayerController.cs에서 playerHp를 사용 설정
        playerHpUI.SetActive(false);
        ShowMaxScore();

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
        scoreText.SetActive(true);
        curScore = (int)playerController.score;
        scoreUI.text = $"현재 점수 : " + curScore.ToString();
        curPlayerHp = playerController.playerHp;
        ChangeSliderHp(curPlayerHp);
        playerHpUI.SetActive(true);
        ShowMaxScore();

        if (GameManager.instance.isGameover == true) // 게임이 종료되면
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
        scoreText.SetActive(true);
        scoreUI.text = $"현재 점수 : {curScore.ToString()}";
        playerHpUI.SetActive(false);
        ShowMaxScore();

        if (Input.GetKeyDown(KeyCode.R)) // 게임종료 중 R키를 누르면
        {
            GameManager.instance.isGameover = false; // 게임 상태를 시작으로 변경
            SceneManager.LoadScene("RunningForever"); // Scene을 재시작
        }
    }

    /// <summary>
    /// PlayerController.cs의 playerHp를 가져와서 최대 체력으로 설정하는 함수
    /// </summary>
    /// <param name="curPlayerHp"></param>
    private void SetPlayerHp(float curPlayerHp)
    {
        maxPlayerHp = curPlayerHp;
    }
    /// <summary>
    /// Hp Slider의 값을 변경하는 함수
    /// </summary>
    /// <param name="curPlayerHp"></param>
    private void ChangeSliderHp(float curPlayerHp)
    {
        playerHpSlider.value = curPlayerHp / maxPlayerHp;
    }
    /// <summary>
    /// 최고 점수 UI를 출력하는 함수
    /// </summary>
    private void ShowMaxScore()
    {
        maxScore = GameManager.instance.SetBestScore(curScore); // 최고점수를 저장
        maxScoreText.SetActive(true);
        maxScoreUI.text = $"최고 점수 : {maxScore.ToString()}";
    }
}
