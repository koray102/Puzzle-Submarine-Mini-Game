using UnityEngine;

public class PieceDragDropController : MonoBehaviour
{
    private PuzzlePiece selectedPiece; // Şu an tuttuğumuz parça
    private Vector3 offset; // Tıkladığımız yer ile objenin merkezi arasındaki fark
    private float zDistance; // Kameraya olan uzaklık

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // mousenin oldugu yerden ileri raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                PuzzlePiece piece = hit.transform.GetComponent<PuzzlePiece>();
                
                if (piece != null)
                {
                    selectedPiece = piece;
                    selectedPiece.isDragging = true;

                    zDistance = Camera.main.WorldToScreenPoint(selectedPiece.transform.position).z;

                    // Offset hesapla (Objenin merkezinden değil, tuttuğumuz köşeden gelsin diye)
                    Vector3 mouseWorldPos = GetMouseWorldPos();
                    offset = selectedPiece.transform.position - mouseWorldPos;
                }
            }
        }

        // 2. Mouse Basılı Tutulduğu Sürece (Sürükleme)
        if (selectedPiece != null && Input.GetMouseButton(0))
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            selectedPiece.transform.position = targetPos;
        }

        // 3. Mouse Bırakıldığı An (Bırakma)
        if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            // ELİMİZDEN BIRAKMADAN ÖNCE KONTROL ET: Yerine geldi mi?
            selectedPiece.CheckPlacement();
            
            selectedPiece.isDragging = false; // Parçayı serbest bırak, yerine dönsün
            selectedPiece = null; // Elimizi boşalt
        }
    }

    // Mouse'un 2D ekran koordinatını 3D dünya koordinatına çeviren yardımcı fonksiyon
    private Vector3 GetMouseWorldPos()
    {
        // Mouse'un X ve Y'sini al, Z olarak objenin derinliğini kullan
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zDistance; 
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}