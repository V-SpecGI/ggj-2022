using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public GameObject fly;
    public float xPos;
    public int yPos;
    public int flyCount;
    private bool pause;
    private List<GameObject> flies = new List<GameObject>();
    private bool croak;
    private double health;

    void Start()
    {
        StartCoroutine((FlyDrop()));
        StartCoroutine((EatFly()));
        pause = false;
    }

    void Update()
    {   
        if (flyCount == 0) {
            croak = false;
        } else {
            croak = true;
        }
        // please that if croak = true, then knockback.
        if (croak == false ) {
            // implement frog taking damage
        }
    }

    // sometimes the flies don't show and I don't know why
    // Spawns flies randomly
    IEnumerator FlyDrop() {
        while (flyCount < 5) {
            while (pause) {
                yield return new WaitForSeconds (50f);
                pause = false;
            }
            xPos = UnityEngine.Random.Range(-3, 15); // add 1 to max
            yPos = UnityEngine.Random.Range(-11, 8); 
            flies.Add(Instantiate(fly, new Vector2(xPos, yPos), Quaternion.identity));
            yield return new WaitForSeconds(0.5f); // 1/2 of a second betweeen spawns
            flyCount += 1;
        }
    }

    IEnumerator EatFly() {
        if (flyCount == 5) {
            while (flyCount > 0) {
                yield return new WaitForSeconds(0.5f);
                pause = true;
                //eat fly animation
                System.Random rnd = new System.Random();
                int index = rnd.Next(0,6);
                Destroy(flies[index]);
                flies.RemoveAt(index);
                yield return new WaitForSeconds(1f);
                // throw fire projectile
                yield return new WaitForSeconds(1f);
                flyCount -= 1;
            }
        }
        pause = false;
    }
}
