using UnityEngine;
using TMPro;

public class PlayerProfile : MonoBehaviour
{
    public TextMeshProUGUI nicknameText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timePlayedText;

    private void Start()
    {
        LoadProfile();
    }

    private void LoadProfile()
    {
        // ��������� ������ �� PlayerPrefs
        string nickname = PlayerPrefs.GetString("PlayerNickname", "Guest");
        int kills = PlayerPrefs.GetInt("PlayerKills", 0);
        int deaths = PlayerPrefs.GetInt("PlayerDeaths", 0);
        int level = PlayerPrefs.GetInt("PlayerLevel", 1);
        float timePlayed = PlayerPrefs.GetFloat("PlayerTimePlayed", 0);

        // ��������� UI � �������� ������
        nicknameText.text = nickname;
        killsText.text = "��������: " + kills;
        deathsText.text = "������: " + deaths;
        levelText.text = "" + level;
        timePlayedText.text = $"����� � ����: {Mathf.Floor(timePlayed)}�.";
    }

    public void UpdateTimePlayed(float deltaTime)
    {
        // ��������� ����� � ����
        float timePlayed = PlayerPrefs.GetFloat("PlayerTimePlayed", 0);
        timePlayed += deltaTime;
        PlayerPrefs.SetFloat("PlayerTimePlayed", timePlayed);
    }

    public void IncrementKills()
    {
        int kills = PlayerPrefs.GetInt("PlayerKills", 0) + 1;
        PlayerPrefs.SetInt("PlayerKills", kills);
        killsText.text = "��������: " + kills;
    }

    public void IncrementDeaths()
    {
        int deaths = PlayerPrefs.GetInt("PlayerDeaths", 0) + 1;
        PlayerPrefs.SetInt("PlayerDeaths", deaths);
        deathsText.text = "������: " + deaths;
    }

    public void UpdateLevel(int newLevel)
    {
        PlayerPrefs.SetInt("PlayerLevel", newLevel);
        levelText.text = "�������: " + newLevel;
    }
}
