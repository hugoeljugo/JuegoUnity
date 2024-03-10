using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private LayerMask _layer;
    
    private float _dirX;
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 14f;

    private enum MovementState
    {
        Idle, Run, Jump, Fall
    }

    [SerializeField] private AudioSource _jumpSound;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.velocity = new Vector2(_dirX * _moveSpeed, _rigidbody2D.velocity.y);
            
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            _jumpSound.Play();
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        switch (_dirX)
        {
            case > 0:
                state = MovementState.Run;
                _spriteRenderer.flipX = false;
                break;
            case < 0:
                state = MovementState.Run;
                _spriteRenderer.flipX = true;
                break;
            default:
                state = MovementState.Idle;
                break;
        }

        state = _rigidbody2D.velocity.y switch
        {
            > .1f => MovementState.Jump,
            < -.1f => MovementState.Fall,
            _ => state
        };
        _animator.SetInteger("State", (int) state);
    }

    private bool IsGrounded()
    {
        var bounds = _boxCollider.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.down, .1f, _layer);
    }
}
