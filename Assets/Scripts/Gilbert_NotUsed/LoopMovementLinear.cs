using UnityEngine;

public class LoopMovementLinear : MonoBehaviour
{
    // tableau qui possède de transform
    [SerializeField] private Transform[] spawnPointArray;
    [SerializeField] private float distancePercentage;
    [SerializeField] private float speed;
    [SerializeField] private float timer;
    [SerializeField] private float actualDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private int originIndex=0;
    [SerializeField] private int endIndex=1;

    private void Start()
    {
        // calcul des valeurs entre les deux points, en mètre, la valeur augmente en fonction du delta time; actual distance= point de départ (0) + le temps * la vitesse (on ajt petit à petit des valeurs)
        maxDistance = Vector3.Distance(spawnPointArray[originIndex].position, spawnPointArray[endIndex].position);
    }


    private void FixedUpdate()  
        // deltaTime= temps passé depuis la dernière frame > vu qu"on est dans fixedupdate c'est fixeddeltatime, c'est sur cube2 
    {
        timer = timer + Time.fixedDeltaTime;// ou timer += Time.fixedDeltaTime
        actualDistance = actualDistance + timer * speed;
        distancePercentage = actualDistance/maxDistance;
        transform.position = Vector3.Lerp(spawnPointArray[originIndex].position, spawnPointArray[endIndex].position, distancePercentage);
        if (actualDistance >= maxDistance)
        {//quand j'avance je divise les index par 5 et je récupère le reste à chaque fois (5%3 = reste est 3 donc il peut encore avancer de trois)  
            timer = 0;
            actualDistance = 0;
            originIndex = (originIndex + 1) % spawnPointArray.Length; 
            endIndex = (endIndex + 1) % spawnPointArray.Length;
            //mais là si on laisse comme ça bah la maxDistance on la recalcule pas; % = modulo
        }

    }
}
