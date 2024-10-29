using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // ������ �� ������
    public Gun gun;
    // ������ �� �������� UI
    public TMP_Text ammoText;
    public TMP_Text redTeamScoreText;
    public TMP_Text blueTeamScoreText;
    public TMP_Text timerText;

    // ���� ������
    public int redTeamScore = 999;
    public int blueTeamScore = 999;

    // ������
    public float startTime = 3601f; // ����� ������� � ��������
    private float currentTime;

    // ������ �� ������ Health
    private Health playerHealth;

    void Start()
    {
        // ������������� ������� � ��������� ���������
        currentTime = startTime;

        // �������� ��������� Health � ������
        playerHealth = GetComponent<Health>();

        // ��������� UI ������� ��� ������ ����
        UpdateTimerUI();
    }

    void Update()
    {
        // ��������� ����������� ��������
        ammoText.text = gun.currentAmmo + " / " + gun.totalAmmo;

        // ��������� ����������� ����� ������
        redTeamScoreText.text = redTeamScore.ToString();
        blueTeamScoreText.text = blueTeamScore.ToString();

        // ������ ����������� �����
        currentTime -= Time.deltaTime;
        if ( currentTime < 0)
        {
            currentTime = 0; // ����������, ��� ������ �� ������ � ������������� ��������
        }

        UpdateTimerUI();
    }

    // ����� ��� ���������� ������� �� UI
    public void UpdateTimerUI()
    {
        // ����������� ����� � ������ ������:�������
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // ������ ��� ���������� �����
    public void AddRedTeamScore(int points)
    {
        redTeamScore += points;
    }

    public void AddBlueTeamScore(int points)
    {
        blueTeamScore += points;
    }
}
