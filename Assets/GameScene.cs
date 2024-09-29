using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    // ������ ���¸� �����Ͽ� ����
    public enum GameState { Ready, Running, Gameover }
    public GameState curState;

    [SerializeField] private GameObject patternController; // PatternController�� Ȱ��ȭ �ϱ� ���� Object ����
    
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
        GameManager.instance.isGameover = true; // GameManager�� isGameover�� ���� ����
        titleText.SetActive(true);
        startText.SetActive(true);
        gameoverText.SetActive(false);
        restartText.SetActive(false);

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

        if( GameManager.instance.isGameover == true ) // ������ ����Ǹ�
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

        if (Input.GetKeyDown(KeyCode.R)) // �������� �� RŰ�� ������
        {
            GameManager.instance.isGameover = false; // ���� ���¸� �������� ����
            SceneManager.LoadScene("RunningForever"); // Scene�� �����
        }
    }
}
