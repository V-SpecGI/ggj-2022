using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed;
    private Vector2 move;
    public Transform player;
    private Rigidbody2D body;
    private void Start() {
        body = this.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Player"))
        {
            Health.instance.reduceHealth(0.5);
        }
    }
/*
    void OnCollisionEnter2D(Collision2D collide){
        float knockback = 3500;
        Vector2 dir = (transform.position - collide.transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * knockback);
    }
*/
    private void Update() {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        direction.Normalize();
        move = direction;
    }

    private void FixedUpdate(){
        moveChar(move);
    }
    
    void moveChar(Vector2 direction){
        body.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
