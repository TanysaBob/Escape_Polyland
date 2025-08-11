using UnityEngine;

public class FallZone : MonoBehaviour
{
    // The respawn point for the player
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
        }
    }
}
