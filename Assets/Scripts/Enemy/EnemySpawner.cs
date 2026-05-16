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

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        do
        {
            for (int i = 0; i < waves.Length; i++)
            {
                yield return StartCoroutine(SpawnWave(waves[i]));
                yield return new WaitForSeconds(waves[i].delayAfterWave);
            }
        }
        while (loopWaves);
    }

    private IEnumerator SpawnWave(EnemyWave wave)
    {
        foreach (EnemyWaveEntry entry in wave.enemies)
        {
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
            return;

        if (spawnPoints == null || spawnPoints.Length == 0)
            return;

        if (coreTarget == null)
            return;

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
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}