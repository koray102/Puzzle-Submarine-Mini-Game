using System.Collections;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    [Header("Piece Settings")]
    [SerializeField] private float returnSpeed = 10f;
    [SerializeField] private float snapThreshold = 1.5f;
    

    [Header("Hover Settings")]
    [SerializeField] private float hoverScaleAmount = 1.1f;
    [SerializeField] private float lerpSpeed = 10f;


    [Header("Shake Settings")]
    [SerializeField] private float shakeAmount = 5f;
    [SerializeField] private float shakeSpeed = 50f;


    [Header("Efekt Ayarları")]
    [SerializeField] private AudioClip successSound;
    [SerializeField] private ParticleSystem successParticles;
    

    private AudioSource audioSource;
    private Quaternion originalRotation;
    private Coroutine shakeCoroutine;
    private Vector3 startPosition;
    internal Vector3 correctPosition;
    internal bool isDragging = false;
    internal bool isLocked = false;
    private Vector3 originalScale;
    private Vector3 targetScale;


    void Start()
    {
        startPosition = transform.position;

        originalScale = transform.localScale;
        targetScale = originalScale;

        originalRotation = transform.localRotation;

        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (!isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * returnSpeed);
        }

        if(!isLocked)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * lerpSpeed);
        }else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * lerpSpeed * 2);
        }
    }


    public void CheckPlacement()
    {
        float distance = Vector3.Distance(transform.position, correctPosition);

        if (distance <= snapThreshold)
        {
            startPosition = correctPosition;
            
            isLocked = true;
            
            PuzzleManager.Instance.CheckPuzzleDone();
            TriggerEffects();
        }
    }


    void OnMouseEnter()
    {
        // mouse gelince targeti buyutecek sekilde ayarliyoruz
        targetScale = originalScale * hoverScaleAmount;
    }


    void OnMouseExit()
    {
        // mouse gidince eski haline getiriyoruz
        targetScale = originalScale;
    }

    // Bu fonksiyonu Manager'dan çağıracaksın
    public void StartCelebration(float duration, float durationBeforeShake)
    {
        // onceden bir titreme varsa ilk durdur sonra baslat
        if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeRoutine(duration, durationBeforeShake));
    }


    private IEnumerator ShakeRoutine(float duration, float durationBeforeShake)
    {
        yield return new WaitForSeconds(durationBeforeShake);

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // sin fonksiyonuna gore salliyor
            float zRotation = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

            transform.localRotation = originalRotation * Quaternion.Euler(0, 0, zRotation);

            yield return null;
        }

        transform.localRotation = originalRotation;
    }


    public void TriggerEffects()
    {
        if (successSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(successSound, 1f); 
        }

        if (successParticles != null)
        {
            successParticles.Play();
        }
    }
}