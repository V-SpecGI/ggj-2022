using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputController _playerActions;
    [SerializeField]
    float _horizontal;
    [SerializeField]
    float _vertical;
    [SerializeField]
    Vector3 rotation;
    public bool UseController = true;
    private void Awake()
    {
        _playerActions = new PlayerInputController();
    }
    private void OnEnable()
    {
        _playerActions.Gameplay.ControllerMove.Enable();
        _playerActions.Gameplay.KeyboardMove.Enable();
    }
    private void OnDisable()
    {
        _playerActions.Gameplay.ControllerMove.Disable();
        _playerActions.Gameplay.KeyboardMove.Disable();

    }
    void FixedUpdate()
    {
        Input();
    }

    void Input()
    {
        Vector2 tempMove = Vector2.zero;
        if (!UseController)
        {
            tempMove = _playerActions.Gameplay.KeyboardMove.ReadValue<Vector2>();
        }
        else
        {
            tempMove = _playerActions.Gameplay.ControllerMove.ReadValue<Vector2>();
        }
        _horizontal = tempMove.x;
        _vertical = tempMove.y;
    }

    public float Horizontal { get { return _horizontal; } }
    public float Vertical { get { return _vertical; } }
}
