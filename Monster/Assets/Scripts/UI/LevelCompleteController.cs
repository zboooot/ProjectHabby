using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompleteController : MonoBehaviour
{
    public TextMeshProUGUI textToFade;
    public float fadeDuration = 2.0f;

    private void Start()
    {
        textToFade = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeTextIn());
    }

    private IEnumerator FadeTextIn()
    {
        Color originalColor = textToFade.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            textToFade.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textToFade.color = targetColor; // Ensure it's fully faded
    }
}
