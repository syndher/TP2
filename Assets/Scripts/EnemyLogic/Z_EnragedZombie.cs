using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Z_EnragedZombie : MonoBehaviour, IZombie
{
    [SerializeField] private float speed = 4f;
    float IZombie.speed => speed;
    [SerializeField] private float damage = 20f;
    private Infection infection;
    [SerializeField] private Animator animator;

    private IPlayer targetPlayerScript;
    private GameObject targetPlayer;
    [SerializeField] private float detectionRange = 30f;
    [SerializeField] private float chaseRange = 100f;


    private void FindPlayerByInterface() // Finds the player by interface
    {
        targetPlayerScript = FindFirstObjectByType<PlayerMovement>(); // Find the first IPlayer instance
        if (targetPlayerScript != null)
        {
            targetPlayer = ((MonoBehaviour)targetPlayerScript).gameObject; // Get the player's GameObject
            Debug.Log("Player found: " + targetPlayer.name); // Debug log to confirm the player is found
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }


    void ChasePlayer()
    {
        if (targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.transform.position - transform.position).normalized; // Get direction to player
            transform.position += direction * speed * Time.deltaTime; // Move the zombie
            animator.SetBool("MovingX", true);
        }
    }

    void Start()
    {
        FindPlayerByInterface();
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            float distance = Vector3.Distance(transform.position, targetPlayer.transform.position);
            if (distance <= detectionRange)
            {
                ChasePlayer();
            }
            else
            {
                animator.SetBool("MovingX", false);
            }
        }
        FlipEnemySpriteTowardPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PenBullet penBullet = collision.gameObject.GetComponent<PenBullet>();
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (penBullet != null)
        {
            Destroy(gameObject);
            infection.Counter();
        }
        if (player != null)
        {
            StartCoroutine(TouchedPlayer());
        }
    }
    public float GetDamage()
    {
        return damage;
    }
    public IEnumerator TouchedPlayer()
    {
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        speed = 4f;
    }
    private void FlipEnemySpriteTowardPlayer()
    {
        if (targetPlayer == null) return;

        // Get direction to the player
        Vector2 directionToPlayer = targetPlayer.transform.position - transform.position;

        // Flip the enemy sprite based on the direction
        if (directionToPlayer.x > 0 && transform.localScale.x < 0)  // Player is on the right and the enemy is flipped
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionToPlayer.x < 0 && transform.localScale.x > 0)  // Player is on the left and the enemy is not flipped
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
