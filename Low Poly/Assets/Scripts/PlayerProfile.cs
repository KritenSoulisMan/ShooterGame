using UnityEngine;
using TMPro;

public class PlayerProfile : MonoBehaviour
{
    public TMP_Text nicknameText;
    public TMP_Text secondNicknameText;
    public TMP_Text killsText;
    public TMP_Text deathsText;
    public TMP_Text levelText;
    public TMP_Text secondLevelText;
    public TMP_Text timePlayedText;

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
        secondNicknameText.text = nickname;
        killsText.text = "��������: " + kills;
        deathsText.text = "������: " + deaths;
        levelText.text = "" + level;
        secondLevelText.text = "" + level;
        timePlayedText.text = $"����� � ����: {Mathf.Floor(timePlayed / 60f * 10f) / 10f}�.";
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
        levelText.text = "" + newLevel;
    }
}
