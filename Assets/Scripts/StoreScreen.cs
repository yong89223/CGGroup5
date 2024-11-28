using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreScreen : MonoBehaviour
{
    public TMP_Text UsernameBox; // UsernameBox UI 연결
    public TMP_Text CoinBox; // CoinBox UI 연결
    void Start()
    {
                // DataManager에서 사용자 이름, 재화 가져오기
        if (DataManager.instance != null && DataManager.instance.nowPlayer != null)
        {
            UsernameBox.text = DataManager.instance.nowPlayer.name; // 사용자 이름 업데이트
            CoinBox.text = $"{DataManager.instance.nowPlayer.coin}"; // 재화 업데이트
        }

    }


    void Update()
    {
        
    }
}
