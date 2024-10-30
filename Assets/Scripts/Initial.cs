using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialL : MonoBehaviour
{
    public TMP_InputField usernameInputField; // �г����� �Է¹޴� TMP_InputField
    public Button usernameSubmitButton;       // �г��� ���� ��ư
    public GameObject usernameUI;             // �г��� �Է� UI �г�

    void Start()
    {
        usernameSubmitButton.onClick.AddListener(OnNicknameSubmit);
    }


    private void OnNicknameSubmit()
    {
        string nickname = usernameInputField.text;
        if (!string.IsNullOrEmpty(nickname))
        {
            CheckOrCreateSaveFile(nickname);
        }
    }

    private void CheckOrCreateSaveFile(string nickname)
    {
        string path = Application.persistentDataPath + "/" + nickname + ".json";

        if (File.Exists(path))
        {
            LoadSaveData(path);
            SceneManager.LoadScene("DifficultySelection"); // ���� ���̵� ���� ȭ������ �̵�
        }
        else
        {
            CreateSaveFile(nickname, path);
            SceneManager.LoadScene("DifficultySelection"); // ���� ���̵� ���� ȭ������ �̵�
        }
    }

    private void LoadSaveData(string path)
    {
        string jsonData = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
        // ���� �����͸� ���� ���� �ε�
    }

    private void CreateSaveFile(string nickname, string path)
    {
        SaveData newData = new SaveData
        {
            Nickname = nickname,
            // �ʿ��� �߰� ������ ���⿡ �߰�
        };
        string jsonData = JsonUtility.ToJson(newData);
        File.WriteAllText(path, jsonData);
    }

    [System.Serializable]
    public class SaveData
    {
        public string Nickname;
        // �߰� ������ �ʵ带 ���⿡ �߰�
    }
}
