using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private CharacterController controller;
   // private Transform cam;
   
   

   private Animator _animator;
   public float turnspeed = 200f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
       // cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        

        transform.Rotate(0f,horizontal*turnspeed*Time.deltaTime,0f);
        
        // Final movement direction
        Vector3 move = transform.forward * vertical;
        
        controller.Move(move * moveSpeed * Time.deltaTime);
        
        
        if (_animator != null)
            _animator.SetBool("isWalking", true);
        else
        {
            // ✅ Play idle animation
            if (_animator != null)
                _animator.SetBool("isWalking", false);
        
        
        }
    }
    
}

