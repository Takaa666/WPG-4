using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class MovementAnimation : MonoBehaviour
{
    #region Variables: Movement

    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    private Animator _animator;

    [SerializeField] private float speed;

    #endregion
    #region Variables: Rotation

    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    #endregion
    #region Variables: Gravity

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    #endregion
    #region Variables: Jumping

    [SerializeField] private float jumpPower;

    #endregion
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
        UpdateAnimation();
    }

    private void ApplyGravity()
    {
        Debug.Log("Is grounded: " + IsGrounded());
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }
        
        _direction.y = _velocity;
    }
    
    private void ApplyRotation()
    {
        if (_input.sqrMagnitude == 0) return;
        
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        Debug.Log("Key pressed: " + _input);
        Vector3 horizontalMovement = new Vector3(_direction.x, 0.0f, _direction.z);
        _characterController.Move(horizontalMovement * speed * Time.deltaTime + Vector3.up * _velocity * Time.deltaTime);
    }

    private void UpdateAnimation()
    {
        if (!IsGrounded())
        {
            _animator.SetTrigger("jump");
        }
        else if (_input.sqrMagnitude == 0)
        {
            _animator.SetTrigger("idle");
        }
        else
        {
            _animator.SetTrigger("walk");
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
        if (!context.started) return;
        if (!IsGrounded()) return;

        _velocity += jumpPower;
        _animator.SetTrigger("jump");
    }

    private bool IsGrounded() => _characterController.isGrounded;
}
