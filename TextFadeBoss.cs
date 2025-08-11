using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeBoss : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fadeDuration = 1f;
    public float displayTime = 2f;

    private void Start()
    {
        if (text != null)
            text.alpha = 0f; // Start invisible
    }

    public void ShowText()
    {
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade in
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            text.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        // Wait
        yield return new WaitForSeconds(displayTime);

        // Fade out
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            text.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }
    }
}
