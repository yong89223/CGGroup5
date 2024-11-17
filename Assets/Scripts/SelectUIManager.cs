using UnityEngine;
using UnityEngine.UI;

public class SelectUIManager : MonoBehaviour
{
    public Button[] chapterButtons; // 0~9번 챕터 버튼
    private PlayerData playerData;

    void Start()
    {
        // DataManager의 인스턴스를 통해 플레이어 데이터를 가져옴
        playerData = DataManager.instance.nowPlayer;

        // 버튼 초기화
        InitializeButtons();
    }

    void InitializeButtons()
    {
        for (int i = 0; i < chapterButtons.Length; i++)
        {
            int index = i; // 클로저 이슈 방지
            bool isUnlocked = playerData.isChapterUnlock[i];

            // 버튼 활성화 상태 설정
            chapterButtons[i].interactable = isUnlocked;

            // 버튼 클릭 이벤트 등록
            chapterButtons[i].onClick.AddListener(() => OnChapterButtonClick(index));
        }
    }

    void OnChapterButtonClick(int chapterIndex)
    {
        if (!playerData.isChapterUnlock[chapterIndex])
        {
            Debug.Log($"챕터 {chapterIndex}는 잠겨 있습니다.");
            return;
        }

        // DataManager를 통해 플레이어 상태 갱신
        playerData.chapterIndex = chapterIndex;
        Debug.Log($"챕터 {chapterIndex}로 입장!");

        // 스테이지 이동 로직 추가 가능
        EnterStage(chapterIndex);
    }

    void EnterStage(int chapterIndex)
    {
        DataManager.instance?.ClearLobbyUI();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
