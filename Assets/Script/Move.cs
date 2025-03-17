using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    //player Move
    [SerializeField] private float gravityMultiplier = 3.0f;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float jumpPower;
    [SerializeField] private Movement movement;
    private Vector2 input;
    private CharacterController characterController;
    private Vector3 direction;
    private float velocity;
    private float gravity = -9.81f;
    public Animator animator;



    private Camera mainCamera;

    [Serializable]
    public struct Movement
    {
        public float speed;
        public float multiplier;
        public float accell;
        [HideInInspector] public bool isSprinting;
        [HideInInspector] public float currentSpeed;

    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

    }

    void Start()
    {
        
    }


    void Update()
    {
        
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
        UpdateAnimation();
    }


    private void ApplyGravity()
    {
        if (IsGrounded() && velocity < 0f)
        {
            velocity = -1f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;

        }
        direction.y = velocity;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        movement.isSprinting = context.started || context.performed;
    }

    private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;

        direction = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f) * new Vector3(input.x, 0f, input.y);
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void ApplyMovement()
    {
        var targetSpeed = movement.isSprinting ? movement.speed * movement.multiplier : movement.speed;
        movement.currentSpeed = Mathf.MoveTowards(movement.currentSpeed, targetSpeed, movement.accell * Time.deltaTime);
        characterController.Move(motion: direction * movement.currentSpeed * Time.deltaTime);

    }

    public void Moving(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, y: 0f, z: input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        velocity += jumpPower;
        animator.SetTrigger("jump");

    }

    private bool IsGrounded() => characterController.isGrounded;

    
    private void UpdateAnimation()
    {
        /*if (!IsGrounded())
        {
            animator.SetTrigger("jump");
        }
        else*/
        if (input.sqrMagnitude == 0)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            animator.SetTrigger("walk");
        }
        if (movement.isSprinting)
        {
            animator.SetTrigger("run");
            animator.ResetTrigger("walk");
        }
    }

    

}
