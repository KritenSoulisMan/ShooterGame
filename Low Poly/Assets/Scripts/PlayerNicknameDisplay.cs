using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class PlayerNicknameDisplay : MonoBehaviour
{
    // Текст для отображения никнейма
    public TextMeshProUGUI nicknameText;
    public Transform canvasTransform;

    private string playerNickname;

    private void Start()
    {
        LoadNickname();
        nicknameText.text = playerNickname;
    }

    void LateUpdate()
    {
        // Ник персонажа всегда повёрнут в сторону камеры
        canvasTransform.LookAt(Camera.main.transform);
        // Разворот canvas на 180 градусов
        canvasTransform.Rotate(0, 180, 0);
    }

    private void LoadNickname()
    {
        // Получение ника из PlayerPrefs
        playerNickname = PlayerPrefs.GetString("PlayerNickname", "Player");
    }
}
