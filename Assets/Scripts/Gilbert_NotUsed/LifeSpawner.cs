using UnityEngine;

public class LifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPosition;

    private void Update()
    {
        //SI j'appuie sur E; get key down suffit pour ne faire spawn qu'un mur 
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Try Spawn");
            //ALORS un mur apparait  
            Instantiate(prefab, spawnPosition);
            //Debug.Log("Spawn OK");
        }

    }

    public void SpawnNewWall()
    {
        Instantiate(prefab, spawnPosition);
    }

}
