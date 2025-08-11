using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed = false;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            isPressed = true;
            if (audioSource != null)
                audioSource.Play();
            Debug.Log("Plate pressed");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            isPressed = false;
        }
    }
}
