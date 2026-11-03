using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // si devant une variable on met ni public ni private la variable sera automatiquement comptée comme étant privée
    private Rigidbody rb;
    private float HorizontalVelocity = 0f;
    private float VerticalVelocity = 0f;
    [SerializeField] private float JumpForce = 7f;// force du sat
    [SerializeField] private bool IsGrounded = false;//est ce que le joueur touche le sol 
    [SerializeField] private float Speed = 0f;
    [SerializeField] private PlayerEffects playerEffects; // malus ralentit le joueur, je dois lier ce script à celui des effets + utilise speedmultiplier

    void Start()
    { // plus opti car je n'ai pas à refaire la recherche pour get rb 
        rb = GetComponent<Rigidbody>();
        if (playerEffects == null)
            playerEffects = GetComponent<PlayerEffects>();

    }

    void Update()
    {// fonction qui fait que vu qu'on a rien avant on s'appuie sur le GO surlequel on est: donc la vu que le script est sur le player bah on se base sur le RB du player 

        //on créer des inputs pour l'axe horizontal (droite/gauche) 
        if (Input.GetKey(KeyCode.RightArrow))
        {
            HorizontalVelocity = 1f; 
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            HorizontalVelocity = -1f;
        }
        else
        {
            HorizontalVelocity = 0f;
        }

        //ici on fait up and down (vertical axe)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            VerticalVelocity = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            VerticalVelocity = -1f;
        }
        else
        {
            VerticalVelocity = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))// getkeydown pour que le saut n'arrive que quand on a appuyé et pas quand on maintient 
        {
            Jump();
        }
        Vector2 direction = new Vector2(HorizontalVelocity, VerticalVelocity).normalized;// le joueur peut avancer en diagonal mais normalized permet que ça ne soit pas + rapide (limite longueur vecteur à 1)
        HorizontalVelocity = direction.x;
        VerticalVelocity = direction.y;

        float currentSpeed = Speed * playerEffects.SpeedMultiplier;// ici on calcul la vitesse normale du joueur + le ralentissement
        rb.linearVelocity = new Vector3(HorizontalVelocity * currentSpeed, rb.linearVelocity.y, VerticalVelocity * currentSpeed); //ici on définit les vitesses xyz; linearvelocity = gravité
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 0.6f);// rayon dirigé vers le bas, détecte sol pour savoir si joueur peut sauter ou non (non s'il est déjà en l'air) 
    }

    private void Jump()
    {
        if (IsGrounded == false)
        {
            return;
        }
        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;

        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        IsGrounded = false; 
    }
}
