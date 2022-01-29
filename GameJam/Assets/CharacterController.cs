using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerInput input;
    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;
    private Vector2 _velocity = Vector2.zero;

    [SerializeField][Range(.01f,.5f)] float smoothTime = 0.2f;
    [SerializeField] [Range(.01f, 20)] private float runSpeed;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        body = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        // Gives a value between -1 and 1
        _horizontal = input.Horizontal; // -1 is left
        _vertical = input.Vertical; // -1 is down
    }

    void FixedUpdate()
    {
        if (_horizontal != 0 && _vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            _horizontal *= _moveLimiter;
            _vertical *= _moveLimiter;
        }
        Vector2 targetVelocity = new Vector2(_horizontal * runSpeed, _vertical * runSpeed);
        body.velocity = Vector2.SmoothDamp(body.velocity, targetVelocity, ref _velocity, smoothTime);
    }
}
