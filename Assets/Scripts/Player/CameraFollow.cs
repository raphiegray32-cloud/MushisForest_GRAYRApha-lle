using UnityEngine;

public class CameraFollow : MonoBehaviour //je veux que la caméra suive le mouvement du joueur avec un peu de décallage le tout en isométrique 
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 offset;

    private void Start()// ici caméra mémorise position actuelle par rapport au joueur 
    {
        if (player == null)
        {
            return;
        }

        offset = transform.position - player.position; // calcule de la position de départ entre caméra et joueur 
    }



    private void LateUpdate()// la caméra ne se déplace qu'après la maj du player > pour - d'accous 
    {
        if (player == null)
        {
            return;
        }

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }// vector3lerp = rapprochement progressif vers la cible (position joueur) ; smoothspeed= vitesse de la caméra s'accomode de la vitesse du joueur (si joueur rapide la caméra s'accomode) 
}
