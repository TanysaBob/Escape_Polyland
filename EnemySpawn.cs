using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject enemyPrefab; // The prefab of the enemy to be spawned.
    public string tag; // The tag of the spawn points attached to this trigger.
    private GameObject[] spawnPoints;
    private bool canSpawn = true; // Flag to control spawning.

    void Start() {
        spawnPoints = GameObject.FindGameObjectsWithTag(tag);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && canSpawn) {

            // Instantiate a new enemy at each spawn point tagged with the specified tag.
            foreach (GameObject spawn in spawnPoints) {
                GameObject enemy = Instantiate(enemyPrefab, spawn.transform.position, Quaternion.identity);
                enemy.GetComponent<EnemyFollow>().Player = other.transform; // Sets the enemy to follow the proper Player GameObject.
            }

            canSpawn = false; // Prevents sequential spawns on this trigger (can be reset).
        }
    }

    public void ResetSpawnTrigger() {
        canSpawn = true;
    }
}
