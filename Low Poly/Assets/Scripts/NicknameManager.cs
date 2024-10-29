using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour
{
    public InputField nicknameInputField; // Поле ввода ника
    private const string NicknameKey = "PlayerNickname";

    void Start()
    {
        // Проверка, установлен ли никнейм
        if (PlayerPrefs.HasKey(NicknameKey))
        {
            // Если ник уже сохранен, переходим в основное меню
            SceneManager.LoadScene("lvl_2");
        }
    }

    // Метод для сохранения ника и перехода в главное меню
    public void SaveNickname()
    {
        string newNickname = nicknameInputField.text;

        // Проверка на пустую строку
        if (!string.IsNullOrEmpty(newNickname))
        {
            PlayerPrefs.SetString(NicknameKey, newNickname);
            PlayerPrefs.Save();
            SceneManager.LoadScene("lvl_2"); // Переход в главное меню после сохранения ника
        }
    }
}





/*
using UnityEngine;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour
{
    public GameObject nicknamePanel;      // Панель для ввода ника
    public InputField nicknameInputField; // Поле ввода ника
    public Text playerNameText;           // Текст для отображения ника игрока в UI

    private const string NicknameKey = "PlayerNickname";

    void Start()
    {
        // Проверка на наличие сохранённого ника
        if (PlayerPrefs.HasKey(NicknameKey))
        {
            string savedNickname = PlayerPrefs.GetString(NicknameKey);
            SetPlayerNickname(savedNickname);
        }
        else
        {
            // Если ника нет, открываем панель ввода
            nicknamePanel.SetActive(true);
        }
    }

    // Метод для сохранения ника
    public void SaveNickname()
    {
        string newNickname = nicknameInputField.text;

        // Проверка на пустую строку
        if (!string.IsNullOrEmpty(newNickname))
        {
            PlayerPrefs.SetString(NicknameKey, newNickname);
            PlayerPrefs.Save();
            SetPlayerNickname(newNickname);
            // Закрываем панель ввода после сохранения
            nicknamePanel.SetActive(false); 
        }
    }

    private void LoadNickname()
    {
        string savedNickname = PlayerPrefs.GetString("PlayerNickname", "Player"); // Ник по умолчанию
        nicknameInputField.text = savedNickname;
        UpdateNicknameDisplay(savedNickname);
    }

    private void UpdateNicknameDisplay(string nickname)
    {
        playerNameText.text = "Ваш ник: " + nickname;
    }

    // Устанавливаем ник игрока
    private void SetPlayerNickname(string nickname)
    {
        playerNameText.text = nickname;
    }
}
*/