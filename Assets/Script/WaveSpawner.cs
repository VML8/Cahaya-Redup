using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab; // The enemy prefab to spawn
        public int enemyCount; // Number of enemies in this wave
        public float spawnInterval; // Time between spawns
    }

    public Wave[] waves; // List of waves
    public Transform spawnPoint; // Spawn point for enemies
    public float spawnAreaHeight = 5f; // Vertical range for random spawn positions
    public float timeBetweenWaves = 5f; // Time before next wave starts

    private int currentWaveIndex = 0; // Current wave index
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(HandleSceneChangeAndStartWaves());
    }

    IEnumerator HandleSceneChangeAndStartWaves()
    {
        // Wait for the scene to initialize (optional delay if needed)
        yield return new WaitForSeconds(1f);
        
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (currentWaveIndex < waves.Length)
        {
            spawning = true;
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            spawning = false;

            // Wait for all enemies to be defeated
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return null;
            }

            // Wait before starting the next wave
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        // Calculate a random vertical position within the spawn area
        float randomY = spawnPoint.position.y + Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, randomY, spawnPoint.position.z);

        // Instantiate the enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
