using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;


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
    public int chapterIndex; //현재 선택한 게임의 난이도
    public int maxChapterIndex; //현재 해금된 최대 챕터
    //환경설정 세팅값 추가예정
    public PlayerInventory inventory = new PlayerInventory(); //갖고 있는 아이템들 정보
}

public class DataManager : MonoBehaviour
{

    public static DataManager instance;
    public Button unlockItemButton; // 해금 버튼
    public PlayerData nowPlayer = new PlayerData();


    public TMP_InputField usernameInputField; // 닉네임을 입력받는 TMP_InputField
    public Button usernameSubmitButton;       // 닉네임 제출 버튼
    public GameObject usernameUI;             // 닉네임 입력 UI 패널
    public GameObject lobbyUI;
    public GameObject selectUI;
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
        unlockItemButton.onClick.AddListener(UnlockAndEquipRandomItem);
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

    void Initialize(string username)
    {
        nowPlayer.name = username;
        nowPlayer.coin = 5000;
        nowPlayer.item = 0;
        nowPlayer.chapterIndex = 0;
        nowPlayer.maxChapterIndex = 0;

        for (int i = 0; i < nowPlayer.isChapterUnlock.Length; i++)
        {
            nowPlayer.isChapterUnlock[i] = false;
        }
        nowPlayer.isChapterUnlock[0] = true; //튜토리얼 언락
        nowPlayer.isChapterUnlock[1] = true; //1스테이지 언락

        InitializeItems();
        SaveData(username);

        usernameUI.SetActive(false);
    }

    void InitializeItems()
    {
        nowPlayer.inventory.items.Add(new Item { id = 0, name = "기본 장비", isItemUnlock = true, damage = 0, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 1, name = "체력의 팔찌", isItemUnlock = false, damage = 0, hp = 20 });
        nowPlayer.inventory.items.Add(new Item { id = 2, name = "체력의 모자", isItemUnlock = false, damage = 0, hp = 40 });
        nowPlayer.inventory.items.Add(new Item { id = 3, name = "공격의 팔찌", isItemUnlock = false, damage = 20, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 4, name = "공격의 모자", isItemUnlock = false, damage = 40, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 5, name = "궁극의 모자", isItemUnlock = false, damage = 30, hp = 30 });
    }

    public void SaveData(string username) //로컬에 플레이어 데이터 저장
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + username, data);
    }

    public void LoadData(string username) //로컬에 저장된 플레이어 정보 불러오기
    {
        string data = File.ReadAllText(path + username);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void ClearLobbyUI()
    {
        if (lobbyUI != null)
        {
            Destroy(lobbyUI);
            lobbyUI = null;
        }
        if (selectUI != null)
        {
            Destroy(selectUI);
            selectUI = null;
        }
    }

    public void UnlockAndEquipRandomItem()
    {
        // 해금되지 않은 아이템 필터링
        List<Item> lockedItems = nowPlayer.inventory.items.FindAll(item => !item.isItemUnlock);

        if (lockedItems.Count > 0)
        {
            // 무작위 아이템 선택
            int randomIndex = Random.Range(0, lockedItems.Count);
            Item selectedItem = lockedItems[randomIndex];

            // 아이템 해금
            selectedItem.isItemUnlock = true;
            Debug.Log($"아이템 '{selectedItem.name}'이(가) 해금되었습니다!");

            // 아이템 장착
            nowPlayer.item = selectedItem.id;
            Debug.Log($"아이템 '{selectedItem.name}'이(가) 장착되었습니다!");

            // 데이터 저장
            SaveData(nowPlayer.name);

        }
        else
        {
            Debug.Log("해금할 아이템이 없습니다!");
        }
    }

}
