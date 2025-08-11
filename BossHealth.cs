using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
 //Inherits from Health.cs and modifies the Die() method to include a death animation and effect.
    public GameObject deathEffect;

    private Animator _animator;
    private bool isDead = false;
    public Slider healthBarSlider;
    
    public GameObject bossHealthUI;  
    public GameObject victoryText; 
    public GameObject bossDeadText; // Text to show when the boss is dead
    public GameObject DoorOpen;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        maxHP = 500;
        base.Start();
        _animator = GetComponent<Animator>();
        healthBarSlider.maxValue = maxHP;
        healthBarSlider.value = maxHP;
        
        UpdateHealthUI();
    }
    
    
    public override void Die()
    {
        
        BossFollow follow = GetComponent<BossFollow>();
        
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
        
        if (bossHealthUI != null)
            bossHealthUI.SetActive(false);

        // Show victory message
        if (bossDeadText != null)
            StartCoroutine(ShowVictoryMessage());
        
    }
    private System.Collections.IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(3f); // match death anim time
        
        if(_animator !=null)
            _animator.enabled = false; //Remove animator to prevent looping
        
        Destroy(gameObject);
    }
    
    public override void TakeDamage(int damage)
    {
        
        base.TakeDamage(damage); 
        UpdateHealthUI();
    }
    
    private void UpdateHealthUI()
    {
        if (healthBarSlider != null)
            healthBarSlider.value = curHP;
    }
    
    private System.Collections.IEnumerator ShowVictoryMessage()
    {
            bossDeadText.SetActive(true);
            yield return new WaitForSeconds(2f);
            bossDeadText.SetActive(false);

            Destroy(DoorOpen);
            
            if (victoryText != null)
            {
                victoryText.SetActive(true);
                yield return new WaitForSeconds(2f);
                victoryText.SetActive(false);
            }
    }
    
}
