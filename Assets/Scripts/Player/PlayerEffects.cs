using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public float SpeedMultiplier { get; private set; } = 1f;// private set= seul playereffects peut modifirt cette variable
    private Coroutine slowCoroutine;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void ApplySlow(float multiplier, float duration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }
        
        slowCoroutine = StartCoroutine(SlowRoutine(multiplier, duration)); // slowroutine et pas slowcoroutine ?

    }

    private IEnumerator SlowRoutine(float multiplier, float duration)
    {
        SpeedMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        SpeedMultiplier = 1f;
        slowCoroutine = null; 
    }   

    public void Bounce(float force)// vitesse horizontale = assurée, vitesse verticale = 0, + impulsion vers le haut 
    {
        if (rb == null)
        {
            return;
        }
        
        Vector3 velocity = rb.linearVelocity;
        velocity.y = Mathf.Max(velocity.y + force, force);//guaranti rebond vers le haut même en chute > on garde la plus grande des valeurs comme score 
        rb.linearVelocity = velocity;
        }
    }

