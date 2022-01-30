using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_loader : MonoBehaviour
{
    public int level_number;

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject collision_game_object = collision.gameObject;

        if (collision_game_object.name == "Player") {
            SceneManager.LoadScene(level_number);
        }
    }
}

/*
Every scene, add gameobject called "start_position" where you want to start.

Add,
DontDestroyOnLoad(gameObject);
in void Start() method of player_movement code.

Add the corresponding code on player_movement code.
private void OnLevelWasLoaded(int level) {
    transform.position = GameObject.Find("start_position").transform.position;
}
*/