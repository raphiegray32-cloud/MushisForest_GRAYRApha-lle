using UnityEngine;
using TMPro;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private ScoreDatas scoreData;
    [SerializeField] private UIController uiController; // je vais appeler l'update score text (après avoir maj le score) donc avant la destruction 
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private PlayerEffects playerEffects;
    [SerializeField] private ColorFeedback colorFeedback;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip poisonSound;
    [SerializeField] private AudioClip luminousSound;

    [SerializeField] private TMP_Text luminousCounterText;
    private int totalLuminous;
    private int collectedLuminous;

    private void Awake()
    {
        if (playerEffects == null)
        {
            playerEffects = GetComponent<PlayerEffects>();
        }
        if (colorFeedback == null)
        {
            colorFeedback = GetComponent<ColorFeedback>();
        }
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }


    private void Start()
    {
        Target[] targets = FindObjectsByType<Target>(FindObjectsSortMode.None);

        int luminousInCurrentLevel = 0;
        foreach (Target target in targets)
        {
            if (target.Type == TargetType.Luminous)
            {
                luminousInCurrentLevel++;
            }
        }
        totalLuminous = luminousInCurrentLevel;
        UpdateLuminousCounter();

        if (scoreData != null && uiController  != null)//score de scoredatas et l'affiche dans ui, en gros quand on passe à un autre level on cherche le score du level précédent   
        {
            uiController.UpdateScoreText(scoreData.Score);
        }
    }

    private void UpdateLuminousCounter()
    {
        {
            if (luminousCounterText != null)
            {
                luminousCounterText.text = collectedLuminous + "/" + totalLuminous;
            }
        }
    }

    // si on veut gagner de la vitesse pendant un temps/ une zone qui ralentit le joueur ou fait perdre des points de vie on serait sur du stay ou du exit
    private void OnTriggerEnter(Collider other)
    {
        PlayerLife playerLife = GetComponent<PlayerLife>();

        if (other.CompareTag("LifeMushroom"))
        {
            if (playerLife != null)
            {
                playerLife.CollectLife();
                Destroy(other.gameObject);
            }

            return;
        }
        Target target = other.GetComponentInParent<Target>();

        //Debug.Log("Touch Something");
        if (target == null)
        {
            return;
        }
        // ne pas supp bounce champis 
        if (target.Type == TargetType.Bounce)
        {
            if (playerEffects != null)
            {
                playerEffects.Bounce(target.BounceForce);
            }

            if (audioSource != null && bounceSound != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }

            return;
        }

        if (target.Type == TargetType.Luminous)
        {
            collectedLuminous++;
            UpdateLuminousCounter();
        }

        if (scoreData == null || uiController == null)
        {
            return;
        }

        scoreData.Score = Mathf.Max(0, scoreData.Score + target.ScoreValue); // ici on calcul les scores de sortes à ce que le joueur ne puisse jamais avec un score négatif
        uiController.UpdateScoreText(scoreData.Score);

        switch (target.Type)// suivant le champis on aura tel comportement :
        {
            case TargetType.Luminous:// != null est sensé éviter les erreurs au cas ou un composant manque
                {
                    if(colorFeedback != null)
                    {
                        colorFeedback.StartHighlight();
                    }

                    break;// je les met la car sinon ça peut executer que quand composant est présent 
                   
                }
                

            case TargetType.Poison:
                {
                    if (playerEffects != null)
                    {
                        playerEffects.ApplySlow(target.SlowMultiplier, target.SlowDuration);
                    }

                    break;
                }

            // case TargetType.Bounce:
                //{
                    //if (playerEffects != null)
                   // {
                        //playerEffects.Bounce(target.BounceForce);
                   // }

                    //break;
                //}
        }

        if (audioSource != null)
        {
            //if (target.Type == TargetType.Bounce && bounceSound != null)
            //{
               // audioSource.PlayOneShot(bounceSound);
            //}
            if (target.Type == TargetType.Poison && poisonSound != null)
            {
                audioSource.PlayOneShot(poisonSound);
            }
            else if (target.Type == TargetType.Luminous && luminousSound != null)
            {
                audioSource.PlayOneShot(luminousSound);
            } 
        }

        if (spawnController != null)
        {
            spawnController.OnTargetDestroyed(target);
        }

        Destroy(target.gameObject);//  objet possèdant script target = détruit > détruit préfab du champi et pas un parent 
    } 
}