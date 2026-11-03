using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour//qu'est ce qu'on fait suivant qui 'meurt' = si player on réinitialise le niv + score mais si c'est mob = détruit 
{
    [SerializeField] private bool resetScoreOnDeath = false; //variable pour savoir si la mort = réinitialiser score/ relancer niveau (instance editable)
    [SerializeField] private ScoreDatas scoreData;//vu que quand on meurt on réinitialise score on doit faire appel à lui 
    [SerializeField] private UIController uiController;//si le score change on doit faire appel à l'ui 
    [SerializeField] private AudioClip deathSound; 

    private bool isDead = false;// pour pas que plusieurs morts soit possibles en même temps sur le même objet 

    public void Die()
    {
        if (isDead)
        {
            return;// si déjà mort on arrête cette fonction 
        }
        isDead = true;// sinon bah isdead = true 
        AudioSource audioSource = GetComponent<AudioSource>();// si joueur meurt = son se lance 
        if (deathSound != null)
        {
           AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        if (resetScoreOnDeath)//si il meurt 
        {

            if (scoreData != null)//là juste faut vérifier que scoredata existe avant de l'utiliser 
            {
                scoreData.Score = 0;//on remet score à 0
            }
            PlayerLife playerLife = GetComponent<PlayerLife>();

            if (playerLife != null)
            {
                playerLife.ResetLifeAfterDeath();
            }

            if (uiController != null)
            {
                uiController.UpdateScoreText(0);
                uiController.ShowDefeat();
            }

            return;

        }
        Destroy(gameObject);// ça ne marche que si dans inspector resetscore est désactivée > détruit l'objet relié à deathmanager (mob quoi)
    }

}
