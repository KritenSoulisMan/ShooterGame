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
        loadingText.text = "Проверка и создание файлов сохранения...";
        SaveManager.CheckOrCreateSaveFile();
        SaveManager.LoadPlayerData();

        // Скрываем вторую надпись до завершения загрузки
        continueText.gameObject.SetActive(false);

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName", "")))
        {
            loadingText.text = "Перенаправление на регистрацию...";
            StartCoroutine(LoadSceneAsync("Registrate"));
        }
        else
        {
            loadingText.text = "Загрузка завершена. Переход в игру...";
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
            // Определение реального прогресса загрузки, доходящего до 0.9
            targetProgress = operation.progress < 0.9f ? operation.progress : 1f;

            // Плавное и рывковое изменение прогресса
            float speed = Random.Range(1.05f, 4.5f);
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, speed * Time.deltaTime);
            progressBar.value = displayedProgress;
            progressText.text = (displayedProgress * 100).ToString("F0") + "%";

            // Добавление пауз для эффекта рывков
            if (Random.Range(0, 3) == 0)
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

            // Когда загрузка завершена
            if (displayedProgress >= 1f && targetProgress >= 1f)
            {
                // Debug.Log("Брух клавишу нажал?");
                // Отключаем прогресс и включаем текст "Нажмите любую клавишу для продолжения"
                progressText.gameObject.SetActive(false);
                continueText.gameObject.SetActive(true);
                StartCoroutine(BlinkText()); // Запускаем мигание текста

                if (Input.anyKey)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

    // Корутина для мигания текста "Нажмите любую клавишу для продолжения..."
    IEnumerator BlinkText()
    {
        while (true)
        {
            continueText.alpha = Mathf.PingPong(Time.time * 1.5f, 1);
            yield return null;
        }
    }
}
