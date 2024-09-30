using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameScene : MonoBehaviour
{
    // ������ ���¸� �����Ͽ� ����
    public enum GameState { Ready, Running, Gameover }
    public GameState curState;

    [SerializeField] private GameObject patternController; // PatternController�� Ȱ��ȭ �ϱ� ���� Object ����
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
        GameManager.instance.isGameover = true; // GameManager�� isGameover�� ���� ����
        titleText.SetActive(true);
        startText.SetActive(true);
        gameoverText.SetActive(false);
        restartText.SetActive(false);
        scoreText.SetActive(false);
        SetPlayerHp(playerController.playerHp); // PlayerController.cs���� playerHp�� ��� ����
        playerHpUI.SetActive(false);
        ShowMaxScore();

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

        if (Input.GetKeyDown(KeyCode.R)) // �������� �� RŰ�� ������
        {
            GameManager.instance.isGameover = false; // ���� ���¸� �������� ����
            SceneManager.LoadScene("RunningForever"); // Scene�� �����
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
