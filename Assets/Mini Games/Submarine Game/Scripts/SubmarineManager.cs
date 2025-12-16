using UnityEngine;

public class SubmarineManager : MonoBehaviour
{
    [Header("Game End Settings")]
    [SerializeField] private int desiaredChestAmount;
    [SerializeField] private GameObject QuestionUI;

    //
    internal bool isChestsDone;
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


    public void CheckIsGameDone()
    {
        openedChestAmount++;

        if(openedChestAmount >= desiaredChestAmount)
        {
            isChestsDone = true;
            
            QuestionManager.Instance.SetUpNewQuestion();
            QuestionUI.SetActive(true);
        }
    }


    public void TurnToMain()
    {
        GameManager.Instance.LoadSceneInstantly("Main Menu");
    }

    public void RestartSubmarine()
    {
        GameManager.Instance.RestartLevel();
    }
}
