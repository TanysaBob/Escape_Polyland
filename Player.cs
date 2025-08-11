using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour {

    // Components of the Player GameObject:
    private Animator animator;
    private Rigidbody rb;
    private Health health;

    // Properties accessible by the Unity interface:
    public String name;
    public float playerSpeed = 8.0f; // How fast the player moves (units / frame).
    public float jumpHeight = 10.0f; // How high up the player jumps (units).
    public Armour armour; // The GameObject with the Armour script attached that the Player wears.
    public Weapon weapon; // The GameObject with the Weapon script attached that the Player wields.

    // Internal script properties:
    private Vector3 move; // The Vector3 produced by the user's input.
    private int level; // The Player's experience level.
    private bool isJumping = false; // Rigidbody methods should be called in FixedUpdate() (in step with the physics system). These booleans are switched in the Update() method to communicate with FixedUpdate().
    private bool isAttacking = false;

    // Called on initialization of the scene.
    private void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();

        weapon.setOwner(gameObject);
    }

    // Called once per physics update.
    void FixedUpdate() {
        if (isJumping && IsGrounded()) {
            Jump();
        } 

        if (isAttacking) {
            Attack();
        }
        
        rb.MovePosition(transform.position + move * Time.fixedDeltaTime * playerSpeed); // Moves the Rigidbody based on the Player's most recent input.
    }

    // Called once per frame.
    void Update() {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // The move direction of the Player ("Horizontal" is x-movement and "Vertical" is z-movement).

        // If input is received, move the Player and enable the Sprint animation. Else, disable the Sprint animation.
        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
            animator.SetBool("isSprinting", true);
        } else {
            animator.SetBool("isSprinting", false);
        }

        // If a "Jump" input is received, trigger the Jump() method from within FixedUpdate() through isJumping.
        if (Input.GetButtonDown("Jump")) {
            isJumping = true;
        }

        //If a "Fire2" (by default right mouse button) input is received, trigger the Attack() method from within FixedUpdate() through isAttacking.
        if (Input.GetButtonDown("Fire2")) {
            isAttacking = true;
        }
    }

    // Causes the Player GameObject to jump, given the specified jumpHeight.
    void Jump() {
        float gravity = Mathf.Abs(Physics.gravity.y); // This could be precalculated, but I like the idea of being able to IN THEORY change the direction of gravity. I don't think we'll ever do it, but it's nice to know we can :)
        float jumpForce = rb.mass * (float) Math.Sqrt(2 * gravity * jumpHeight); // Converts jumpHeight to a force that can be applied to the Rigidbody.
        rb.AddForce(-Physics.gravity.normalized * jumpForce, ForceMode.Impulse); // Add jumpForce as an impulse force in the opposite direction of gravity.
        animator.SetTrigger("isJumping"); // Triggers the "Jump" animation.
        isJumping = false; // Reset the switch.
    }

    // Causes the Player to attack using their Weapon (currently only plays the animation).
    public void Attack() {
        animator.SetTrigger("isAttacking");
        isAttacking = false;
    }

    // Increases the level of the Player by one (not sure what else to put here at the moment...)
    void LevelUp() {
        level++;
    }

    // Changes the equipped weapon.
    public void Equip(GameObject weaponPrefab) { //Updated and usable equip method
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        GameObject newWeaponObj = Instantiate(weaponPrefab);
        Weapon newWeapon = newWeaponObj.GetComponent<Weapon>();

        if (newWeapon != null)
        {
            newWeapon.setOwner(gameObject);
            weapon = newWeapon;
        }
        else
        {
            Debug.LogWarning("Tried to equip a weapon prefab that doesn't have a Weapon script!");
        }
    }

    // Returns true if the Player is touching the ground (any ground, no tag required), and false otherwise.
    bool IsGrounded() {
        return Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.1f);
    }
}