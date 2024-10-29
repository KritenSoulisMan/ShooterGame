using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NameGame", "playerData.dat");
    private static string saveDirectory = Path.GetDirectoryName(savePath);
    private static string encryptionKey = "YourEncryptionKey123"; // Выберите надёжный ключ

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

        // Шифруем данные и сохраняем в файл
        string encryptedData = EncryptData(data);
        File.WriteAllText(savePath, encryptedData);
        Debug.Log("Данные игрока успешно сохранены.");
    }

    // Загрузка данных игрока
    public static void LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            string encryptedData = File.ReadAllText(savePath);
            string data = DecryptData(encryptedData);

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

    // Шифрование данных
    private static string EncryptData(string data)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        byte[] iv = new byte[16];
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = iv;
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataBytes, 0, dataBytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    // Дешифрование данных
    private static string DecryptData(string encryptedData)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
        byte[] iv = new byte[16];
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = iv;
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
