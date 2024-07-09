using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public projectile projectileData;

    public string[] targetTags;

    public Rigidbody rb;

    private Vector3 direction;


    void Start()
    {
        Destroy(gameObject, projectileData.projectileLifetime);
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            rb.velocity = direction * projectileData.projectileSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in targetTags)
        {
            Debug.Log(tag);
            if (other.CompareTag(tag) && other.gameObject.GetComponent<HealthManager>())
            {
                other.GetComponent<HealthManager>().TakeDamage(projectileData.damage);
            }
        }
        Debug.Log(other.gameObject.tag + " " + other.gameObject.name);

        if (projectileData.impactEffect)
        {
            Instantiate(projectileData.impactEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    public void addTargetTag(string tag)
    {
        List<string> tags = new List<string>(targetTags);
        tags.Add(tag);
        targetTags = tags.ToArray();
    }   
    
}
