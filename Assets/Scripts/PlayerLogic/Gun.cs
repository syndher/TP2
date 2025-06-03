using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject penBulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] PlayerMovement player;
    PenBulletsDisplay penBulletsDisplay;
    BulletsDisplay bulletsDisplay;
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        mouseWorldPos.z = 0f;

        // Rotate gun to face mouse
        Vector3 direction = mouseWorldPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (mouseWorldPos.x < transform.position.x)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

    }
    public void Shoot()
{
    if (bulletPrefab != null && shootingPoint != null)
    {
        if (player.currentBullets > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootingPoint.position).normalized;
                rb.linearVelocity = direction * bulletSpeed;

                // Rotate bullet to face the direction
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            player.currentBullets--;
        }
    }
}

    public void ShootPen()
    {
        if (penBulletPrefab != null && shootingPoint != null)
        {
            if (player.currentPenBullets > 0)
            {
                GameObject bullet = Instantiate(penBulletPrefab, shootingPoint.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - shootingPoint.position).normalized;
                    rb.linearVelocity = direction * bulletSpeed;

                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                player.currentPenBullets--;

            }

        }
    }
}
