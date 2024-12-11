using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManagerY : MonoBehaviour
{
    public TMP_Text timerText;            // 타이머를 표시할 UI 텍스트
    public GameObject successScreen;     // 게임 성공 화면
    public GameObject failureScreen;     // 게임 실패 화면
    public int gameDuration = 180;       // 게임 시간(초) - 3분
    public int preparationTime = 3;      // 준비 타이머(초)

    private float gameTimer;             // 남은 게임 시간
    private bool isGameStarted = false;  // 게임 시작 여부
    private bool isGameOver = false;     // 게임 종료 여부
    private Health playerHealth;         // 플레이어의 Health 컴포넌트

    void Start()
    {
        // 플레이어 Health 컴포넌트 참조
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }

        if (playerHealth == null || !playerHealth.isPlayer)
        {
            Debug.LogError("플레이어 Health 컴포넌트를 찾을 수 없습니다!");
            return;
        }

        // 초기 설정
        gameTimer = gameDuration;
        successScreen.SetActive(false);
        failureScreen.SetActive(false);

        // 체력 변화 이벤트 구독
        Health.OnPlayerHealthChanged += HandlePlayerHealthChanged;

        // 준비 타이머 시작
        StartCoroutine(StartPreparation());
    }

    void Update()
    {
        if (!isGameStarted || isGameOver) return;

        // 게임 타이머 감소
        gameTimer -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.CeilToInt(gameTimer)}s";

        // 승리 조건 체크
        if (gameTimer <= 0 && playerHealth.currentHealth > 0)
        {
            ShowSuccessScreen();
        }
    }

    private IEnumerator StartPreparation()
    {
        timerText.text = $"Game starts in {preparationTime}s";

        for (int i = preparationTime; i > 0; i--)
        {
            timerText.text = $"Game starts in {i}s";
            yield return new WaitForSeconds(1f);
        }

        StartGame();
    }

    private void StartGame()
    {
        isGameStarted = true;
        timerText.text = $"Time: {Mathf.CeilToInt(gameTimer)}s";
    }

    private void HandlePlayerHealthChanged(int currentHealth)
    {
        if (currentHealth <= 0 && !isGameOver)
        {
            ShowFailureScreen();
        }
    }

    private void ShowSuccessScreen()
    {
        isGameOver = true;
        successScreen.SetActive(true);
        timerText.text = "You Win!";
        Time.timeScale = 0; // 게임 정지
    }

    private void ShowFailureScreen()
    {
        isGameOver = true;
        failureScreen.SetActive(true);
        timerText.text = "You Lose!";
        Time.timeScale = 0; // 게임 정지
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // 게임 속도 복구
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void ClearButtonClick()
    {
        Time.timeScale = 1; // 씬 전환 전에 타임스케일 복구
        SceneManager.sceneLoaded -= OnSelectSceneLoaded; // 중복 구독 방지
        SceneManager.sceneLoaded += OnSelectSceneLoaded;

        UnlockNextStage(DataManager.instance.nowPlayer.chapterIndex);
        SceneManager.LoadScene("SelectScene");
    }

    public void MenuButtonClick()
    {
        Time.timeScale = 1; // 씬 전환 전에 타임스케일 복구
        SceneManager.sceneLoaded -= OnSelectSceneLoaded; // 중복 구독 방지
        SceneManager.sceneLoaded += OnSelectSceneLoaded;

        SceneManager.LoadScene("SelectScene");
    }

    private void OnSelectSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SelectScene")
        {
            SceneManage uiManager = FindObjectOfType<SceneManage>();
            if (uiManager != null)
            {
                uiManager.ActivateSelectPanel();
            }
            else
            {
                Debug.LogWarning("SelectScene에서 SceneManage를 찾을 수 없습니다!");
            }

            SceneManager.sceneLoaded -= OnSelectSceneLoaded; // 이벤트 해제
        }
    }

    private void UnlockNextStage(int currentStageIndex)
    {
        int nextStageIndex = currentStageIndex + 1;

        if (nextStageIndex < DataManager.instance.nowPlayer.isChapterUnlock.Length)
        {
            DataManager.instance.nowPlayer.isChapterUnlock[nextStageIndex] = true;
            DataManager.instance.SaveData(DataManager.instance.nowPlayer.name);
        }
        else
        {
            Debug.LogWarning("모든 스테이지가 이미 해금되었습니다!");
        }
    }

    private void OnDestroy()
    {
        Health.OnPlayerHealthChanged -= HandlePlayerHealthChanged;
    }
}