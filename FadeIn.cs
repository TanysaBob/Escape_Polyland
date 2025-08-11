using UnityEngine.UI;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage; // Drag in the black full-screen Image
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    private System.Collections.IEnumerator FadeFromBlack()
    {
        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration); // 1 to 0 alpha
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // Optional: disable after fade
    }
}
