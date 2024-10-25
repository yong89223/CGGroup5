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
            // i�� é���ε��� ������ ��� ��ư�� Ȱ��ȭ�ϰ�, �׷��� ������ ��Ȱ��ȭ
            chapterButtons[i].SetActive(i <= DataManager.instance.nowPlayer.chapterIndex);
        }
    }
}
