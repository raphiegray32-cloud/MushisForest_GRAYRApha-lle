using UnityEngine;

public class DeathZone : MonoBehaviour// zone de détection qui permet de cerner quand le joueur est hors scène et si oui = mort 
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerLife playerLife = other.GetComponentInParent<PlayerLife>();

        if (playerLife == null)
        {
            return;
        }

        DeathManager deathManager = playerLife.GetComponent<DeathManager>();

        if (deathManager == null)
        {
            return;
        }

        if (playerLife.TryProtectFromFall())
        {
            return;
        }

        deathManager.Die();
    }
}