using UnityEngine;

public class MobLoop : MonoBehaviour
{
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform[] spawnPointArray;// points ou va se diriger le mobs, contenus dans cette variable 
    [SerializeField] private float speed = 2f;
    [SerializeField] private float startRotationY = 45f;// dégré de rotation du mob pour qu'il regarde les points ou il se dirige
    [SerializeField] private float secondRotationY = -135f;

    [SerializeField] private float actualDistance;// est associé à speed/time car la distance augmente suivant ces deux là  
    [SerializeField] private float maxDistance;
    [SerializeField] private float startHeight;

    [SerializeField] private int originIndex = 0;
    [SerializeField] private int endIndex = 1;

    private void Start()
    {

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (spawnPointArray == null || spawnPointArray.Length < 2)
        {
            enabled = false;
            return;
        }

        startHeight = rb.position.y;// pitié que mes mobs arrêtent de tomber

        rb.useGravity = false;
        rb.isKinematic = true;

        CalculateMaxDistance();

        rb.rotation = Quaternion.Euler(0f, startRotationY, 0f);
    }

    private void FixedUpdate()// augmente actualDistance en fonction de la vitesse + temps écoulé 
    {
        if (maxDistance <= 0)
        {
            return;
        }

        actualDistance += speed * Time.fixedDeltaTime;
        float distancePercentage = Mathf.Clamp01(actualDistance / maxDistance);

        Vector3 targetPosition = Vector3.Lerp(spawnPointArray[originIndex].position,spawnPointArray[endIndex].position,distancePercentage);// direction: ou se trouve le prochain point
        targetPosition.y = rb.position.y;
        rb.MovePosition(targetPosition);

        if (actualDistance >= maxDistance)
        {
            actualDistance = 0f;
            originIndex = (originIndex + 1) % spawnPointArray.Length;
            endIndex = (endIndex + 1) % spawnPointArray.Length;

            if (originIndex == 1)
            {
                rb.MoveRotation(Quaternion.Euler(0f, secondRotationY, 0f));// switch entre les deux rotations 
            }
            else if (originIndex == 0)
            {
                rb.MoveRotation(Quaternion.Euler(0f, startRotationY, 0f));
            }

            CalculateMaxDistance();
            //quand le mob atteint 1 les index avancent; % permet de revenir au premier point à la fin du tableau  > toujours même parcours 
        }
    }
    private void CalculateMaxDistance()//recalcule distance pour nouveau trajet 
    {
        maxDistance = Vector3.Distance(spawnPointArray[originIndex].position, spawnPointArray[endIndex].position);
    }

}
