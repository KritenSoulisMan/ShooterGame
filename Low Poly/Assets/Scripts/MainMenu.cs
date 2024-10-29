using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public GameObject _settings;
    public GameObject _profile;

    void Start()
    {
        _profile.SetActive(false);
        _settings.SetActive(false);
    }

    public void StartGame()
    {
        // Загружаем сцену с настройкой сервера
        SceneManager.LoadScene("ServerSettings");
    }

    public void OpenProfile()
    {
        _profile.SetActive(true);
        _settings.SetActive(false);
    }

    public void OpenSettings()
    {
        _profile.SetActive(false);
        _settings.SetActive(true);
    }

    public void Discord()
    {
        Application.OpenURL("https://discord.gg/Vc2khCFrth");
    }

    public void Telegram()
    {
        Application.OpenURL("https://t.me/KritenGitHub");
    }

    public void CloseWindow()
    {
        _profile.SetActive(false);
        _settings.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
