using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IPlayer
{
    float IPlayer.speed => speed;
    float IPlayer.health => health;
    [SerializeField] private float speed = 5f;
    [SerializeField] public float health = 100f;
    [SerializeField] private int maxBullets = 6; // Max regular ammo (weapon's bullet capacity)
    [SerializeField] private int maxPenBullets = 4; // Max pen ammo (weapon's pen bullet capacity)
    [SerializeField] public int currentBullets = 3;
    [SerializeField] public int currentPenBullets = 1;
    [SerializeField] private Gun gun;
    [SerializeField] private float InfectedTicks = 2f;
    [SerializeField] private float InfectedDamage = 1f;
    [SerializeField] private bool isInfected = true;
    private bool MovingX;
    private bool MovingUp;
    private bool MovingDown;
    [SerializeField] private Animator animator;

    private bool isInvulnerable = false;
    public float currentHealth;
    private InventoryManager inventory;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Quaternion initialRotation;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRotation = transform.rotation;
        currentHealth = health;
        inventory = GetComponent<InventoryManager>();
        if (isInfected == true) StartCoroutine(Infected());
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        if (movement.x < 0 && transform.right.x > 0)
        {
            transform.rotation = initialRotation * Quaternion.Euler(0f, 180f, 0f);
            MovingX = true;
            animator.SetBool("MovingX", MovingX);
        }
        else if (movement.x > 0 && transform.right.x < 0)
        {
            transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, 0f);
            MovingX = true;
            animator.SetBool("MovingX", MovingX);
        }
        else if (movement.x == 0)
        {
            MovingX = false;
            animator.SetBool("MovingX", MovingX);
        }
        if (movement.y > 0)
        {
            MovingUp = true;
            animator.SetBool("MovingUp", MovingUp);
        }
        else
        {
            MovingUp = false;
            animator.SetBool("MovingUp", MovingUp);
        }
        if (movement.y < 0)
        {
            MovingDown = true;
            animator.SetBool("MovingDown", MovingDown);
        }
        else
        {
            MovingDown = false;
            animator.SetBool("MovingDown", MovingDown);
        }

        // Reload Ammo or Pen Ammo
        if (Input.GetKeyDown(KeyCode.E) && inventory.Ammo > 0)  // Reload regular ammo
        {
            ReloadAmmo();
        }

        if (Input.GetKeyDown(KeyCode.R) && inventory.PenAmmo > 0)  // Reload pen ammo
        {
            ReloadPenAmmo();
        }

        // Use Medkit
        if (Input.GetKeyDown(KeyCode.Q) && inventory.MedKit > 0)
        {
            ConsumeMedkit();
        }
        if (Input.GetMouseButtonDown(0))
        {
            gun.Shoot();
        }
        if (Input.GetMouseButtonDown(1))
        {
            gun.ShootPen();
        }
    }

    // Update player movement
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    // Handle player damage and death
    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart scene on death
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }


    // Item collision logic for pickups
    void OnTriggerEnter2D(Collider2D collision)
    {
        Z_EnragedZombie eZombie = collision.gameObject.GetComponent<Z_EnragedZombie>();
        Z_Zombie zombie = collision.gameObject.GetComponent<Z_Zombie>();
        P_Ammo ammo = collision.gameObject.GetComponent<P_Ammo>();
        P_MedKit medKit = collision.gameObject.GetComponent<P_MedKit>();
        P_PenAmmo penAmmo = collision.gameObject.GetComponent<P_PenAmmo>();

        if (zombie != null)
        {
            TakeDamage(zombie.GetDamage());
        }
        if (eZombie != null)
        {
            TakeDamage(eZombie.GetDamage());
        }

        if (ammo != null)
        {
            ammo.PickUpAmmo();
        }

        if (medKit != null)
        {
            medKit.PickUpMedKit();
        }

        if (penAmmo != null)
        {
            penAmmo.PickUpPenAmmo();
        }


    }

    // Reload standard ammo
    void ReloadAmmo()
    {
        if (inventory.Ammo > 0) // Only reload if there's ammo in the inventory
        {
            int ammoToReload = 3;  // Fixed reload amount (3 bullets)
            currentBullets += ammoToReload;
            if (currentBullets > maxBullets)
            {
                currentBullets = maxBullets;
            }
            inventory.UseAmmo();
        }
    }

    // Reload pen ammo
    void ReloadPenAmmo()
    {
        if (inventory.PenAmmo > 0) // Only reload if there's pen ammo in the inventory
        {
            int penAmmoToReload = 2;  // Fixed reload amount (2 pen bullets)
            currentPenBullets += penAmmoToReload;
            if (currentPenBullets > maxPenBullets)
            {
                currentPenBullets = maxPenBullets;
            }
            inventory.UsePenAmmo();
            
        }
    }
    

    // Consume a medkit to restore health
    void ConsumeMedkit()
    {
        inventory.ConsumeMedKit();  // Consume a medkit from inventory
        currentHealth += 50f;  // Heal the player
        currentHealth = Mathf.Min(currentHealth, health);  // Cap health at max value
    }

    private IEnumerator Infected()
    {
        yield return new WaitForSeconds(InfectedTicks);
        currentHealth -= InfectedDamage;
        StartCoroutine(Infected());
        
    }
    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(2);
        isInvulnerable = false;
    }
}
