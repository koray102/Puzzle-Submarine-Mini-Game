using System.Collections;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    [Header("Piece Settings")]
    [SerializeField ] private float returnSpeed = 10f;
    [SerializeField ] private float snapThreshold = 1.5f;
    

    [Header("Hover Settings")]
    public float hoverScaleAmount = 1.1f;
    public float lerpSpeed = 10f;


    [Header("Titreme Ayarları")]
    public float shakeAmount = 5f;
    public float shakeSpeed = 50f;


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
    public void StartCelebration(float duration)
    {
        // Eğer zaten titriyorsa önce durdur
        if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeRoutine(duration));
    }


    private IEnumerator ShakeRoutine(float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // PerlinNoise veya Sinüs dalgası kullanarak yumuşak ama rastgele bir titreme üret
            // Z ekseninde (2D düzlemde) sağa sola salla
            float zRotation = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

            // Rastgelelik katmak istersen şunu kullanabilirsin:
            // float zRotation = Random.Range(-shakeAmount, shakeAmount);

            // Mevcut orijinal duruşunun üzerine bu sapmayı ekle
            transform.localRotation = originalRotation * Quaternion.Euler(0, 0, zRotation);

            yield return null;
        }

        // Süre bitince parçayı "Lap" diye düzeltme, yumuşakça eski haline dönsün
        transform.localRotation = originalRotation;
    }
}