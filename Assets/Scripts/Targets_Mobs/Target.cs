using UnityEngine;

public enum TargetType // me permet de renseigner dans l'editor un type pour les prefabs sur mes scènes 
{
    Luminous, // pas de ; mais des virgules sont utilisées pour une énumération!!
    Poison,
    Bounce, 
}

public class Target : MonoBehaviour
{
    public TargetType Type;
    public int ScoreValue; // me permet de renseigner la valeur associé à ce prefab direct dans l'editor 

    [Header("Abilities")] // effets renseignés dans l'editor + valeurs modifiables/ ajustables
    public float SlowDuration = 5f;
    public float SlowMultiplier = 0.5f;
    public float BounceForce = 10f;
}