using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question Data")]
public class QuestionData : ScriptableObject
{
    [TextArea(2, 5)]
    public string questionText;
    
    public string[] options; 
    
    public int correctOptionIndex; 
}