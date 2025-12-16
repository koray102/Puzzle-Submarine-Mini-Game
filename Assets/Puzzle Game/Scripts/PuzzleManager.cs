using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{   
    [Header("General Settings")]
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private Texture2D[] allLevelsImages;
    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 3;
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    [Tooltip("Bu normalde kod icinden kendisi otomatik degisiyor ama spesifik bir fotoya bakmak icin bu sekilde ayarladim")]
    [SerializeField] private int currentLvlIndex;
    [SerializeField] private float waitBeforeEnd;


    [Header("Next Puzzle Creation Settings")]
    [SerializeField] private float timeToWaitForNext = 5;
    [SerializeField] private float congratsMessageTargetAlpha = 1, congratsMessagedDuration = 3;


    //
    private bool isPuzzleDone;
    private int correctPuzzlePieces;
    private List<GameObject> puzzlePieces = new List<GameObject>();
    public static PuzzleManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        CreatePuzzle(currentLvlIndex);
    }


    private void CreatePuzzle(int levelIndex)
    {
        Texture2D currentImage = allLevelsImages[levelIndex];
        List<Transform> spawnPosCopy = new List<Transform>(spawnPositions);
        
        // Parça boyutu (UV uzayında 0 ile 1 arasındadır)
        Vector2 size = new Vector2(1.0f / columns, 1.0f / rows);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject newPiece = Instantiate(piecePrefab);
                
                newPiece.GetComponent<PuzzlePiece>().correctPosition = new Vector3(x, y, 0); 
                
                // ilk tum parcalara ayni resmi atiyoruz daha sonra o parcanin olmasi gereken konumna gore resmi pozisyonluyoruz
                Renderer rend = newPiece.GetComponent<Renderer>();
                rend.material.mainTexture = currentImage;
                
                rend.material.mainTextureScale = size; 
                
                rend.material.mainTextureOffset = new Vector2(x * size.x, y * size.y); // pozisyinlama kismi
                
                // Parcalari her seferinde rastgele noktalarda spawn etmek icin
                int newPiecePositionIndex = Random.Range(0, spawnPosCopy.Count());
                newPiece.transform.position = spawnPosCopy[newPiecePositionIndex].position;
                spawnPosCopy.RemoveAt(newPiecePositionIndex);

                puzzlePieces.Add(newPiece);
            }
        }
    }


    private void ClearPreviousPuzzle()
    {
        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            Destroy(puzzlePiece);
        }

        puzzlePieces.Clear();
    }


    private IEnumerator CreateNextPuzzle(float waitDur = 5)
    {
        currentLvlIndex++;

        // Son puzzleye ulastiginda bitti demektir
        if(currentLvlIndex >= allLevelsImages.Count())
        {
            GameManager.Instance.LoadScene("Main Menu", waitBeforeEnd);;
        }

        yield return new WaitForSeconds(waitDur);

        // Onceki puzzlenin parcalarini temizlemek icin (icine ek islemler de eklenebilir ama su anlik sadece destroy ediyo)
        ClearPreviousPuzzle();

        correctPuzzlePieces = 0;
        isPuzzleDone = false;
        PuzzleUIManager.Instance.ShowUpCongratMessage(0, 0.1f);

        CreatePuzzle(currentLvlIndex);
    }


    public void CheckPuzzleDone()
    {
        correctPuzzlePieces++;

        if(correctPuzzlePieces == rows * columns)
        {
            Debug.Log("Puzzle done");

            isPuzzleDone = true;

            PuzzleUIManager.Instance.ShowUpCongratMessage(congratsMessageTargetAlpha, congratsMessagedDuration);

            foreach (GameObject puzzlePiece in puzzlePieces)
            {
                puzzlePiece.GetComponent<PuzzlePiece>().StartCelebration(timeToWaitForNext);
            }

            // onceki puzzleden kalan artiklari temizleyip yeni puzzleyi olusturuyo
            StartCoroutine(CreateNextPuzzle(timeToWaitForNext));
        }
    }


    public void TurnToMain()
    {
        GameManager.Instance.LoadScene("Main Menu");
    }


    public void RestartPuzzle()
    {
        GameManager.Instance.RestartLevel();
    }
}
