using UnityEngine;
using System.Collections.Generic; 
public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy & Spawn Settings")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private float initialSpawnInterval = 5f;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float spawnIntervalDecrease = 0.5f;
    [SerializeField] private int scoreStepToIncrease = 500;
    [SerializeField] private int spawnPosCount; 
    private float currentSpawnInterval;
    private float spawnTimer;
    private int nextScoreThreshold;

    private PlayerMove player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        currentSpawnInterval = initialSpawnInterval;
        spawnTimer = currentSpawnInterval;
        nextScoreThreshold = scoreStepToIncrease;
    }

    private void Update()
    {
        if (player != null && player.receiveInput)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemiesAtAllPoints();
                spawnTimer = currentSpawnInterval;
            }

            CheckAndReduceSpawnInterval();
        }
    }

    private void CheckAndReduceSpawnInterval()
    {
        if (InGameUI.Instance != null && InGameUI.Instance.score >= nextScoreThreshold)
        {
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalDecrease);
            spawnPosCount = Mathf.Min(spawnPosCount + 1, spawnPoints.Length);
            nextScoreThreshold += scoreStepToIncrease;
        }
    }

    private void SpawnEnemiesAtAllPoints()
    {
        int count = Mathf.Min(spawnPosCount, spawnPoints.Length);
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, spawnPoints.Length);
            } while (usedIndices.Contains(index));

            usedIndices.Add(index);
            SpawnEnemyAtPoint(spawnPoints[index]);
        }
    }

    private void SpawnEnemyAtPoint(GameObject spawnObject)
    {
        if (spawnObject == null) return;

        int childCount = spawnObject.transform.childCount;
        if (childCount == 0) return;

        Vector3 spawnPos = spawnObject.transform.GetChild(Random.Range(0, childCount)).position;
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
       
    }
}
