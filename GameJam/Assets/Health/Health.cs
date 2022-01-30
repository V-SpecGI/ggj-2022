using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart; 
    public Sprite halfHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > numOfHearts) {
            health = numOfHearts;
        }

        
        float remainder = health % 1;

        if (remainder == 0) {
            for (int i = 0; i < hearts.Length; i++) {
                
                if (i < health) {
                    hearts[i].sprite = fullHeart;
                } else {
                    hearts[i].sprite = emptyHeart;
                }
                if (i < numOfHearts) {
                    hearts[i].enabled = true;
                } else {
                    hearts[i].enabled = false;
                }
            }
        } else {
            for (int i = 0; i < hearts.Length; i++) {
                
                if (i + 1 < health) {
                    hearts[i].sprite = fullHeart;
                } else if (remainder != 0) {
                    hearts[i].sprite = halfHeart;
                    remainder = 0;
                }
                else {
                    hearts[i].sprite = emptyHeart;
                }
                if (i < numOfHearts) {
                    hearts[i].enabled = true;
                } else {
                    hearts[i].enabled = false;
                }
            }
        }
    }
    
    void reduceHealth(double reduction) {
        if (reduction % 1 != 0) {
            reduction = Math.Truncate(reduction) + 0.5;
        }
        health -= (float) reduction;
        if (health < 0) {
            health = 0;
        }
    }


    void increaseHealth(double increment) {
        if (increment % 1 != 0) {
            increment = Math.Truncate(increment) + 0.5;
        }
        health += (float) increment;
        if (health > numOfHearts) {
            health = 3;
        }
    }

    float getHealth() {
        return health;
    }
    
}