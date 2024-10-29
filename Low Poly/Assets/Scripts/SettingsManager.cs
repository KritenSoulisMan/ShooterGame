using Photon.Chat.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    [Header("Чувствительность мыши")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;

    [Header("Режим окна")]
    public TMP_Dropdown screenModeDropdown;

    void Start()
    {
        LoadSettings();
    }

    public void OnSensitivityChanged()
    {
        float sensitivity = sensitivitySlider.value;
        sensitivityValueText.text = sensitivity.ToString("F2");
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void OnScreenModeChanged()
    {
        int selectedMode = screenModeDropdown.value;
        FullScreenMode screenMode = FullScreenMode.Windowed;

        switch (selectedMode)
        {
            case 0:
                screenMode = FullScreenMode.Windowed;
                break;
            case 1:
                screenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 2:
                screenMode = FullScreenMode.FullScreenWindow;
                break;
        }

        Screen.fullScreenMode = screenMode;
        PlayerPrefs.SetInt("ScreenMode", selectedMode);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        /// Загрузка чувствительности мыши ///
        /// 
        /// Значение по умолчанию - 1
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        sensitivitySlider.value = savedSensitivity;
        sensitivityValueText.text = savedSensitivity.ToString("F2");


        /// Загрузка режима экрана ///
        /// 
        /// Оконный режим по умолчанию
        int savedScreenMode = PlayerPrefs.GetInt("screenMode", 0);
        screenModeDropdown.value = savedScreenMode;
        /// Применение настроек режима экрана
        OnScreenModeChanged();
    }
}
