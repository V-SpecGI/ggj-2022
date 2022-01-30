using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake(){
        if(LevelManager.instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void GameOver(){
            UIManager ui = GetComponent<UIManager>();
            if(ui != null){
                ui.ToggleDeathPanel();
            }
    }
}
