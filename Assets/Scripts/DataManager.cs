using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Item
{
    public int id; //�������� �ĺ���
    public string name; //�������� �̸�
    public bool isItemUnlock; //�������� �ر�����
    public int damage; //�������� ������ų ������
    public int hp; //�������� ������ų ü��
}

public class PlayerInventory
{
    public List<Item> items = new List<Item>();
}

public class PlayerData
{
    public string name; //�÷��̾� �̸�
    public int coin; //�÷��̾� ��ȭ
    public int item; //item�� �� ������ȣ�� �־� ���� �������� ��� �ҷ����µ� ���̴� ������
    public bool[] isChapterUnlock = new bool[10]; //é���� ��ݿ��θ� �����ϴ� �迭
    public int chapterIndex; //���� �رݵ� �ִ� é��
    //ȯ�漳�� ���ð� �߰�����
    public PlayerInventory inventory = new PlayerInventory(); //���� �ִ� �����۵� ����
}

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();


    public TMP_InputField usernameInputField; // �г����� �Է¹޴� TMP_InputField
    public Button usernameSubmitButton;       // �г��� ���� ��ư
    public GameObject usernameUI;             // �г��� �Է� UI �г�
    public GameObject lobbyUI;
    string path;

    private void Awake()
    {
        #region �̱���
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

    void Initialize(string username) // �÷��̾� �ʱⰪ ����
    {
        // �÷��̾� �⺻ ����
        nowPlayer.name = username; // �÷��̾� �̸� �ʱⰪ
        nowPlayer.coin = 100;       // �ʱ� ����
        nowPlayer.item = -1;         // �⺻ ���� �������� ����
        nowPlayer.chapterIndex = 0; // ù ��° é�ͺ��� ����

        // é�� ��� ���� �ʱ�ȭ
        for (int i = 0; i < nowPlayer.isChapterUnlock.Length; i++)
        {
            nowPlayer.isChapterUnlock[i] = false; // ��� é�� ���
        }
        nowPlayer.isChapterUnlock[0] = true; // ù ��° é�ʹ� �⺻ �ر� ����

        // �ʱ� ������ ���� (�رݵ��� ���� ������ ��� ����)
        nowPlayer.inventory.items.Add(new Item { id = 1, name = "ü���� ����", isItemUnlock = false, damage = 0, hp = 5 });
        nowPlayer.inventory.items.Add(new Item { id = 2, name = "ü���� ����", isItemUnlock = false, damage = 0, hp = 10 });
        nowPlayer.inventory.items.Add(new Item { id = 3, name = "������ ����", isItemUnlock = false, damage = 5, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 4, name = "������ ����", isItemUnlock = false, damage = 10, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 5, name = "�ñ��� ����", isItemUnlock = false, damage = 10, hp = 10 });

        // �����͸� JSON ���Ϸ� ����
        SaveData(username);

        usernameUI.SetActive(false);
    }

    void SaveData(string username) //���ÿ� �÷��̾� ������ ����
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + username, data);
    }

    void LoadData(string username) //���ÿ� ����� �÷��̾� ���� �ҷ�����
    {
        string data = File.ReadAllText(path + username);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

}
