using UnityEngine;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<Target> targets;// liste champignon sur niv à ramasser (ce sont tous des targets car possèdent le script dans inspector)

    [SerializeField] private UIController uiController;

    [SerializeField] private LevelManager levelManager;
    private bool lifeMushroomSpawned = false;

    [Header("Life Mushroom")]
    [SerializeField] private GameObject lifeMushroomPrefab;
    [SerializeField] private Transform lifeSpawnPoint;



    public void OnTargetDestroyed(Target targetDestroyed) //fonction : argument de celle ci = target qui a été détruite//
    {
        if (targetDestroyed == null) // est ce que cette liste (targets) contient cet element (targetdestroyed) 
        {
            return;
        }

        if (targetDestroyed.Type != TargetType.Luminous)//la on vérifie que c'est les champignons lumineux 
        {
            return;
        }

        if (targets.Contains(targetDestroyed))
        {
            targets.Remove(targetDestroyed);

            if (targets.Count <= 0)
            {
                if (!lifeMushroomSpawned && !PlayerLife.HasLife)//si y'a plus de champignons sur le niveaux = ont tous été ramassés = lifemush peut apparaitre sur niv 
                {
                    if (lifeMushroomPrefab != null && lifeSpawnPoint != null)
                    {
                        Instantiate(lifeMushroomPrefab, lifeSpawnPoint.position, lifeSpawnPoint.rotation);
                        lifeMushroomSpawned = true;
                    }

                    else//sinon
                    {
                        //Debug.LogError("Prefab/spawnpoint po lo ");
                    }
                }
            }
        }
    }
}    