using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    public static int collectible = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            addCollect();
            Destroy(gameObject);
        }
    }

    void addCollect()
    {
        collectible++;
        Debug.Log("Coin! " + collectible);
    }
}
