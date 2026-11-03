using UnityEngine;
using System.Collections;

public class ColorFeedback : MonoBehaviour// pas utiliser pour mon code actuel 
{
    [SerializeField] private Material newMaterial;
    [SerializeField] private Material originalMaterial;
    [SerializeField] private float duration;
    private float timer;
    private bool isHighlight;
    private Material baseMaterial;

    
    private void Start()
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
    }
    
    
    public void ChangeMaterial(Material newMat)// ce qu'il y a entre parenthèses dans une fonction ce sont les inputs de cette fonction (input = quand on l'appelle on modifie une valeur dans cette fonction; par exemple: spawn actor of class, à quoi le pin entrée 'transform' sert ? pour rentrer une donnée par rapport à ça)
    {
        GetComponent<MeshRenderer>().material = newMat; // j'accède à un component pour modifier une de ses variables
    }

    private void FixedUpdate()
    {
        if (isHighlight)
        {
            timer = timer + Time.fixedDeltaTime;

            if (timer >= duration)
            {
                timer = 0;
                isHighlight = false;
                ChangeMaterial(baseMaterial);
            }
        }
        
    }

    IEnumerator TempoCoroutine() // pas de void (void = je renvoie rien, pas d'output) ici j'en ai un spécifique 'yield return' qui répond à la co routine
    {
        ChangeMaterial(newMaterial);
        yield return new WaitForSeconds(duration);// au moment ou la ligne est lue ka co couritne fait un delai de la duréée de duration, donc 
        ChangeMaterial(baseMaterial);
    }


    public void StartHighlight()
    {
        StartCoroutine(TempoCoroutine());
        //ChangeMaterial(newMaterial); cntrl k cntr c = commente tout 
        //isHighlight = true; cntrl k cntrl u = décommenté
    }

    public void ResetMaterial()
    {
        GetComponent<MeshRenderer>().material = originalMaterial; //je retourne à mon component de base
    }
}
