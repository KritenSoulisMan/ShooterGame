using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class PlayerNicknameDisplay : MonoBehaviour
{
    // ����� ��� ����������� ��������
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
        // ��� ��������� ������ ������� � ������� ������
        canvasTransform.LookAt(Camera.main.transform);
        // �������� canvas �� 180 ��������
        canvasTransform.Rotate(0, 180, 0);
    }

    private void LoadNickname()
    {
        // ��������� ���� �� PlayerPrefs
        playerNickname = PlayerPrefs.GetString("PlayerNickname", "Player");
    }
}
