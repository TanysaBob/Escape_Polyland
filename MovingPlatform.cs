using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    public Vector3 platformVelocity { get; private set; }

    private Vector3 target;
    private Vector3 lastPosition;

    void Start()
    {
        target = pointB.position;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Move platform
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Switch target if close
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }

        // Calculate velocity
        platformVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}
