using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    public GameObject initialPanel; // 처음 나오는 패널
    public GameObject selectPanel;  // Select 패널
    public void ActivateSelectPanel()
    {
        if (initialPanel != null)
        {
            initialPanel.SetActive(false); // 처음 패널 비활성화
        }
        if (selectPanel != null)
        {
            selectPanel.SetActive(true); // Select 패널 활성화
        }
    }
}
