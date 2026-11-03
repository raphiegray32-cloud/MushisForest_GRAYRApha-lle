using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour// afficher ou masquer menus 
{
    [SerializeField] private TMP_Text scoreTest;
    [SerializeField] private ScoreDatas scoreData;

    [Header("Menu")]
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private GameObject victoryPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource levelMusicSource;
    [SerializeField] private AudioSource menuAudioSource;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;

    private void Start()
    {
        if (scoreData != null)
        {
            UpdateScoreText(scoreData.Score);
        }

        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false);
        }

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // musique des niveaux
        if (levelMusicSource != null)
        {
            levelMusicSource.loop = true;
            levelMusicSource.Play();
        }
    }

    public void UpdateScoreText(int scoreValue)
    {
        if (scoreTest != null)
        {
            scoreTest.text = scoreValue.ToString();
        }
    }

    public void ShowDefeat()//wbp defeat panel
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }

        StopLevelMusic();//on arrête la musique du niveau comme ça on peut lancer celle du defeat panel

        if (menuAudioSource != null && defeatSound != null)
        {
            menuAudioSource.PlayOneShot(defeatSound);
        }

        Time.timeScale = 0f;
    }

    public void ShowVictory()//wbp victory panel
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        StopLevelMusic();// same que pour wbp defeat panel

        if (menuAudioSource != null && victorySound != null)
        {
            menuAudioSource.PlayOneShot(victorySound);
        }

        Time.timeScale = 0f;
    }

    private void StopLevelMusic()
    {
        if (levelMusicSource != null)
        {
            levelMusicSource.Stop();
        }
    }
}