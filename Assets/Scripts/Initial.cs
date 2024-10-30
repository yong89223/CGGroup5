using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialL : MonoBehaviour
{
    public TMP_InputField usernameInputField; // 닉네임을 입력받는 TMP_InputField
    public Button usernameSubmitButton;       // 닉네임 제출 버튼
    public GameObject usernameUI;             // 닉네임 입력 UI 패널

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
            SceneManager.LoadScene("DifficultySelection"); // 게임 난이도 선택 화면으로 이동
        }
        else
        {
            CreateSaveFile(nickname, path);
            SceneManager.LoadScene("DifficultySelection"); // 게임 난이도 선택 화면으로 이동
        }
    }

    private void LoadSaveData(string path)
    {
        string jsonData = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
        // 유저 데이터를 게임 내에 로드
    }

    private void CreateSaveFile(string nickname, string path)
    {
        SaveData newData = new SaveData
        {
            Nickname = nickname,
            // 필요한 추가 정보도 여기에 추가
        };
        string jsonData = JsonUtility.ToJson(newData);
        File.WriteAllText(path, jsonData);
    }

    [System.Serializable]
    public class SaveData
    {
        public string Nickname;
        // 추가 저장할 필드를 여기에 추가
    }
}
