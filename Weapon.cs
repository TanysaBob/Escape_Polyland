using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float damage = 25;
    public double reach;
    private GameObject player;
    private Transform holdTransform;
    
    public string weaponName = "Unknown";
    public string weaponDescription = "Unknown";
    public AudioClip hitClip;
    
    private HitMarker marker;
    
    public float hitCooldown = 0.5f;
    public float hitCooldownTime = -999f;

    public void setOwner(GameObject player) {
        this.player = player; // is attached to palyer
        holdTransform = player.transform.Find("Armature").Find("Root_M").Find("Spine1_M").Find("Spine2_M").Find("Chest_M").Find("Scapula_R").Find("Shoulder_R").Find("Elbow_R").Find("Wrist_R").Find("jointItemR");
    }
    
    private void Start()
    {
        marker = FindObjectOfType<HitMarker>();
    }

    void Update()
    {
        if (holdTransform != null)
        {
            transform.position = holdTransform.position;
            transform.rotation = holdTransform.rotation;
        }
    }
    
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return; //So it doesnt hit player
        if(Time.time - hitCooldownTime < hitCooldown) return; //Cooldown

        if (other.CompareTag("Enemy")) //If the player is attacking
        {
            if (marker != null)
            {
                marker.ShowHitMarker();
            }
            
            if (hitClip != null)
            {
                AudioSource.PlayClipAtPoint(hitClip, transform.position);
            }
            //Knockback enemy
            EnemyFollow _enemyFollow = other.GetComponentInParent<EnemyFollow>();
            if (_enemyFollow != null)
            {
                Vector3 knockDirection = other.transform.position - player.transform.position;
                _enemyFollow.Knockback(knockDirection);
            }
            
            //Deal Damage
            EnemyHealth health = other.GetComponentInParent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage((int)damage); //Damage the enemy
                hitCooldownTime = Time.time;
            }
        }
        
        if(other.CompareTag("Boss")) // If player is attacking boss
        {
            BossHealth health = other.GetComponentInParent<BossHealth>();
            if (health != null)
            {
                health.TakeDamage((int)damage); //Damage the enemy
                hitCooldownTime = Time.time;
            }
        }
        
    }
    
}

// Compare this snippet from Assets/Scripts/Armour.cs:
