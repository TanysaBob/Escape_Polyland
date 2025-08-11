using UnityEngine;

public class EnemyWeapon : Weapon
{
    public override void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>() != null || other.CompareTag("Player")) {
            if(Time.time - hitCooldownTime < hitCooldown) return;
            
            
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)damage); //Damage the player
                hitCooldownTime = Time.time;
            }
            
            Debug.Log("Weapon hit: " + other.name);
            Debug.Log("Hit player");
        }
    }
}
