using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private Vector3 startPosition;
    internal Vector3 correctPosition;
    internal bool isDragging = false;
    internal bool isLocked = false;

    [Header("Ayarlar")]
    [SerializeField ] private float returnSpeed = 10f; // Geri dönme hizi
    [SerializeField ] private float snapThreshold = 1.5f;


    void Start()
    {
        startPosition = transform.position;
    }


    void Update()
    {
        if (!isDragging)
        {
            // Lerp: A noktasından B noktasına yumuşak geçiş
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * returnSpeed);
        }
    }


    // Mouse bırakıldığında bu fonksiyon çağrılacak
    public void CheckPlacement()
    {
        // Hedef ile şu anki konum arasındaki mesafeyi ölç
        float distance = Vector3.Distance(transform.position, correctPosition);

        // Eğer mesafe tolerans değerinden küçükse (Yani yeterince yakınsa)
        if (distance <= snapThreshold)
        {
            // 1. Hedefi güncelle: Artık geri döneceği yer "Doğru Yer" olsun.
            startPosition = correctPosition;
            
            // 2. Kilitle: Artık bu parçayı kimse hareket ettiremesin.
            isLocked = true;
            
            PuzzleManager.Instance.CheckPuzzleDone();
            Debug.Log("Parça yerine oturdu!");
        }
    }
}