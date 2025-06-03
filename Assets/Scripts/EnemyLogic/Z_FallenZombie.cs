
using System.Collections;
using UnityEngine;

public class Z_FallenZombie : MonoBehaviour, IZombie
{

    [SerializeField] private float speed = 0f;
    float IZombie.speed => speed;
    [SerializeField] private float timeToTransform = 60f;
    [SerializeField] private GameObject newEnragedZombiePrefab;

    private IEnumerator Transforming(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(newEnragedZombiePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(Transforming(timeToTransform));
    }

}
