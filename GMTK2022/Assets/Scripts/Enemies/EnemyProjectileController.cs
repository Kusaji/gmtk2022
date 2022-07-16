using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [Header("Attributes")]
    public float damage;
    public float projectileSpeed;

    [Header("Prefabs")]
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        else if (other.gameObject.CompareTag("Environment") || other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }
}
