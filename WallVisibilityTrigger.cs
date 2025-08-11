using UnityEngine;

public class WallVisibilityTrigger : MonoBehaviour
{
    public GameObject wallGroup; // Assign in inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWallVisibility(false); // Hide wall
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWallVisibility(true); // Show wall
        }
    }

    void SetWallVisibility(bool visible)
    {
        Renderer[] renderers = wallGroup.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = visible;
        }
    }
}
