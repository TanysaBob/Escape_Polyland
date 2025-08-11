using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;         // the player
    public Vector3 offset = new Vector3(0f, 2f, -5f); // height and behind
    public float followSpeed = 5f;   // smooth follow speed
    public float rotationSpeed = 10f; // smooth rotation speed

    private Vector3 currentOffset;
    private Transform camTransform;
    
    void Start()
    {
        camTransform = transform;
        currentOffset = offset;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Get input
        float forwardInput = Input.GetAxis("Vertical");

        // Only update camera rotation when moving forward
        if (forwardInput > 0.1f)
        {
            Quaternion desiredRotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);
            currentOffset = desiredRotation * offset;
        }

        // Desired camera position
        Vector3 desiredPosition = target.position + currentOffset;

        // Smoothly follow
        camTransform.position = Vector3.Lerp(camTransform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Always look at the player (optionally, offset upward to look at chest/head)
        camTransform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
