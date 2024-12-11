using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPurchaseHandler : MonoBehaviour
{
    public Button purchaseButton;              // 구매 버튼
    public TMP_Text coinBox;                   // 코인 표시 텍스트
    public GameObject cantOpenMessage;         // 코인 부족 메시지
    public int itemCost;                       // 아이템 가격

    [System.Serializable]
    public class GachaItem
    {
        public GameObject itemImage;           // 아이템 이미지
        public float probability;              // 확률 (퍼센트 값, 합이 100이 되어야 함)
    }

    public GachaItem[] gachaItems;             // 가챠 아이템 목록

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        }

        // 초기 상태 업데이트
        UpdateCoinBox();
        ResetCantOpenMessage();
    }

    private void OnPurchaseButtonClicked()
    {
        int currentCoins = DataManager.instance.nowPlayer.coin;

        // 버튼 클릭 시 메시지 초기화
        ResetCantOpenMessage();

        if (currentCoins >= itemCost)
        {
            // 코인 차감
            DataManager.instance.nowPlayer.coin -= itemCost;
            DataManager.instance.SaveData(DataManager.instance.nowPlayer.name);

            // 가챠 실행
            ExecuteGacha();
        }
        else
        {
            // 코인 부족 메시지 활성화
            if (cantOpenMessage != null)
            {
                cantOpenMessage.SetActive(true);
            }
        }

        // 코인 업데이트
        UpdateCoinBox();
    }

    private void ExecuteGacha()
    {
        float totalProbability = 0;
        foreach (var item in gachaItems)
        {
            totalProbability += item.probability;
        }

        if (Mathf.Abs(totalProbability - 100f) > 0.01f)
        {
            Debug.LogError("Gacha probabilities must sum to 100.");
            return;
        }

        float randomValue = Random.Range(0f, 100f);
        float cumulativeProbability = 0;

        foreach (var item in gachaItems)
        {
            cumulativeProbability += item.probability;
            if (randomValue <= cumulativeProbability)
            {
                // 선택된 아이템 활성화
                if (item.itemImage != null)
                {
                    item.itemImage.SetActive(true);
                }
                break;
            }
        }
    }

    private void UpdateCoinBox()
    {
        if (coinBox != null && DataManager.instance != null && DataManager.instance.nowPlayer != null)
        {
            coinBox.text = DataManager.instance.nowPlayer.coin.ToString(); // 숫자만 표시
        }
    }

    private void ResetCantOpenMessage()
    {
        if (cantOpenMessage != null)
        {
            cantOpenMessage.SetActive(false);
        }

        // 모든 가챠 아이템 비활성화
        foreach (var item in gachaItems)
        {
            if (item.itemImage != null)
            {
                item.itemImage.SetActive(false);
            }
        }
    }
}
