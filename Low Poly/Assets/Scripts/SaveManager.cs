using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NameGame", "playerData.dat");
    private static string saveDirectory = Path.GetDirectoryName(savePath);
    private static string encryptionKey = "YourEncryptionKey123"; // �������� ������� ����

    // �������� ������� ����� ����������
    public static void CheckOrCreateSaveFile()
    {
        // ������ ����������, ���� � ���
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
            Debug.Log("�������� ����� ��� ����������...");
        }

        // ������ ����, ���� ��� ���
        if (!File.Exists(savePath))
        {
            Debug.Log("���� ���������� �� ������. �������� ������...");
            SavePlayerData(); // ������ ����� ���� � ���������� �������
        }
        else
        {
            Debug.Log("���� ���������� ������.");
        }
    }

    // ���������� ������ ������
    public static void SavePlayerData()
    {
        // ������ ������ �� PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        int playerKills = PlayerPrefs.GetInt("PlayerKills", 0);
        int playerDeaths = PlayerPrefs.GetInt("PlayerDeaths", 0);

        // ���������� ������ � ������ ��� ����������
        string data = $"{playerName}|{playerKills}|{playerDeaths}";

        // ������� ������ � ��������� � ����
        string encryptedData = EncryptData(data);
        File.WriteAllText(savePath, encryptedData);
        Debug.Log("������ ������ ������� ���������.");
    }

    // �������� ������ ������
    public static void LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            string encryptedData = File.ReadAllText(savePath);
            string data = DecryptData(encryptedData);

            // ��������� ������ � ��������� � PlayerPrefs
            string[] parts = data.Split('|');
            if (parts.Length >= 3)
            {
                PlayerPrefs.SetString("PlayerName", parts[0]);
                PlayerPrefs.SetInt("PlayerKills", int.Parse(parts[1]));
                PlayerPrefs.SetInt("PlayerDeaths", int.Parse(parts[2]));
            }
            Debug.Log("������ ������ ������� ���������.");
        }
        else
        {
            Debug.LogWarning("���� ���������� �� ������.");
        }
    }

    // ���������� ������
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

    // ������������ ������
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
