using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectManager : MonoBehaviour
{

    public static CollectManager instance;

    public Text collectText;
    int collected = 0;

    private void Awake(){
        instance = this;
    }

    void Start()
    {
        collectText.text = collected.ToString() + " POINTS";
    }

    public void addCollect()
    {
        collected++;
        collectText.text = collected.ToString() + " POINTS";
    }
}
