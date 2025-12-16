using UnityEngine;

public class PieceDragDropController : MonoBehaviour
{
    private PuzzlePiece selectedPiece;
    private Vector3 offset;
    private float zDistance;

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

                    // Offset hesapla
                    Vector3 mouseWorldPos = GetMouseWorldPos();
                    offset = selectedPiece.transform.position - mouseWorldPos;
                }
            }
        }

        if(selectedPiece && selectedPiece.isLocked)
            return;

        // surukleme kismi
        if (selectedPiece != null && Input.GetMouseButton(0))
        {
            Vector3 targetPos = GetMouseWorldPos() + offset;
            selectedPiece.transform.position = targetPos;
        }

        // birakma kismi
        if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            selectedPiece.CheckPlacement();
            
            selectedPiece.isDragging = false;
            selectedPiece = null;
        }
    }


    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zDistance; 
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}