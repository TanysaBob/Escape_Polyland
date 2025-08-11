using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera newCamera; // Drag your new camera here in Inspector
    public Camera oldCamera; // The current active camera

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            oldCamera.gameObject.SetActive(false);
            newCamera.gameObject.SetActive(true);
        }
    }
}
