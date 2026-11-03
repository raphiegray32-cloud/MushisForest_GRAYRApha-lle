using UnityEngine;

public class MobCollision : MonoBehaviour
{

    [SerializeField] private DeathManager mobDeathManager;
    [SerializeField] private Collider mobCollider;

    private void Awake()
    {
        if (mobDeathManager == null)//on recup composant si pas assigné 
        {
            mobDeathManager = GetComponent<DeathManager>();
        }
        if (mobCollider == null)
        {
            mobCollider = GetComponent<Collider>();
        }

    }

    private void OnCollisionEnter(Collision collision)//marche si objets rencontrés possèdent collider non déclencheurs + rb  parmis objets en collision 
    {
        PlayerLife playerLife =
            collision.collider.GetComponentInParent<PlayerLife>();

        if (playerLife == null)
        {
            return;
        }

        Debug.Log("Collision avec : " + collision.gameObject.name);// bug mob 


        DeathManager playerDeathManager = playerLife.GetComponent<DeathManager>(); //on cherche un deathmanager sur objet touché

        if (playerDeathManager == null)
        { 
            return;// si y'a pas de death manager = ignore la collision 
        }

        Rigidbody playerRb = playerLife.GetComponent<Rigidbody>();
        
        if (playerRb == null || mobCollider == null)
        {
            return;
        }

        bool landedOnHead = playerRb.linearVelocity.y < 0f && collision.collider.bounds.min.y >= mobCollider.bounds.max.y - 0.15f;//vérifier si joueur tombe sur mob, LinearVelocity.y <0 vérifie que le joueur descend, bounds.min.y: point le + bas du collider, bounds.max.y - 0.15f : haut collider mob 0.15 = petite marge
        //condition pour tuer mob = sauter avant coup (genre champi) et si collider suffisament haut au moment ou il descend; 
        if (landedOnHead)// si joueur touche par dessus le mob = mob détruit par deathmanager
        {
            if (mobDeathManager != null)
            {
                mobDeathManager.Die();
            }
            return;
        }
        if (playerLife != null && playerLife.TryProtectFromEnemy())
        {
            return;
        }

        playerDeathManager.Die();//sinon fonctionnement de base appliqué par deathmanager
    }
}
