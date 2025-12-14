using UnityEngine;

public class SubmarineManager : MonoBehaviour
{
    [Header("Game End Settings")]
    [SerializeField] private int desiaredChestAmount;
    [SerializeField] private GameObject QuestionUI;

    //
    internal bool isGameDone;
    private int openedChestAmount;

    public static SubmarineManager Instance;

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
        
    }


    public void CheckIsGameDone()
    {
        openedChestAmount++;

        if(openedChestAmount >= desiaredChestAmount)
        {
            isGameDone = true;
            
            QuestionManager.Instance.SetUpNewQuestion();
            QuestionUI.SetActive(true);
        }
    }
}
