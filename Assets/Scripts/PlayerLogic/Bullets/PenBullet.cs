using UnityEngine;

public class PenBullet : MonoBehaviour
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

}
