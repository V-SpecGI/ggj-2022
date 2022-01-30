using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearShieldManager : MonoBehaviour
{
    public enum SpearShieldState { Spear, Shield, None}
    public SpearShieldState state = SpearShieldState.None;
    CharacterController characterController;
    InputManager inputManager;
    Animator animator;
    public float shieldBashDistance, shieldBashSpeed, shieldBashPlayerStunTime, shieldMeter, shieldMeterMax, shieldDepletionRate;
    public bool isBashing = false, isShielding = false;
    Coroutine currentCoroutine;
    Rigidbody2D rb;
    public GameObject BigShield;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputManager.SwapButtonPressedEvent += SwapWeapons;
        inputManager.Button1PressedEvent += Button1;
        inputManager.Button2PressedEvent += Button2;
    }
    void Button1()
    {
        print("Button 1");
        switch (state)
        {
            case SpearShieldState.None:

                break;
            case SpearShieldState.Shield:
                Shield();
                break;
            case SpearShieldState.Spear:
                
                break;
        }
    }
    void Button2()
    {
        print("Button 2");
        switch (state)
        {
            case SpearShieldState.None:
                break;
            case SpearShieldState.Shield:
                ShieldBash();
                break;
            case SpearShieldState.Spear:
                
                break;
        }
    }
    void SwapWeapons()
    {
        print("Swap");
        switch(state)
        {
            case SpearShieldState.None:
                break;
            case SpearShieldState.Shield:
                state = SpearShieldState.Spear;
                break;
            case SpearShieldState.Spear:
                state = SpearShieldState.Shield;
                break;
        }
        UpdateAnimator();
    }
    void Shield()
    {
        if (currentCoroutine != null)
        {
            return;
        }
        currentCoroutine = StartCoroutine(ShieldAnimation());
        print("Shield");
    }
    IEnumerator ShieldAnimation()
    {
        isShielding = true;
        print("Start Shield");
        BigShield.SetActive(true);
        while(inputManager.Button1Held)
        {
            print(shieldMeter);
            shieldMeter -= Time.deltaTime * shieldDepletionRate;
            yield return null;
        }
        currentCoroutine = null;
        isShielding = false;
        BigShield.SetActive(false);
    }
    void ShieldBash()
    {
        if (currentCoroutine != null || isBashing)
        {
            return;
        }
            isBashing = true;
            currentCoroutine = StartCoroutine(shieldBashAnimation());
        print("bash");
    }
    IEnumerator shieldBashAnimation()
    {
        float distanceLeft = shieldBashDistance;
        characterController.DisableControl(false);
        Vector2 direction = Vector2.zero;
        switch(characterController.Direction)
        {
            case CharacterController.DirectionFacing.Up:
                direction = Vector2.up;
                break;
            case CharacterController.DirectionFacing.Down:
                direction = Vector2.down;
                break;
            case CharacterController.DirectionFacing.Left:
                direction = Vector2.left;
                break;
            case CharacterController.DirectionFacing.Right:
                direction = Vector2.right;
                break;
        }
        while(distanceLeft > 0)
        {
            rb.velocity = direction * shieldBashSpeed;
            distanceLeft -= direction.magnitude * shieldBashSpeed * Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(shieldBashPlayerStunTime);
        currentCoroutine = null;
        characterController.DisableControl(true);
        isBashing = false;
    }
    void SpearPoke()
    {

    }
    void UpdateAnimator()
    {
        animator.SetInteger("SpearShieldState", (int)state);
    }
}
