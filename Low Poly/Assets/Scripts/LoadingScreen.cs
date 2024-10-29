using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun.Demo.PunBasics;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;          // ������� ���������
    public TMP_Text progressText;       // �������� ��������
    public Text loadingText;            // ����� �� ����������� ������

    void Start()
    {
        loadingText.text = "�������� � �������� ������ ����������...";
        SaveManager.CheckOrCreateSaveFile();
        SaveManager.LoadPlayerData();

        // ���� ������ �����������, ���������� �� �����������
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName", "")))
        {
            loadingText.text = "��������������� �� �����������...";
            SceneManager.LoadScene("Registrate");
        }
        else
        {
            loadingText.text = "�������� ���������. ������� � ����...";
            SceneManager.LoadScene("lvl_2");
        }

        /// ����� � ��� ������������, �� ���� ����� �� ������� ///
        /*
        // �������� �� ������������� ��������
        if (PlayerPrefs.HasKey("PlayerNickname"))
        {
            // ������� � ��������� ����
            StartCoroutine(LoadSceneAsync("lvl_2"));
        }
        else
        {
            // ������� � ����� ����� ��������
            StartCoroutine(LoadSceneAsync("Registrate"));
        }
        */
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // ��������� ����������� ��������
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // ��������� �������������� ���������, ����� �� ����� ��������� �� �������
        operation.allowSceneActivation = false;

        // ���� ����� �����������, ��������� ��������
        while (!operation.isDone)
        {
            // �������� (����� �� 0.9, ��� ��� ����� ����������� ������ �� 90%)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;  // ��������� ������� ���������
            progressText.text = (progress * 100).ToString("F0") + "%";  // ��������� ��������

            // ����� �������� ������ 90%, ��������� ��������� �����
            if (operation.progress >= 0.9f)
            {
                progressText.text = "������� ����� ������� ��� �����������...";
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
