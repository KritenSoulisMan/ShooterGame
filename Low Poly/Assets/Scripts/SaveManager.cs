using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NameGame", "playerData.dat");
    private static string saveDirectory = Path.GetDirectoryName(savePath);

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

        // ��������� ������ � ����
        File.WriteAllText(savePath, data);
        Debug.Log("������ ������ ������� ���������.");
    }

    // �������� ������ ������
    public static void LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            string data = File.ReadAllText(savePath);

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
}
