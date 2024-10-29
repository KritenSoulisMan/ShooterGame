using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun.Demo.PunBasics;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;          // Полоска прогресса
    public TMP_Text progressText;       // Проценты загрузки
    public Text loadingText;            // Текст на загрузачном экране

    void Start()
    {
        loadingText.text = "Проверка и создание файлов сохранения...";
        SaveManager.CheckOrCreateSaveFile();
        SaveManager.LoadPlayerData();

        // Если данные отсутствуют, отправляем на регистрацию
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName", "")))
        {
            loadingText.text = "Перенаправление на регистрацию...";
            SceneManager.LoadScene("Registrate");
        }
        else
        {
            loadingText.text = "Загрузка завершена. Переход в игру...";
            SceneManager.LoadScene("lvl_2");
        }

        /// Можно и его использовать, но выше будет по надёжней ///
        /*
        // Проверка на существование никнейма
        if (PlayerPrefs.HasKey("PlayerNickname"))
        {
            // Переход к основному меню
            StartCoroutine(LoadSceneAsync("lvl_2"));
        }
        else
        {
            // Переход к сцене ввода никнейма
            StartCoroutine(LoadSceneAsync("Registrate"));
        }
        */
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Запускаем асинхронную загрузку
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Отключаем автоматическую активацию, чтобы мы могли управлять ею вручную
        operation.allowSceneActivation = false;

        // Пока сцена загружается, обновляем прогресс
        while (!operation.isDone)
        {
            // Прогресс (делим на 0.9, так как сцена загружается только до 90%)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;  // Обновляем полоску прогресса
            progressText.text = (progress * 100).ToString("F0") + "%";  // Обновляем проценты

            // Когда прогресс достиг 90%, разрешаем активацию сцены
            if (operation.progress >= 0.9f)
            {
                progressText.text = "Нажмите любую клавишу для продолжения...";
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
