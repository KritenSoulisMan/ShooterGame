using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public TMP_Text progressText;
    public TMP_Text loadingText;
    public TMP_Text continueText;

    private void Start()
    {
        loadingText.text = "�������� � �������� ������ ����������...";
        SaveManager.CheckOrCreateSaveFile();
        SaveManager.LoadPlayerData();

        // �������� ������ ������� �� ���������� ��������
        continueText.gameObject.SetActive(false);

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName", "")))
        {
            loadingText.text = "��������������� �� �����������...";
            StartCoroutine(LoadSceneAsync("Registrate"));
        }
        else
        {
            loadingText.text = "�������� ���������. ������� � ����...";
            StartCoroutine(LoadSceneAsync("lvl_2"));
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float targetProgress = 0f;
        float displayedProgress = 0f;

        while (!operation.isDone)
        {
            // ����������� ��������� ��������� ��������, ���������� �� 0.9
            targetProgress = operation.progress < 0.9f ? operation.progress : 1f;

            // ������� � �������� ��������� ���������
            float speed = Random.Range(1.05f, 4.5f);
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, speed * Time.deltaTime);
            progressBar.value = displayedProgress;
            progressText.text = (displayedProgress * 100).ToString("F0") + "%";

            // ���������� ���� ��� ������� ������
            if (Random.Range(0, 3) == 0)
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

            // ����� �������� ���������
            if (displayedProgress >= 1f && targetProgress >= 1f)
            {
                // Debug.Log("���� ������� �����?");
                // ��������� �������� � �������� ����� "������� ����� ������� ��� �����������"
                progressText.gameObject.SetActive(false);
                continueText.gameObject.SetActive(true);
                StartCoroutine(BlinkText()); // ��������� ������� ������

                if (Input.anyKey)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    // �������� ��� ������� ������ "������� ����� ������� ��� �����������..."
    IEnumerator BlinkText()
    {
        while (true)
        {
            continueText.alpha = Mathf.PingPong(Time.time * 1.5f, 1);
            yield return null;
        }
    }
}
