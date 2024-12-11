using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFind : MonoBehaviour
{
    public Button unlockItemButton; // 해금 버튼
    public Button unlockItemButton2; // 해금 버튼
    public Button unlockItemButton3; // 해금 버튼
    public Button unlockItemButton4; // 해금 버튼

    public void FindAndInitializeButtons()
    {
        unlockItemButton = GameObject.Find("UnlockItemButton").GetComponent<Button>();
        unlockItemButton2 = GameObject.Find("UnlockItemButton2").GetComponent<Button>();
        unlockItemButton3 = GameObject.Find("UnlockItemButton3").GetComponent<Button>();
        unlockItemButton4 = GameObject.Find("UnlockItemButton4").GetComponent<Button>();

        if (unlockItemButton != null)
            unlockItemButton.onClick.AddListener(DataManager.instance.UnlockAndEquipRandomItem);
        if (unlockItemButton2 != null)
            unlockItemButton2.onClick.AddListener(DataManager.instance.UnlockAndEquipRandomItem);
        if (unlockItemButton3 != null)
            unlockItemButton3.onClick.AddListener(DataManager.instance.UnlockAndEquipRandomItem);
        if (unlockItemButton4 != null)
            unlockItemButton4.onClick.AddListener(DataManager.instance.UnlockAndEquipRandomItem);
    }
}
