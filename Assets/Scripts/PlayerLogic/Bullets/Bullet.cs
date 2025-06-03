using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifespan = 3f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
    void Update()
    {      
        transform.Translate(direction * speed * Time.deltaTime);

        Destroy(gameObject, lifespan);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Z_Zombie zombie = collision.gameObject.GetComponent<Z_Zombie>();
        Z_EnragedZombie eZombie = collision.gameObject.GetComponent<Z_EnragedZombie>();
        if (zombie != null || eZombie != null)
        {
            Destroy(gameObject);
        }
        
    }
}
