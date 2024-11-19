using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIDM : MonoBehaviour
{
    // ������ �� ������
    public Gun gun;
    // ������ �� �������� UI
    public TMP_Text ammoText;
    public TMP_Text timerText;

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

        // ������ ����������� �����
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
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
}
