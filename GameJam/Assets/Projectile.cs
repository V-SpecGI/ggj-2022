using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //target = new Vector2(player.position.x, player.position.y);
        transform.right = player.position - transform.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }
    
    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Player"))
        {
            DestroyProjectile();
        }
        /* else if (collide.CompareTag("Tree"))
        {
            DestroyProjectile();
        }
        
         */
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
