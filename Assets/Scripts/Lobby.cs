using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{

    public List<GameObject> chapterButtons;


    // Start is called before the first frame update
    void Start()
    {
        UpdateChapterButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateChapterButtons()
    {
        for (int i = 0; i < chapterButtons.Count; i++)
        {
            // i가 챕터인덱스 이하인 경우 버튼을 활성화하고, 그렇지 않으면 비활성화
            chapterButtons[i].SetActive(i <= DataManager.instance.nowPlayer.chapterIndex);
        }
    }
}
