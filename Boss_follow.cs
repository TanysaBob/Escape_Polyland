using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public Transform Player;
    public float moveSpeed = 1f;
    public float stoppingDistance = 3f;
    public float attackDistance = 2f;
    public float attackDelay = 3f;
    public float knockbackForce = 10f;

    private CharacterController controller;
    private Animator animator;

    private float lastAttackTime = -Mathf.Infinity;
    private float timeInAttackRange = 0f;

    public float gravity = -9.81f;
    private float vertVelocity = 0f;
    private bool canFollow = false; // Controlled by animation end
    public Collider swordCollider; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        if (swordCollider != null)
            swordCollider.enabled = false;
    }

    void Update()
    {
        if (!canFollow || Player == null) return;

        Vector3 direction = Player.position - transform.position;
        direction.y = 0f;
        float distance = direction.magnitude;

        if (!controller.isGrounded)
        {
            vertVelocity  += gravity * Time.deltaTime;
        }
        else
        {
            vertVelocity = -1f;
        }
        
        Vector3 move = Vector3.zero;
        
        if (distance > stoppingDistance)
        {
            move = direction.normalized * moveSpeed;
            move.y = vertVelocity;

            controller.Move(move * Time.deltaTime);
            
            // Rotate to face player
            if (move != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);

            animator?.SetBool("isWalking", true);
        }
        else
        {
            move.y = vertVelocity;
            // Apply gravity if idle
            animator?.SetBool("isWalking", false);
        }

        if (distance <= attackDistance)
        {
            timeInAttackRange += Time.deltaTime;
            if (Time.time >= lastAttackTime + attackDelay)
            {
                Attack();
                lastAttackTime = Time.time;
                timeInAttackRange = 0f;
            }
        }
        else
        {
            timeInAttackRange = 0f;
        }
    }

    void Attack()
    {
        animator?.SetTrigger("Attack");
        animator?.SetBool("isWalking", true);
        
        StartCoroutine(ActivateSwordHitbox());
    }

    public void Knockback(Vector3 direction)
    {
        Vector3 knockback = direction.normalized * knockbackForce;
    }

    public void EnableFollow()
    {
        canFollow = true;
    }
    
    public void EnableSwordCollider()
    {
        if (swordCollider != null)
            swordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        if (swordCollider != null)
            swordCollider.enabled = false;
    }
    
    private System.Collections.IEnumerator ActivateSwordHitbox()
    {
        yield return new WaitForSeconds(1f);

        if (swordCollider != null)
            swordCollider.enabled = true;

        // Wait for duration of the attack animation (adjust this to match your swing)
        yield return new WaitForSeconds(1f);

        if (swordCollider != null)
            swordCollider.enabled = false;
    }
}
