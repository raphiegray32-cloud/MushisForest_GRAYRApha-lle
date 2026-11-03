using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenusNavigation : MonoBehaviour//btns des menus (next lvl, retry, return2menu)
{
    [Header("Scene à charger")]
    [SerializeField] private string newLevelSceneName = "Lvl_1";

    [Header("Button Audio")]
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private float soundDelay = 0.2f;

    public void StartGame()//sound design click btns 
    {
        LoadSceneWithSound("Lvl_Tuto");
    }

    public void RestartGame()
    {
        LoadSceneWithSound(SceneManager.GetActiveScene().name);
    }

    public void LoadNewLevel()
    {
        LoadSceneWithSound(newLevelSceneName);
    }

    public void ReturnToMenu()
    {
        LoadSceneWithSound("MenuPrincipal");
    }

    public void QuitGame()
    {
        StartCoroutine(QuitAfterSound());
    }

    private void LoadSceneWithSound(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        PlayButtonSound();

        yield return new WaitForSecondsRealtime(soundDelay);

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator QuitAfterSound()
    {
        PlayButtonSound();

        yield return new WaitForSecondsRealtime(soundDelay);

        Application.Quit();
    }

    private void PlayButtonSound()
    {
        if (buttonAudioSource != null && buttonSound != null)
        {
            buttonAudioSource.PlayOneShot(buttonSound);
        }
    }
}