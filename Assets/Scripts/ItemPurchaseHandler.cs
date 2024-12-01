using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPurchaseHandler : MonoBehaviour
{
    public Button purchaseButton;          // 구매 버튼
    public GameObject itemImage;           // 아이템 이미지
    public TMP_Text coinBox;               // 코인 표시 텍스트
    public GameObject cantOpenMessage;     // 코인 부족 메시지
    public int itemCost;                   // 아이템 가격

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
        }

        // 초기 설정
        if (itemImage != null)
        {
            itemImage.SetActive(false); // 아이템 이미지 비활성화
        }

        if (cantOpenMessage != null)
        {
            cantOpenMessage.SetActive(false); // 메시지 비활성화
        }

        UpdateCoinBox();
    }

    private void OnPurchaseButtonClicked()
    {
        if (DataManager.instance != null && DataManager.instance.nowPlayer != null)
        {
            int currentCoins = DataManager.instance.nowPlayer.coin;

            if (currentCoins >= itemCost)
            {
                // 코인 차감
                DataManager.instance.nowPlayer.coin -= itemCost;
                DataManager.instance.SaveData(DataManager.instance.nowPlayer.name);

                // 아이템 이미지 활성화
                if (itemImage != null)
                {
                    itemImage.SetActive(true);
                }

                // 메시지 비활성화
                if (cantOpenMessage != null)
                {
                    cantOpenMessage.SetActive(false);
                }
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
    }

    private void UpdateCoinBox()
    {
        if (coinBox != null && DataManager.instance != null && DataManager.instance.nowPlayer != null)
        {
            coinBox.text = DataManager.instance.nowPlayer.coin.ToString(); // 숫자만 표시
        }
    }
}
