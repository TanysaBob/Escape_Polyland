using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DoorExit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image fadeImage; // Drag the black UI image here
    public float fadeDuration = 1f;
    public string escapeSceneName = "End_Scene";

    private bool hasFaded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasFaded && other.CompareTag("Player"))
        {
            hasFaded = true;
            StartCoroutine(FadeAndLoad());
        }
    }

    private System.Collections.IEnumerator FadeAndLoad()
    {
        float t = 0f;
        Color color = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(escapeSceneName);
    }
}
