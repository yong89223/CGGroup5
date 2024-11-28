using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunsMenu : MonoBehaviour
{
    public Button CoperButton;        // CoperButton 연결
    public GameObject CoperImage;    // 아이템 이미지(Coper) 연결
    public TMP_Text CoinBox;          // CoinBox UI 텍스트 연결
    public GameObject CantOpenMessage; // Coins 부족 시 활성화할 메시지 오브젝트

    private int coperCost = 100;     // Coper 아이템 가격

    void Start()
    {
        // 버튼 클릭 이벤트 등록
        CoperButton.onClick.AddListener(OnCoperButtonClicked);

        // 초기 설정
        if (CoperImage != null)
        {
            CoperImage.SetActive(false);
        }
        if (CantOpenMessage != null)
        {
            CantOpenMessage.SetActive(false); // 시작 시 비활성화
        }

        UpdateCoinBox(); // Coins 업데이트
    }

    private void OnCoperButtonClicked()
    {
        // DataManager에서 Coins 가져오기
        if (DataManager.instance != null && DataManager.instance.nowPlayer != null)
        {
            int currentCoins = DataManager.instance.nowPlayer.coin;

            if (currentCoins >= coperCost)
            {
                // Coins 차감
                DataManager.instance.nowPlayer.coin -= coperCost;
                DataManager.instance.SaveData(DataManager.instance.nowPlayer.name);

                // 아이템 이미지 활성화
                if (CoperImage != null)
                {
                    CoperImage.SetActive(true);
                }

                // Coins 부족 메시지 비활성화
                if (CantOpenMessage != null)
                {
                    CantOpenMessage.SetActive(false);
                }
            }
            else
            {
                // Coins 부족 시 메시지 활성화
                if (CantOpenMessage != null)
                {
                    CantOpenMessage.SetActive(true);
                }
            }

            UpdateCoinBox(); // CoinBox 업데이트
        }
    }

    private void UpdateCoinBox()
    {
        if (CoinBox != null && DataManager.instance != null && DataManager.instance.nowPlayer != null)
        {
            CoinBox.text = DataManager.instance.nowPlayer.coin.ToString();
        }
    }

    private void HideCantOpenMessage()
    {
        if (CantOpenMessage != null)
        {
            CantOpenMessage.SetActive(false);
        }
    }
}
