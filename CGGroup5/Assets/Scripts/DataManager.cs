using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    //ȯ�漳�� ���ð�
    public PlayerInventory inventory = new PlayerInventory(); //���� �ִ� �����۵� ����
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    PlayerData nowPlayer = new PlayerData();

    string path;
    string filename = "save";

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
        if (!System.IO.File.Exists(path+filename))
        {
            Initialize();
        }
        else
        {
            LoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveData() //���ÿ� �÷��̾� ������ ����
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText( path + filename, data );
    }

    void LoadData() //���ÿ� ����� �÷��̾� ���� �ҷ�����
    {
        string data = File.ReadAllText( path + filename );
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
    
    void Initialize() // �÷��̾� �ʱⰪ ����
    {
        // �÷��̾� �⺻ ����
        nowPlayer.name = "Player1"; // �÷��̾� �̸� �ʱⰪ
        nowPlayer.coin = 100;       // �ʱ� ����
        nowPlayer.item = 0;         // �⺻ ���� ������ (������ȣ 0)
        nowPlayer.chapterIndex = 0; // ù ��° é�ͺ��� ����

        // é�� ��� ���� �ʱ�ȭ
        for (int i = 0; i < nowPlayer.isChapterUnlock.Length; i++)
        {
            nowPlayer.isChapterUnlock[i] = false; // ��� é�� ���
        }
        nowPlayer.isChapterUnlock[0] = true; // ù ��° é�ʹ� �⺻ �ر� ����

        // �ʱ� ������ ����
        nowPlayer.inventory.items.Add(new Item { id = 1, name = "ü���� ����", isItemUnlock = false, damage = 0, hp = 5 });
        nowPlayer.inventory.items.Add(new Item { id = 2, name = "ü���� ����", isItemUnlock = false, damage = 0, hp = 10 });
        nowPlayer.inventory.items.Add(new Item { id = 3, name = "������ ����", isItemUnlock = false, damage = 5, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 4, name = "������ ����", isItemUnlock = false, damage = 10, hp = 0 });
        nowPlayer.inventory.items.Add(new Item { id = 5, name = "�ñ��� ����", isItemUnlock = false, damage = 10, hp = 10 });

        // �����͸� JSON ���Ϸ� ����
        SaveData();
    }
    
}
