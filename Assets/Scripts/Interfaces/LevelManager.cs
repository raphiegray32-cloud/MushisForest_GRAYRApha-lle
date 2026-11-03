using UnityEngine;
using UnityEngine.SceneManagement;
//pas oublier de créer un empty dans lequel mettre ce script sur la scene de départ si pas objet physique mais juste une touche appuyée

public class LevelManager : MonoBehaviour// pour passer au niveau prochain + afficher menu victoire 
{

    [Header("level")]
    [SerializeField] private string nextSceneName = "Lvl_2";
    [SerializeField] private bool isFinalLevel = false;
    

    [Header("UI")]
    [SerializeField] private UIController uiController;

    private bool hasTransitioned = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponentInParent<PlayerMovement>();

        if (player == null || hasTransitioned)
        {
            return;
        }

        hasTransitioned = true;

        if (isFinalLevel)
        {
            if (uiController != null)
            {
                uiController.ShowVictory();
            }

            return;
        }
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);// ici on indique la scène directement dans l'inspector 
        }

         else
        {
            //Debug.LogError("Le nom de la scène suivante po renseigné !");
        }
    }
}
