using System;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] string PlayerTag = "Player";
    [SerializeField] Transform platform;

    private void OnTriggerEnter(Collider other)
    {
        // If the object that collided with the platform is the player
           if (other.gameObject.tag.Equals(PlayerTag))
           {
             //  other.transform.SetParent(platform);
           }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // If the object that collided with the platform is the player
        if (other.gameObject.tag.Equals(PlayerTag))
        {
            // Unparent the player from the platform if they exit the trigger
            other.gameObject.transform.parent = null;
        }
    }
}
