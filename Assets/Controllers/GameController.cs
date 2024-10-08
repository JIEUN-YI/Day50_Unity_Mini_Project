using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    // ������ ���¸� �����Ͽ� ����
    public enum GameState { Ready, Running, Gameover, Pause }
    //public enum GameState { Ready, Running, Gameover }
    public GameState curState;

    [SerializeField] private GameObject patternController; // PatternController�� Ȱ��ȭ �ϱ� ���� Object ����
    private PlayerController playerController;

    [Header("UI")]
    // Ȱ��ȭ ���� ������ ���� GameObject ������ TextUI
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject restartText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject playerHpUI;
    [SerializeField] private GameObject maxScoreText;
    [SerializeField] private GameObject PauseText;
    [SerializeField] private GameObject UnpauseText;
    // UI ����� ���� UI ������ UI 
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI maxScoreUI;
    [SerializeField] private Slider playerHpSlider;

    [Header("Status")]
    [SerializeField] int curScore;
    [SerializeField] float curPlayerHp;
    private int maxScore;
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
            case GameState.Pause:
                Pause();
                break;
            default:
                break;
        }
    }

    private void Ready()
    {
        GameManager.instance.isGameover = true; // GameManager�� isGameover�� ���� ����
        titleText.SetActive(true);
        startText.SetActive(true);
        gameoverText.SetActive(false);
        restartText.SetActive(false);
        scoreText.SetActive(false);
        SetPlayerHp(playerController.playerHp); // PlayerController.cs���� playerHp�� ��� ����
        playerHpUI.SetActive(false);
        ShowMaxScore();
        PauseText.SetActive(false);
        UnpauseText.SetActive(false);

        if (Input.anyKeyDown) // �ƹ�Ű�� ������
        {
            GameManager.instance.isGameover = false; // ���� ����
            patternController.SetActive(true); // PatternController Object�� Ȱ��ȭ
            curState = GameState.Running; // ���� ���¸� ���� ������ ����
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
        scoreUI.text = $"���� ���� : " + curScore.ToString();
        curPlayerHp = playerController.playerHp;
        ChangeSliderHp(curPlayerHp);
        playerHpUI.SetActive(true);
        ShowMaxScore();
        PauseText.SetActive(false);
        UnpauseText.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            curState = GameState.Pause;
        }

        if (GameManager.instance.isGameover == true) // ������ ����Ǹ�
        {
            curState = GameState.Gameover; // ���� ���� ���·� ����
        }
    }

    private void Gameover()
    {
        titleText.SetActive(false);
        startText.SetActive(false);
        gameoverText.SetActive(true);
        restartText.SetActive(true);
        scoreText.SetActive(true);
        scoreUI.text = $"���� ���� : {curScore.ToString()}";
        playerHpUI.SetActive(false);
        ShowMaxScore();
        PauseText.SetActive(false);
        UnpauseText.SetActive(false);

        if (Input.GetKeyDown(KeyCode.R)) // �������� �� RŰ�� ������
        {
            GameManager.instance.isGameover = false; // ���� ���¸� �������� ����
            Time.timeScale = 1f;
            SceneManager.LoadScene("RunningForever"); // Scene�� �����
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void Pause()
    {
        PauseText.SetActive(true);
        UnpauseText.SetActive(true);
        Time.timeScale = 0; // �Ͻ�����
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            curState = GameState.Running;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    /// <summary>
    /// PlayerController.cs�� playerHp�� �����ͼ� �ִ� ü������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="curPlayerHp"></param>
    private void SetPlayerHp(float curPlayerHp)
    {
        maxPlayerHp = curPlayerHp;
    }
    /// <summary>
    /// Hp Slider�� ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="curPlayerHp"></param>
    private void ChangeSliderHp(float curPlayerHp)
    {
        playerHpSlider.value = curPlayerHp / maxPlayerHp;
    }
    /// <summary>
    /// �ְ� ���� UI�� ����ϴ� �Լ�
    /// </summary>
    private void ShowMaxScore()
    {
        maxScore = GameManager.instance.SetBestScore(curScore); // �ְ������� ����
        maxScoreText.SetActive(true);
        maxScoreUI.text = $"�ְ� ���� : {maxScore.ToString()}";
    }
}
