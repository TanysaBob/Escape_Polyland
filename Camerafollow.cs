using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    
    public Transform cameraTransform; // Assign the camera in the Inspector
    private Vector3 offset;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        
            offset = new Vector3(offsetX, offsetY, offsetZ);
            transform.position = cameraTransform.position + offset;
        
    }
}
