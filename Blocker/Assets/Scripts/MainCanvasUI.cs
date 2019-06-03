using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCanvasUI : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject bestResult;

    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
        ShowBestResult();
        volumeSlider.value = PlayerPrefsController.GetVolume();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SettingsPanelOpen()
    {
        AudioManager.PlayMouseSound();
        settingsPanel.SetActive(true);
        volumeSlider.value = PlayerPrefsController.GetVolume();
    }

    public void Close()
    {
        AudioManager.PlayMouseSound();
        settingsPanel.SetActive(false);
    }

    private void ShowBestResult()
    {
        TextMeshProUGUI bestResultText = bestResult.GetComponent<TextMeshProUGUI>();
        bestResultText.text = PlayerPrefsController.GetBestScore().ToString();
    }

    public void DefaultScore()
    {
        AudioManager.PlayMouseSound();
        PlayerPrefsController.SetBestScore(0);
        ShowBestResult();

    }
}
