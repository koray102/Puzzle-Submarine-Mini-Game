using System.Collections;
using TMPro;
using UnityEngine;

public class PuzzleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject congratsMessage;

    private Coroutine currentFadeRoutine;
    public static PuzzleUIManager Instance;


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
        congratsMessage.SetActive(false);
    }


    public void ShowUpCongratMessage(float targetAlpha, float duration, string message = "Tebrikler!")
    {
        congratsMessage.SetActive(true);
        TextMeshProUGUI textComponent = congratsMessage.GetComponent<TextMeshProUGUI>();
        textComponent.text = message;

        FadeTo(targetAlpha, duration, textComponent);
    }

    private void FadeTo(float targetAlpha, float duration, TextMeshProUGUI textComponent)
    {
        // eger fade islemi varsa durdur
        if (currentFadeRoutine != null)
        {
            StopCoroutine(currentFadeRoutine);
        }

        currentFadeRoutine = StartCoroutine(FadeRoutine(targetAlpha, duration, textComponent));
    }

    private IEnumerator FadeRoutine(float targetAlpha, float duration, TextMeshProUGUI textComponent)
    {
        float startAlpha = textComponent.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            Color newColor = textComponent.color;
            newColor.a = newAlpha;
            textComponent.color = newColor;

            yield return null;
        }

        Color finalColor = textComponent.color;
        finalColor.a = targetAlpha;
        textComponent.color = finalColor;
    }
}
