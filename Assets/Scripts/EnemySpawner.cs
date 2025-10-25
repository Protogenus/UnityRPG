using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public int maxEnemies = 20;
    public float spawnRadius = 10f;

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        // Count current enemies efficiently
        int currentEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (spawnTimer <= 0f && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPos.y = 0.5f; // ground height
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// Clears all enemies and resets spawn timer.
    /// Called when player respawns.
    /// </summary>
    public void ResetEnemies()
    {
        Debug.Log("ResetEnemies() called — clearing all enemies.");

        // Destroy all existing enemies in the scene
        var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        // Reset spawn timer to force immediate spawn cycle
        spawnTimer = 0f;

        Debug.Log("All enemies cleared. Spawner reset and ready to start spawning again.");
    }
}
