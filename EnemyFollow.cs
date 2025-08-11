using UnityEngine;
using UnityEngine.UI;

public class EnemyFollow : MonoBehaviour
{

    public Transform Player;
    public float moveSpeed = 3f;
    public float stoppingDistance = 3f;
    public float attackDistance = 2f;
    public float attackDelay = 1.5f;
    public float kncockbackForce = 10f;

    private CharacterController controller;

    private Animator _animator;

    private float timeInAttackRange = 0f; // for smoother attacks
    
    //Adjustments for gravity
    private float vertVelocity = 0f;
    public float gravity = -9.81f;
    private float lastAttackTime = -Mathf.Infinity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Gravity Control
        if (!controller.isGrounded)
        {
            vertVelocity += gravity * Time.deltaTime;
        }
        else
        {
            vertVelocity = -1f; // small value to stay grounded
        }

        if (Player == null) return;

        Vector3 direction = Player.position - transform.position;
        direction.y = 0f; // keep movement flat

        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
            move.y = vertVelocity;

            controller.Move(move);

            // Rotate to face player
            if (move != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            if (_animator != null)
                _animator.SetBool("isWalking", true);
        }
        else
        {
            // Play idle animation
            if (_animator != null)
                _animator.SetBool("isWalking", false);
        }

        if ( distance <= attackDistance)
        {
            timeInAttackRange += Time.deltaTime;
            
            if (distance <= attackDistance && Time.time >= lastAttackTime + attackDelay) //Attack distance
            {
                Attack();
                lastAttackTime = Time.time;
                timeInAttackRange = 0f; //Reset to avoid chaining attacks
            }
        }
        else
        {
            timeInAttackRange = 0f;// rset if player leaves the attack range
        }
       
    }
    
    //Sets trigger to do attack animation
    void Attack()
    {
        if (_animator != null)
            _animator.SetTrigger("Attack");
    }

    public void Knockback(Vector3 direction)
    {
        Vector3 knockbackDirection = direction.normalized * kncockbackForce;
        knockbackDirection.y = 0;  // keep horizontal
        controller.Move(knockbackDirection * Time.deltaTime);
    }
    
    
}



