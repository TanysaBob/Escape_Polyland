using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public GameObject flashPanel;       // Assign your red overlay panel here
    public float flashDuration = 0.1f;  // How long the flash lasts

    private bool isFlashing = false;

    public void TriggerFlash()
    {
        if (!isFlashing)
            StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        isFlashing = true;

        flashPanel.SetActive(true);

        yield return new WaitForSeconds(flashDuration);

        flashPanel.SetActive(false);

        isFlashing = false;
    }
}