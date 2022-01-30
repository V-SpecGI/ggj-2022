//Work in progress
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Health instance;

    [SerializeField] private float invDur;
    [SerializeField] private float invDelta = 0.15f;
    [SerializeField] private GameObject model;
    private bool isInv = false;
    private float health;
    private float newHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        newHealth = Health.instance.getHealth();
        if(health != newHealth){
            StartCoroutine(StartInv());
        }
    }

    void TriggerInvuln(){
        if(!isInv){
            StartCoroutine(StartInv());
        }
    }

    private IEnumerator StartInv(){
        isInv = true;
        for(float i = 0; i < invDur; i+= invDelta)
        {
            if(model.transform.localScale == Vector3.one){
                ScaleModel(Vector3.zero); 
            }else{
                ScaleModel(Vector3.one);
            }
            yield return new WaitForSeconds(invDelta);
        }
        ScaleModel(Vector3.one);
        isInv = false;
    }

    private void ScaleModel(Vector3 scale){
        model.transform.localScale = scale;
    }
}
