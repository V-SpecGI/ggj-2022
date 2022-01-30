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

    void OnPlayerEnter(Collider2D other){
        OnTriggerEnter2D(other);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CollectManager.instance.addCollect();
            Destroy(gameObject);
        }
    }
}
