using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using UnityEngine.UI;

public class CamZoneSwitch : MonoBehaviour
{
    public CinemachineCamera lowCam;
    public CinemachineCamera defaultCam;
    
    public GameObject welcomeText;      // "This must be the way out..."
    public CanvasGroup questionText;     // "Huh? Three Buttons

    public float fadeDuration = 1f;
    public float displayDuration = 2f;
    
    private bool hasActivated = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated) return;
        
        if (other.CompareTag("Player"))
        {
            hasActivated = true; // Prevents re-triggering
            
            // Lower priority on lowCam, raise on defaultCam
            lowCam.Priority = 5;
            defaultCam.Priority = 11;
            
            if(welcomeText !=null)
                welcomeText.SetActive(false);
            
            if (questionText != null)
                StartCoroutine(FadeInThenOut(questionText));
        }
    }
    
    private System.Collections.IEnumerator FadeInThenOut(CanvasGroup cg)
    {
        yield return StartCoroutine(FadeCanvasGroup(cg, 0f, 1f, fadeDuration)); // Fade in
        yield return new WaitForSeconds(displayDuration);                       // Stay visible
        yield return StartCoroutine(FadeCanvasGroup(cg, 1f, 0f, fadeDuration)); // Fade out
    }

    private System.Collections.IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, t / duration);
            yield return null;
        }
        cg.alpha = end;
    }
}
