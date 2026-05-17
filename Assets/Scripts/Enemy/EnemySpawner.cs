using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private class EnemyWaveEntry
    {
        public EnemyMovement enemyPrefab;
        public int count = 1;
        public float spawnDelay = 0.5f;
    }

    [System.Serializable]
    private class EnemyWave
    {
        public string waveName;
        public EnemyWaveEntry[] enemies;
        public float delayAfterWave = 3f;
    }

    [SerializeField] private EnemyWave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform coreTarget;
    [SerializeField] private bool loopWaves = false;

    public event Action OnAllWavesCompleted;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        if (waves == null || waves.Length == 0)
        {
            Debug.LogWarning("EnemySpawner has no waves assigned.");
            yield break;
        }

        do
        {
            for (int i = 0; i < waves.Length; i++)
            {
                yield return StartCoroutine(SpawnWave(waves[i]));
                yield return new WaitForSeconds(waves[i].delayAfterWave);
            }
        }
        while (loopWaves);

        yield return new WaitUntil(AllEnemiesDefeated);

        Debug.Log("All waves completed.");
        OnAllWavesCompleted?.Invoke();
    }

    private IEnumerator SpawnWave(EnemyWave wave)
    {
        if (wave == null || wave.enemies == null)
        {
            yield break;
        }

        foreach (EnemyWaveEntry entry in wave.enemies)
        {
            if (entry == null)
            {
                continue;
            }

            for (int i = 0; i < entry.count; i++)
            {
                SpawnEnemy(entry.enemyPrefab);
                yield return new WaitForSeconds(entry.spawnDelay);
            }
        }
    }

    private void SpawnEnemy(EnemyMovement enemyPrefab)
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("EnemySpawner tried to spawn a missing enemy prefab.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("EnemySpawner has no spawn points assigned.");
            return;
        }

        if (coreTarget == null)
        {
            Debug.LogWarning("EnemySpawner has no core target assigned.");
            return;
        }

        Transform spawnPoint = GetRandomSpawnPoint();

        EnemyMovement enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            Quaternion.identity
        );

        enemy.SetTarget(coreTarget);
    }

    private Transform GetRandomSpawnPoint()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }

    private bool AllEnemiesDefeated()
    {
        EnemyMovement[] activeEnemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        return activeEnemies.Length == 0;
    }
}