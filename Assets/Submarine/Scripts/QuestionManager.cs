using System.Linq;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private QuestionData[] questons;

    [Header("UI Settings")]
    [SerializeField] private GameObject QuestionUI;
    [SerializeField] private GameObject questionText;
    [SerializeField] private GameObject option1;
    [SerializeField] private GameObject option2;
    [SerializeField] private GameObject option3;
    [SerializeField] private GameObject congratsText;


    private int currentQuestionIndex;
    private int correctAnswerCount;

    public static QuestionManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    public void SetUpNewQuestion()
    {
        QuestionData question = questons[currentQuestionIndex];

        questionText.GetComponent<TextMeshProUGUI>().text = question.questionText;
        option1.GetComponent<TextMeshProUGUI>().text = question.options[0];
        option2.GetComponent<TextMeshProUGUI>().text = question.options[1];
        option3.GetComponent<TextMeshProUGUI>().text = question.options[2];
    }

    public void CheckIsCorrectAnswer(int optionIndex)
    {
        QuestionData question = questons[currentQuestionIndex];
        int correctIndex = question.correctOptionIndex;

        if(currentQuestionIndex == questons.Count() - 1)
        {
            ShowUpCongratsMessage();
            return;
        }

        if(optionIndex == correctIndex)
        {
            Debug.Log("Correct Answer");
            correctAnswerCount++;
        }else
        {
            Debug.Log("Wrong Answer");
        }

        currentQuestionIndex++;
        SetUpNewQuestion();
    }


    private void ShowUpCongratsMessage()
    {
        QuestionUI.SetActive(false);

        if(correctAnswerCount <= 1)
        {
            congratsText.GetComponent<TextMeshProUGUI>().text = "IYI!";
        }else if(correctAnswerCount <= 3)
        {
            congratsText.GetComponent<TextMeshProUGUI>().text = "AFERIN!";
        }else
        {
            congratsText.GetComponent<TextMeshProUGUI>().text = "KUSURSUZ!";
        }

        congratsText.SetActive(true);
    }
}