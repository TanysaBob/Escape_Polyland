using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public GameObject player;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    private Vector3 playerPos;
    private Vector3 offset;

    void Start() {
        
    }
    
    void Update() {
        playerPos = player.transform.position;
        offset = new Vector3(offsetX, offsetY, offsetZ);
        transform.position = playerPos - offset;
    }
}
