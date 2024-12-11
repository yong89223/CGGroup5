using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreScreen : MonoBehaviour
{
    public TMP_Text UsernameBox; // UsernameBox UI 연결
    public TMP_Text CoinBox; // CoinBox UI 연결

    private void OnEnable()
    {
        // Store 화면 활성화 시 사용자 이름과 코인 값을 업데이트
        UpdateStoreScreen();
    }

    // 데이터 업데이트 메서드
    private void UpdateStoreScreen()
    {
        if (DataManager.instance != null && DataManager.instance.nowPlayer != null)
        {
            UsernameBox.text = DataManager.instance.nowPlayer.name; // 사용자 이름 업데이트
            CoinBox.text = $"{DataManager.instance.nowPlayer.coin}"; // 재화 업데이트
        }
    }
}
