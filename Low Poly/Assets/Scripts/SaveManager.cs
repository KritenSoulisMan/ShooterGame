using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NameGame", "playerData.dat");
    private static string saveDirectory = Path.GetDirectoryName(savePath);

    // Проверка наличия файла сохранения
    public static void CheckOrCreateSaveFile()
    {
        // Создаём директорию, если её нет
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
            Debug.Log("Создание папки для сохранений...");
        }

        // Создаём файл, если его нет
        if (!File.Exists(savePath))
        {
            Debug.Log("Файл сохранения не найден. Создание нового...");
            SavePlayerData(); // Создаём новый файл с начальными данными
        }
        else
        {
            Debug.Log("Файл сохранения найден.");
        }
    }

    // Сохранение данных игрока
    public static void SavePlayerData()
    {
        // Данные игрока из PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        int playerKills = PlayerPrefs.GetInt("PlayerKills", 0);
        int playerDeaths = PlayerPrefs.GetInt("PlayerDeaths", 0);

        // Объединяем данные в строку для сохранения
        string data = $"{playerName}|{playerKills}|{playerDeaths}";

        // Сохраняем данные в файл
        File.WriteAllText(savePath, data);
        Debug.Log("Данные игрока успешно сохранены.");
    }

    // Загрузка данных игрока
    public static void LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            string data = File.ReadAllText(savePath);

            // Разделяем данные и сохраняем в PlayerPrefs
            string[] parts = data.Split('|');
            if (parts.Length >= 3)
            {
                PlayerPrefs.SetString("PlayerName", parts[0]);
                PlayerPrefs.SetInt("PlayerKills", int.Parse(parts[1]));
                PlayerPrefs.SetInt("PlayerDeaths", int.Parse(parts[2]));
            }
            Debug.Log("Данные игрока успешно загружены.");
        }
        else
        {
            Debug.LogWarning("Файл сохранения не найден.");
        }
    }
}
