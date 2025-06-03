using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [Header("Global Settings")]
    [SerializeField] private float globalSpawnInterval = 10f;
    [SerializeField] private float itemCheckRadius = 1f;
    [SerializeField] private LayerMask itemLayerMask;
    [SerializeField] private ItemSpawnPoint[] spawnPoints;

    private float nextSpawnTime;
    private List<GameObject> activeItems = new List<GameObject>();

    private void Start()
    {
        if (spawnPoints.Length == 0)
            spawnPoints = FindObjectsByType<ItemSpawnPoint>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            TrySpawnItem();
            nextSpawnTime = Time.time + globalSpawnInterval;
        }
    }

    private void TrySpawnItem()
    {
        foreach (var point in GetShuffledPoints())
        {
            if (!point.CanSpawn()) continue;

            // Additional physics check
            if (Physics2D.OverlapCircle(point.transform.position, itemCheckRadius, itemLayerMask))
            {
                point.isOccupied = true;
                continue;
            }

            GameObject itemPrefab = point.GetRandomItem();
            if (itemPrefab != null)
            {
                SpawnItem(itemPrefab, point);
                break; // Only spawn one item per cycle
            }
        }
    }

    private void SpawnItem(GameObject prefab, ItemSpawnPoint point)
    {
        GameObject newItem = Instantiate(prefab, point.transform.position, Quaternion.identity);
        point.isOccupied = true;
        activeItems.Add(newItem);

        // Auto-cleanup system
        var pickup = newItem.GetComponent<IPickup>();
        if (pickup != null)
        {
            pickup.OnPickedUp += () => {
                point.isOccupied = false;
                activeItems.Remove(newItem);
            };
        }

        // Fail-safe destruction
        Destroy(newItem, 60f); 
    }

    private ItemSpawnPoint[] GetShuffledPoints()
    {
        ItemSpawnPoint[] shuffled = new ItemSpawnPoint[spawnPoints.Length];
        System.Array.Copy(spawnPoints, shuffled, spawnPoints.Length);

        for (int i = 0; i < shuffled.Length; i++)
        {
            int rnd = Random.Range(i, shuffled.Length);
            var temp = shuffled[i];
            shuffled[i] = shuffled[rnd];
            shuffled[rnd] = temp;
        }
        return shuffled;
    }

    private void OnValidate()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
            spawnPoints = FindObjectsByType<ItemSpawnPoint>(FindObjectsSortMode.None);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        foreach (var point in spawnPoints)
        {
            Gizmos.DrawWireSphere(point.transform.position, itemCheckRadius);
        }
    }
}