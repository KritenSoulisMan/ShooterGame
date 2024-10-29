using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour
{
    public InputField nicknameInputField; // ���� ����� ����
    private const string NicknameKey = "PlayerNickname";

    void Start()
    {
        // ��������, ���������� �� �������
        if (PlayerPrefs.HasKey(NicknameKey))
        {
            // ���� ��� ��� ��������, ��������� � �������� ����
            SceneManager.LoadScene("lvl_2");
        }
    }

    // ����� ��� ���������� ���� � �������� � ������� ����
    public void SaveNickname()
    {
        string newNickname = nicknameInputField.text;

        // �������� �� ������ ������
        if (!string.IsNullOrEmpty(newNickname))
        {
            PlayerPrefs.SetString(NicknameKey, newNickname);
            PlayerPrefs.Save();
            SceneManager.LoadScene("lvl_2"); // ������� � ������� ���� ����� ���������� ����
        }
    }
}





/*
using UnityEngine;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour
{
    public GameObject nicknamePanel;      // ������ ��� ����� ����
    public InputField nicknameInputField; // ���� ����� ����
    public Text playerNameText;           // ����� ��� ����������� ���� ������ � UI

    private const string NicknameKey = "PlayerNickname";

    void Start()
    {
        // �������� �� ������� ����������� ����
        if (PlayerPrefs.HasKey(NicknameKey))
        {
            string savedNickname = PlayerPrefs.GetString(NicknameKey);
            SetPlayerNickname(savedNickname);
        }
        else
        {
            // ���� ���� ���, ��������� ������ �����
            nicknamePanel.SetActive(true);
        }
    }

    // ����� ��� ���������� ����
    public void SaveNickname()
    {
        string newNickname = nicknameInputField.text;

        // �������� �� ������ ������
        if (!string.IsNullOrEmpty(newNickname))
        {
            PlayerPrefs.SetString(NicknameKey, newNickname);
            PlayerPrefs.Save();
            SetPlayerNickname(newNickname);
            // ��������� ������ ����� ����� ����������
            nicknamePanel.SetActive(false); 
        }
    }

    private void LoadNickname()
    {
        string savedNickname = PlayerPrefs.GetString("PlayerNickname", "Player"); // ��� �� ���������
        nicknameInputField.text = savedNickname;
        UpdateNicknameDisplay(savedNickname);
    }

    private void UpdateNicknameDisplay(string nickname)
    {
        playerNameText.text = "��� ���: " + nickname;
    }

    // ������������� ��� ������
    private void SetPlayerNickname(string nickname)
    {
        playerNameText.text = nickname;
    }
}
*/