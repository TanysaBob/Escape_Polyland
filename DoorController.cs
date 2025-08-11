using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public PressurePlate plate1;
    public PressurePlate plate2;
    public PressurePlate plate3;
    public PressurePlate plate4;
    public PressurePlate plate5;
    public PressurePlate plate6;
    
    public GameObject door; // drag your door here in Inspector
    private bool plate1Activated = false;
    private bool plate2Activated = false;
    private bool plate3Activated = false;
    private bool plate4Activated = false;
    private bool plate5Activated = false;
    private bool plate6Activated = false;
    private bool doorOpened = false;
    private AudioSource doorAudio;
    
    void Start()
    {
        doorAudio = GetComponent<AudioSource>();
    }
    
   

    void Update()
    {
        if (!plate1Activated && plate1.isPressed)
        {
            plate1Activated = true;
            Debug.Log("Plate 1 activated");
        }

        if (!plate2Activated && plate2.isPressed)
        {
            plate2Activated = true;
            Debug.Log("Plate 2 activated");
        }
        if (!plate3Activated && plate3.isPressed)
        {
            plate3Activated = true;
            Debug.Log("Plate 3 activated");
        }
        if (!plate4Activated && plate4.isPressed)
        {
            plate4Activated = true;
            Debug.Log("Plate 4 activated");
        }
        if (!plate5Activated && plate5.isPressed)
        {
            plate5Activated = true;
            Debug.Log("Plate 5 activated");
        }
        if (!plate6Activated && plate6.isPressed)
        {
            plate6Activated = true;
            Debug.Log("Plate 6 activated");
        }
        // If all plates are activated and door hasn't opened yet
        if (plate1Activated && plate2Activated && plate3Activated && plate4Activated && plate5Activated && plate6Activated && !doorOpened)
        {
            doorOpened = true;

            if (doorAudio != null)
                doorAudio.Play();

            StartCoroutine(DisableAfterSound());

            Debug.Log("Door opened");
        }
    }

    IEnumerator DisableAfterSound()
    {
        yield return new WaitForSeconds(doorAudio.clip.length);
        door.SetActive(false);
    }
}
