using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Item
{
    public int id; //아이템의 식별자
    public string name; //아이템의 이름
    public bool isItemUnlock; //아이템의 해금정보
    public int damage; //아이템이 증가시킬 데미지
    public int hp; //아이템이 증가시킬 체력
}

public class PlayerInventory
{
    public List<Item> items = new List<Item>();
}

public class PlayerData
{
    public string name; //플레이어 이름
    public int coin; //플레이어 재화
    public int item; //item에 각 고유번호가 있어 현재 장착중인 장비를 불러오는데 쓰이는 정수값
    public bool[] isChapterUnlock = new bool[10]; //챕터의 잠금여부를 저장하는 배열
    public int chapterIndex; //현재 해금된 최대 챕터
    //환경설정 세팅값 추가예정
    public PlayerInventory inventory = new PlayerInventory(); //갖고 있는 아이템들 정보
}

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();


    public TMP_InputField usernameInputField; // 닉네임을 입력받는 TMP_InputField
    public Button usernameSubmitButton;       // 닉네임 제출 버튼
    public GameObject usernameUI;             // 닉네임 입력 UI 패널
    public GameObject lobbyUI;
    string path;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
        path = Application.persistentDataPath + "/";
    }
    void Start()
    {
        usernameSubmitButton.onClick.AddListener(OnNicknameSubmit);
    }

    // Update is called once per frame

    private void OnNicknameSubmit()
    {
        string username = usernameInputField.text;
        if (!string.IsNullOrEmpty(username))
        {
            CheckOrCreateSaveFile(username);
        }
    }
    private void CheckOrCreateSaveFile(string username)
    {
        string path = Application.persistentDataPath + "/" + username + ".json";

        if (File.Exists(path))
        {
            LoadData(username);
            usernameUI.SetActive(false);
            lobbyUI.SetActive(true);
        }
        else
        {
            Initialize(username);
            lobbyUI.SetActive(true);
        }
    }

    void Initialize(string username) // 플레이어 초기값 생성
    {
        // 플레이어 기본 정보
        nowPlayer.name = username; // 플레이어 이름 초기값
        nowPlayer.coin = 100;       // 초기 코인
        nowPlayer.item = -1;         // 기본 장착 아이템이 없음
        nowPlayer.chapterIndex = 0; // 첫 번째 챕터부터 시작

        // 챕터 잠금 상태 초기화
        for (int i = 0; i < nowPlayer.isChapterUnlock.Length; i++)
        {
            nowPlayer.isChapterUnlock[i] = false; // 모든 챕터 잠금
        }
        nowPlayer.isChapterUnlock[0] = true; // 첫 번째 챕터는 기본 해금 상태

        // 초기 아이템 설정 (해금되지 않은 아이템 목록 생성)
        nowPlayer.inventory.items.Add(new Item { id = 1, name = "체력의 팔찌", isItemUnlock = false, damage = 0, hp = 5 });
        nowPlayer.inventory.items.Add(new Item { id = 2, name = "체력의 모자", isItemUnlock = false, damage = 0, hp = 10 });
        nowPlayer.inventory.items.Add(new Item { id = 3, name = "공격의 팔찌", isItemUnlock = false, damage = 5, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 4, name = "공격의 모자", isItemUnlock = false, damage = 10, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 5, name = "궁극의 모자", isItemUnlock = false, damage = 10, hp = 10 });

        // 데이터를 JSON 파일로 저장
        SaveData(username);

        usernameUI.SetActive(false);
    }

    void SaveData(string username) //로컬에 플레이어 데이터 저장
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + username, data);
    }

    void LoadData(string username) //로컬에 저장된 플레이어 정보 불러오기
    {
        string data = File.ReadAllText(path + username);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

}
