using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public int maxHealth = 6;
    public int minHealth = 0;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite halfHeart;
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
        
        public void reduceHealth(int reduction) {
            currentHealth -= reduction;
            if (currentHealth < minHealth) {
                currentHealth = minHealth
                // We could have the player die here
            }
        }

        public void increaseHealth(int increment) {
            currentHealth += increment;
            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }
        }
    }
}
