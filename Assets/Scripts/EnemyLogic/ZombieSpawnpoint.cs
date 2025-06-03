using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ZombieSpawnpoint : MonoBehaviour
{
    [Header("Player Detection")]
    [Tooltip("Radius where player blocks spawning")]
    public float detectionRadius = 5f;

    [HideInInspector] public bool playerInZone;
    private CircleCollider2D triggerZone;

    private void Awake()
    {
        triggerZone = GetComponent<CircleCollider2D>();
        triggerZone.isTrigger = true;
        triggerZone.radius = detectionRadius; // Set collider size
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for PlayerMovement component instead of tag
        if (other.GetComponent<PlayerMovement>() != null)
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            playerInZone = false;
        }
    }


}