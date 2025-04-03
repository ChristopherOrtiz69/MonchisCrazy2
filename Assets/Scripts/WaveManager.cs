using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    public int initialEnemies = 2;
    public float waveInterval = 5f;
    public float difficultyMultiplier = 1.2f;

    private int currentWave = 1;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveInterval);
            yield return StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        if (isSpawning) yield break;
        isSpawning = true;

        int enemiesToSpawn = initialEnemies * currentWave;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }

        // Esperar hasta que todos los enemigos sean destruidos
        yield return new WaitUntil(() => AreAllEnemiesDestroyed());

        currentWave++;
        isSpawning = false;
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        EnemyFollower enemyScript = enemy.GetComponent<EnemyFollower>();
        if (enemyScript != null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                enemyScript.player = playerObj.transform;
                Debug.Log($"Enemigo spawneado en {spawnPoint.position} persiguiendo a {playerObj.name}");
            }
            else
            {
                Debug.LogError("No se encontró un GameObject con la etiqueta 'Player'. Asegúrate de que tu personaje tiene la etiqueta correcta.");
            }

            enemyScript.health = Mathf.RoundToInt(enemyScript.health * difficultyMultiplier);
            enemyScript.moveSpeed *= difficultyMultiplier;

            enemyScript.enabled = true;
        }
        else
        {
            Debug.LogError("El prefab del enemigo no tiene el script EnemyFollower asignado.");
        }
    }

    private bool AreAllEnemiesDestroyed()
    {
       
        return FindObjectsOfType<EnemyFollower>().Length == 0;
    }
}
