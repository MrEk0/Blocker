using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public static UIManager instanse=null;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI resultScoreText;
    [SerializeField] Sprite deathSprite;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pauseMenuPanel;
    [SerializeField] GameObject optionsMenuPanel;
    [SerializeField] GameObject pauseButton;

    private int currentScore = 0;
    private int scorePoints = 50;
    private List<GameObject> lifes;

    public Slider volumeSlider;
    //GameObject volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (instanse == null)
        {
            instanse = this;
        }
        else if (instanse != this)
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1;
        //AudioManager.instance.StateVolumeSound();
        volumeSlider.value = PlayerPrefsController.GetVolume();

        scoreText.text = currentScore.ToString();
        gameOverPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);
        GetLifePictures();
    }

    private void GetLifePictures()
    {
        lifes = new List<GameObject>();
        Transform UItransform = gameObject.transform;
        foreach (Transform child in UItransform)
        {
            if (child.gameObject.CompareTag("PlayerLife"))
            {
                lifes.Add(child.gameObject);
            }
        }
    }

    public void Score()
    {
        currentScore += scorePoints;
        scoreText.text = currentScore.ToString();
        PlayerPrefsController.SetBestScore(currentScore);
    }

    public void ReplaceLifePicture()
    {
        int lastLife = lifes.Count-1;
        GameObject lifePicture = lifes[lifes.Count - 1];
        lifePicture.GetComponent<Image>().sprite = deathSprite;
        lifes.RemoveAt(lastLife);
        if(lifes.Count==0)
        {
            ShowGameOverPanel();
            resultScoreText.text = currentScore.ToString();
            if (currentScore > PlayerPrefsController.GetBestScore())
            {
                PlayerPrefsController.SetBestScore(currentScore);
            }
            GameManager.instance.PlayerDeath();
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void Pause()
    {
        AudioManager.PlayMouseSound();
        pauseMenuPanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        AudioManager.PlayMouseSound();
        pauseMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Options()
    {
        AudioManager.PlayMouseSound();
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        AudioManager.PlayMouseSound();
        optionsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
