using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIDM : MonoBehaviour
{
    // Ссылка на оружее
    public Gun gun;
    // Ссылки на элементы UI
    public TMP_Text ammoText;
    public TMP_Text timerText;

    // Таймер
    public float startTime = 3601f; // Время указано в секундах
    private float currentTime;

    // Ссылка на скрипт Health
    private Health playerHealth;

    void Start()
    {
        // Инициализация таймера с стартовым значением
        currentTime = startTime;

        // Получаем компонент Health у игрока
        playerHealth = GetComponent<Health>();

        // Обновляем UI таймера при старте игры
        UpdateTimerUI();
    }

    void Update()
    {
        // Обновляем отображение патронов
        ammoText.text = gun.currentAmmo + " / " + gun.totalAmmo;

        // Таймер отсчитывает время
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            currentTime = 0; // Убеждаемся, что таймер не уходит в отрицательное значение
        }

        UpdateTimerUI();
    }

    // Метод для обновления таймера на UI
    public void UpdateTimerUI()
    {
        // Преобразуем время в формат Минуты:Секунды
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
