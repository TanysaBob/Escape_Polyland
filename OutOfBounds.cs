using Unity.VisualScripting;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public Transform respawnLocation; // The location to return the Player to.

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Player>() != null) {
            other.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
}
