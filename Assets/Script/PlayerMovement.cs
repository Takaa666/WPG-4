using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEditor.Progress;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    //player Move
    [SerializeField] private float gravityMultiplier = 3.0f;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float jumpPower;
    private Vector2 input;
    private CharacterController characterController;
    private Vector3 direction;
    private float velocity;
    private float gravity = -9.81f;
    private Animator animator;


    //player Pick Up
    [SerializeField] private LayerMask pickableLayerMask, pickToInventoryLayerMask;
    [SerializeField] private GameObject pickUpUI, pickToInventoryUI;
    [SerializeField][Min(1)] private float hitRange = 3;
    [SerializeField] private Transform pickUpParent;
    [SerializeField] private GameObject inHandItem;
    [SerializeField] private InputActionReference interactionInput, dropInput, pickToInventory, submitButton;
    private RaycastHit hit;


    public Text totalItemText;


    private Camera mainCamera;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        interactionInput.action.performed += Interact;
        dropInput.action.performed += Drop;
        pickToInventory.action.performed += Loot;
    }


    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
            pickToInventoryUI.SetActive(false);
        }



        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out hit,
            hitRange,
            pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);

        }

        if (Physics.Raycast(
            transform.position,
            transform.forward,
            out hit,
            hitRange,
            pickToInventoryLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickToInventoryUI.SetActive(true);

        }

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

    private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;

        direction = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f) * new Vector3(input.x, 0f, input.y);
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void ApplyMovement()
    {
        characterController.Move(motion: direction * speed * Time.deltaTime);

    }

    public void Move(InputAction.CallbackContext context)
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

    public void Drop(InputAction.CallbackContext obj)
    {
        if (inHandItem != null)
        {
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private void UpdateAnimation()
    {
        /*if (!IsGrounded())
        {
            animator.SetTrigger("jump");
        }
        else*/ if (input.sqrMagnitude == 0)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            animator.SetTrigger("walk");
        }
    }

    public void Interact(InputAction.CallbackContext obj)
    {
        //Debug.Log(hit.collider.name);
        if (hit.collider == null) return;
        Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
        if (hit.collider.GetComponent<Item>())
        {
            Debug.Log("It's a useless item!");
            inHandItem = hit.collider.gameObject;
            inHandItem.transform.SetParent(pickUpParent.transform, true);

            if (rb != null)
            {
                rb.isKinematic = true;
            }
            return;
        }
    }



    public void Loot(InputAction.CallbackContext obj)
    {
        if (hit.collider == null) return;
        Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

        if (hit.collider.GetComponent<PickToInventory>().enabled)
        {
            QuestLog questLog = FindObjectOfType<QuestLog>();
            if (questLog != null && !questLog.CanLoot(hit.collider.gameObject))
            {
                Debug.Log("Cannot loot item: " + hit.collider.gameObject.name + " because the quest is not in progress or already completed.");
                return;
            }

            Debug.Log("Claiming Item: " + hit.collider.gameObject.name);
            

            if (questLog != null)
            {
                questLog.CheckTargetObjectDestruction(hit.collider.gameObject);
            }

            DestroyImmediate(hit.collider.gameObject); // Destroy the item after adding to inventory
            itemScore();
            totalItemText.text = TotalItem.totalItem.ToString();
        }
    }





    public static void itemScore()
    {
        TotalItem.totalItem += 1;
    }

    
}
