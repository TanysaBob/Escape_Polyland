using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health {
    public Transform respawnLocation;
    private Animator _animator;

    public Slider healthBarSlider;
    public TMP_Text healthText;
    
    
    public GameObject deathScreen;
    private Player playerScript;
   
    public int maxHPChoice;
    
    protected override void Start()
    {
        maxHP = maxHPChoice;
        base.Start();
        Debug.Log($"PlayerMaxHealth {maxHP} PLayerCurrentHealth" );
        _animator = GetComponent<Animator>();
        healthBarSlider.maxValue = maxHP;
        healthBarSlider.value = maxHP;

        UpdateHealthUI();
    }


    public override void Die()
    {
        StartCoroutine(HandleDeathSequence());
    }

    private System.Collections.IEnumerator HandleDeathSequence()
    {
        
        Player player = GetComponent<Player>();
            
            if (_animator != null)
            {
                _animator.SetTrigger("Die"); //Trigger for death animation
            }
            
        
            
            yield return new WaitForSeconds(1.7f);
            // Reset all spawn triggers.
            GameObject[] spawnTriggers = GameObject.FindGameObjectsWithTag("SpawnTrigger");

            foreach (GameObject spawnTrigger in spawnTriggers) {
                spawnTrigger.GetComponent<EnemySpawn>().ResetSpawnTrigger();
            }

            // Destroy all existing enemy GameObjects.
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach (GameObject enemy in enemies) {
                Destroy(enemy);
            }

            Time.timeScale = 0f;
            
            deathScreen.SetActive(true);
            
    }

    public void RestartLevel()
    {
        // Re-enable player movement (in case scene doesn't fully reload — defensive)
        if (playerScript != null)
            playerScript.enabled = true;
        ResetHealth();
        PillarInteract.pillarPressCount = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public override void TakeDamage(int damage)
    {
        
        base.TakeDamage(damage); // still calls ResetHealth() and logs
        healthBarSlider.value = Mathf.Clamp(healthBarSlider.value - damage, 0, maxHP);
        UpdateHealthUI();
        FindObjectOfType<DamageFlash>().TriggerFlash();
    }
    
    public override void ResetHealth()
    {
        base.ResetHealth();
        if (healthBarSlider != null)
            healthBarSlider.value = maxHP;
        UpdateHealthUI();
    }
    
    private void UpdateHealthUI()
    {
        if (healthBarSlider != null)
            healthBarSlider.value = curHP;

        if (healthText != null)
            healthText.text = $"{curHP} / {maxHP}";
    }
    
}
