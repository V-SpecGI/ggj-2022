using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum DirectionFacing { Up, Down, Left, Right}
    public DirectionFacing Direction;
    public string[] AnimatorBoolNames;
    private Rigidbody2D body;
    private InputManager input;
    private float _horizontal;
    private float _vertical;
    private float _moveLimiter = 0.7f;
    [SerializeField]private Vector2 _velocity = Vector2.zero;

    [SerializeField][Range(.01f,.5f)] float smoothTime = 0.2f;
    [SerializeField] [Range(.01f, 20)] private float runSpeed;

    Animator animator;
    void Start()
    {
        input = GetComponent<InputManager>();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
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
        _velocity = Vector2.SmoothDamp(body.velocity, targetVelocity, ref _velocity, smoothTime);
        body.velocity = _velocity;
        SetAnimator();
    }
    void SetAnimator()
    {
        if (_horizontal == 0 && _vertical == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                animator.SetBool(AnimatorBoolNames[i], false);
            }
            return;
        }
        else if(_vertical > 0)
            Direction = DirectionFacing.Up;
        else if(_vertical < 0)
            Direction = DirectionFacing.Down;
        else if(_horizontal > 0)
            Direction = DirectionFacing.Right;
        else if(_horizontal < 0)
            Direction = DirectionFacing.Left;
        for(int i = 0; i < 4; i++)
        {
            animator.SetBool(AnimatorBoolNames[i], i == (int)Direction);
        }
    }
}
