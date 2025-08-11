using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;

public class PillarInteract : MonoBehaviour
{
    public string interactionMessage = "Press E to Activate";
    public GameObject interactionUI; // a Text UI object to show the prompt
    public enum PillarType { SpawnEnemies, AwakenBoss }
    public PillarType type;

    public GameObject enemyPrefab; // Array of enemies to spawn
    public Transform[] spawnPoints;
    public int enemyCount = 4; 
    public float spawnDelay = 0.5f; // Delay between spawns
    
    private bool playerInRange = false;
    private bool hasActivated = false;

    public CinemachineCamera playcam;
    
    //For Boss
    public GameObject undeadKing;
    public CinemachineCamera bossCam;
    public GameObject[] allEnemies;
    public BossCamPan panScript;
    public GameObject bossHealthUI;
    public Slider healthBarSlider;
    
    public static int pillarPressCount = 0; // Static variable to track the number of times the pillar has been pressed

    public GameObject firstButtonText;
    public GameObject secondButtonText;
    public float displayTime = 2f;
    
    public static bool isSpawning = false;
    

    void Update()
    {
        if (!playerInRange || hasActivated|| isSpawning) return;
        
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !hasActivated)
        {
            hasActivated = true;
            pillarPressCount++;
            Debug.Log("Activated Pillar: " + type);
            interactionUI.SetActive(false);

            if (pillarPressCount == 1 && firstButtonText != null)
            {
                StartCoroutine(ShowFirstButtonText());
                
            }
            else if (pillarPressCount == 2 && secondButtonText != null)
            {
                
                StartCoroutine(ShowSecondButtonText());
            }
            
            if (pillarPressCount < 3)
            {
                SpawnEnemies();
            }
            else  if (pillarPressCount >= 3)
            {
                TriggerBossSequence();
            }
            // Trigger Undead King awakening here
            
        }
    }
    
    
    private void TriggerBossSequence()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        bossCam.Priority = 20;
        panScript.StartPan();
        
        if (undeadKing != null)
        {
            undeadKing.SetActive(true);

            // 🛠 Assign the slider from the bossHealthUI to the boss script
            BossHealth bh = undeadKing.GetComponent<BossHealth>();
            if (bh != null && bossHealthUI != null)
            {
                Slider slider = bossHealthUI.GetComponentInChildren<Slider>();
                bh.healthBarSlider = slider;
            }
        }
        
        if (bossHealthUI != null)
        {
            bossHealthUI.SetActive(true); //show the boss health bar
        }
        
        if (undeadKing != null)
        {
            undeadKing.SetActive(true);
        }
        
        BossWalk bossWalkScript = undeadKing.GetComponent<BossWalk>();
        if (bossWalkScript != null)
        {
            bossWalkScript.StartWalking();
        }
        
        
        gameObject.SetActive(false);
    }
    
    void SpawnEnemies()
    {
       StartCoroutine(SpawnEnemiesOverTime());
    }

    private System.Collections.IEnumerator SpawnEnemiesOverTime()
    {
        PillarInteract.isSpawning = true;
        
        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            yield return new WaitForSeconds(spawnDelay);
        }
        PillarInteract.isSpawning = false;
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PillarInteract.isSpawning)
        {
            playerInRange = true;
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionUI.SetActive(false);
        }
    }

    private System.Collections.IEnumerator ShowFirstButtonText()
    {
        firstButtonText.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        firstButtonText.SetActive(false);
    }
    private System.Collections.IEnumerator ShowSecondButtonText()
    {
        secondButtonText.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        secondButtonText.SetActive(false);
    }
}
