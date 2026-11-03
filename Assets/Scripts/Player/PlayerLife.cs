using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    private static bool hasLife = false;

    public static bool HasLife// cette variable nous permet de garder l'information de si le joueur a une vie ou non entre les niveaux
    {
        get {  return hasLife; }
    }

    [Header("UI")]
    [SerializeField] private Image lifeIcon;
    [SerializeField] private TMP_Text lifeMessage;
    [SerializeField] private float messageDuration = 2f;// message du tuto 'vous avez obtenu champi de vie youpi'

    [Header("Protection")]
    [SerializeField] private float invincibilityDuration = 5f; 

    private bool isInvicible  = false;
    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetLife()
    {
        hasLife = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation; 
    }


    private void Start()
    {
        UpdateLifeUI();
        if (lifeMessage != null)
        {
            lifeMessage.gameObject.SetActive(false);
        }

    }

    public void CollectLife()
    {
        if (hasLife)
        {
            return;
        }

        hasLife = true;
        UpdateLifeUI();

        if (lifeMessage != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowLifeMessage());
        }
    }

    public bool TryProtectFromEnemy()
    {
        if (isInvicible)
        {
            return true;
        }
        if (!hasLife)
        {
            return false;
        }
        hasLife = false;
        UpdateLifeUI();

        StartCoroutine(InvincibilityRoutine());
        return true;
    }

    public bool TryProtectFromFall()
    {
        if (!hasLife)
        {
            return false;
        }

        hasLife = false;
        UpdateLifeUI();

        ReturnToStart();
        return true;
    }

    private void ReturnToStart()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = startPosition;
            rb.rotation = startRotation;
        }
        else
        {
            transform.SetPositionAndRotation(startPosition, startRotation);
        }
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvicible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvicible = false;
    }

    private IEnumerator ShowLifeMessage()
    {
        lifeMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        lifeMessage.gameObject.SetActive(false);
    }

    public void ResetLifeAfterDeath()
    {
        hasLife = false;
        isInvicible = false;
        StopAllCoroutines();
        UpdateLifeUI();
    }

    private void UpdateLifeUI()
    {
        if (lifeIcon != null)
        {
            lifeIcon.gameObject.SetActive(hasLife);
        }
    }
    

}
