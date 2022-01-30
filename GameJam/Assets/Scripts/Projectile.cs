using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public static Health instance;
    public float speed;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        transform.right = player.position - transform.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D collide)
    {
        print("Collide: " + collide.name);
        if (collide.CompareTag("Player"))
        {
            Health.instance.reduceHealth(0.5);
            DestroyProjectile();
        }
        else if(collide.CompareTag("Shield"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
