using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputController _playerActions;
    public event System.Action SwapButtonPressedEvent;
    public event System.Action Button1PressedEvent;
    public event System.Action Button2PressedEvent;
    public bool Button1Held = false, Button2Held = false;
    [SerializeField]
    float _horizontal;
    [SerializeField]
    float _vertical;
    [SerializeField]
    Vector3 rotation;
    private void Awake()
    {
        _playerActions = new InputController();
        _playerActions.Gameplay.Swap.performed += ctx => SwapButtonPressed();
        _playerActions.Gameplay.Button1.performed += ctx => Button1Pressed();
        _playerActions.Gameplay.Button1.canceled += ctx => Button1Released();
        _playerActions.Gameplay.Button2.performed += ctx => Button2Pressed();
        _playerActions.Gameplay.Button2.canceled += ctx => Button2Released();
    }
    private void OnEnable()
    {
        _playerActions.Gameplay.Move.Enable();
        _playerActions.Gameplay.Swap.Enable();
        _playerActions.Gameplay.Button1.Enable();
        _playerActions.Gameplay.Button2.Enable();
    }
    private void OnDisable()
    {
        _playerActions.Gameplay.Move.Disable();
        _playerActions.Gameplay.Swap.Disable();
        _playerActions.Gameplay.Button1.Disable();
        _playerActions.Gameplay.Button2.Disable();

    }
    void FixedUpdate()
    {
        Input();
    }

    void Input()
    {
        Vector2 tempMove = _playerActions.Gameplay.Move.ReadValue<Vector2>();
        _horizontal = tempMove.x;
        _vertical = tempMove.y;
    }
    void SwapButtonPressed()
    {
        SwapButtonPressedEvent();
    }
    void Button1Pressed()
    {
        Button1PressedEvent();
        Button1Held = true;
    }
    void Button1Released()
    {
        Button1Held = false;
    }
    void Button2Pressed()
    {
        Button2PressedEvent();
        Button2Held = true;
    }
    void Button2Released()
    {
        Button2Held = false;
    }

    public float Horizontal { get { return _horizontal; } }
    public float Vertical { get { return _vertical; } }
}
