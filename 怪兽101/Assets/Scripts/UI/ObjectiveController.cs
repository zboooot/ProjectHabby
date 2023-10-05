using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float fadeDuration = 2f; // Duration in seconds before the text fades out
    private float fadeTimer;

    public float minSize = 10f;
    public float maxSize = 50f;
    public float speed = 1f;

    private bool isExpanding = true;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        // Initialize the fade timer
        fadeTimer = fadeDuration;

        // Start the shrink and expansion
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

    }

    private void Update()
    {
        StartCoroutine(ChangeTextSize());
        Invoke("FadeText", 1f);
    }

    private IEnumerator ChangeTextSize()
    {
        while (true)
        {
            float newSize = isExpanding ? maxSize : minSize;

            // Smoothly change the font size over time
            while (Mathf.Abs(textMeshPro.fontSize - newSize) > 0.01f)
            {
                textMeshPro.fontSize = Mathf.Lerp(textMeshPro.fontSize, newSize, Time.deltaTime * speed);
                yield return null;
            }

            // Toggle between expanding and shrinking
            isExpanding = !isExpanding;

            // Wait for a moment before changing size again
            yield return new WaitForSeconds(1.0f);
        }
    }

    void FadeText()
    {
        // Reduce the fade timer over time
        fadeTimer -= Time.deltaTime;

        // Calculate the alpha value based on the remaining time
        float alpha = fadeTimer / fadeDuration;

        // Set the text alpha to fade it out
        textMeshPro.alpha = alpha;

        // Check if the text should be destroyed when it fully fades out
        if (fadeTimer <= 0)
        {
            Destroy(gameObject); // Destroy the GameObject (including the TextMeshPro text)
        }
    }
}
