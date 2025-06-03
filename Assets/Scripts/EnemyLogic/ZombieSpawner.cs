using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private ZombieSpawnpoint[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    
    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        
        if (spawnTimer <= 0f)
        {
            spawnTimer = spawnInterval;
            TrySpawnZombie();
        }
    }

    private void TrySpawnZombie()
{
    // First filter only available spawn points (where player isn't present)
    List<ZombieSpawnpoint> availablePoints = new List<ZombieSpawnpoint>();
    foreach (ZombieSpawnpoint sp in spawnPoints)
    {
        if (!sp.playerInZone)
        {
            availablePoints.Add(sp);
        }
    }

    // If we have any available points, pick one at random
    if (availablePoints.Count > 0)
    {
        int randomIndex = Random.Range(0, availablePoints.Count);
        ZombieSpawnpoint chosenPoint = availablePoints[randomIndex];
        Instantiate(zombiePrefab, chosenPoint.transform.position, Quaternion.identity);
    }
}

    private void OnValidate()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            spawnPoints = FindObjectsByType<ZombieSpawnpoint>(FindObjectsSortMode.None);
            if (spawnPoints.Length > 0)
            {
                EditorUtility.SetDirty(this);
            }
        }
    }

}