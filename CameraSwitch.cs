using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera cam1;  // Drag and drop Camera 1 in the Inspector
    public Camera cam2;  // Drag and drop Camera 2 in the Inspector

    void Start()
    {
        cam1.enabled = true;   // Camera 1 starts enabled
        cam2.enabled = false;  // Camera 2 starts disabled
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.C))  // Press 'C' to switch
        {
            cam1.enabled = !cam1.enabled;  
            cam2.enabled = !cam2.enabled;
        } 
    }
}
