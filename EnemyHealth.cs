using UnityEngine;

public class EnemyHealth : Health
{
 //Inherits from Health.cs and modifies the Die() method to include a death animation and effect.
    public GameObject deathEffect;
    private Animator _animator;
    private bool isDead = false;
  
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        maxHP = 50;
        base.Start();
        _animator = GetComponent<Animator>();
    }
    
    
    public override void Die()
    {
        
        EnemyFollow follow = GetComponent<EnemyFollow>();
        
        if (isDead) return;
        isDead = true;

        if (_animator != null)
        {
            _animator.SetTrigger("Die"); //Trigger for death animation
            
            if(follow != null)
                follow.enabled = false; //Stop enemy from following player
            
            StartCoroutine(WaitAndDestroy()); //Wait for death animation to finish
        }
        else
        {
            Destroy(gameObject);
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
    private System.Collections.IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1.2f); // match death anim time
        
        if(_animator !=null)
            _animator.enabled = false; //Remove animator to prevent looping
        
        Destroy(gameObject);
    }
}
