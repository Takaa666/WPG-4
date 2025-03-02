using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class CameraFollow : MonoBehaviour
{
    #region.Variables
    [SerializeField] private Transform target;
    
    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;
    
    private float distanceToPlayer;
    private Vector2 input;
    private CameraRotation cameraRotation;

    #endregion
    private void Awake() => distanceToPlayer = Vector3.Distance(transform.position, target.position);


    private void Update()
    {
        cameraRotation.Yaw += input.x * mouseSensitivity.horizontal * Time.deltaTime;
        cameraRotation.Pitch += input.y * mouseSensitivity.vertical * Time.deltaTime;
        cameraRotation.Pitch = Mathf.Clamp(cameraRotation.Pitch, cameraAngle.min, cameraAngle.max);
    }

    public void Look(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(cameraRotation.Pitch, cameraRotation.Yaw, 0f);
        transform.position = target.position - transform.forward * distanceToPlayer;
    }

    [Serializable]
    public struct MouseSensitivity
    {
        public float horizontal;
        public float vertical;
    }

    public struct CameraRotation
    {
        public float Pitch;
        public float Yaw;
    }

    [Serializable]

    public struct CameraAngle
    {
        public float min;
        public float max;
    }

    /*public Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    public Vector2 turn;

    // Start is called before the first frame update
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }*/
}
