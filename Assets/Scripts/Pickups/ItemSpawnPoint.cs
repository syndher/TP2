using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemSpawnPoint : MonoBehaviour
{
    [System.Serializable]
    public class WeightedItem
    {
        public GameObject prefab;
        [Range(1, 100)] public int weight = 50;
        public float spawnCooldown = 5f; // Per-item delay
    }

    [Header("Detection Settings")]
    public float playerDetectionRadius = 3f;
    
    [Header("Spawn Settings")]
    public WeightedItem[] itemsToSpawn;
    
    [HideInInspector] public bool playerIsNearby;
    [HideInInspector] public bool isOccupied;
    private CircleCollider2D detectionCollider;
    private float currentCooldown;
    private int totalWeight;

    private void Awake()
    {
        detectionCollider = GetComponent<CircleCollider2D>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = playerDetectionRadius;
        CalculateWeights();
    }

    private void Update()
    {
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;
    }

    public bool CanSpawn()
    {
        return !playerIsNearby && !isOccupied && currentCooldown <= 0;
    }

    public GameObject GetRandomItem()
    {
        if (itemsToSpawn.Length == 0 || totalWeight <= 0) return null;

        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        foreach (var item in itemsToSpawn)
        {
            cumulativeWeight += item.weight;
            if (randomWeight < cumulativeWeight)
            {
                currentCooldown = item.spawnCooldown; // Set cooldown
                return item.prefab;
            }
        }
        return null;
    }

    private void CalculateWeights()
    {
        totalWeight = 0;
        foreach (var item in itemsToSpawn)
        {
            totalWeight += item.weight;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
            playerIsNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
            playerIsNearby = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isOccupied ? Color.yellow : 
        playerIsNearby ? Color.red : 
        currentCooldown > 0 ? Color.cyan : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}