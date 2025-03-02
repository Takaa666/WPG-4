using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingObject : MonoBehaviour
{
   
    [Header("Push Settings")]

    [Range(-10.0f, 100.0f)]
    public float PushPower = 10.0f;
    [Range(-10.0f, 10.0f)]
    public float PushDistance = 2.8f;
    //public Animator animator;
    Vector3 Direction;
    private bool ObjectMoving = false;

    private bool CanPush = false;


    void Start()
    {

    }

    void Update()
    {
        PushAction();
    }

    private void PushAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CanPush = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CanPush = false;
            ObjectMoving = false;
            //animator.SetBool("pushing", false); // deactivating the push animation
        }

        if (ObjectMoving)
        {
            //animator.SetBool("pushing", true); // activating the push animation
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody Body = hit.collider.attachedRigidbody;

        if (Body == null || Body.tag != "Pushable") { return; } // check tag

        if (hit.moveDirection.y < -0.3) { return; }

        Body.GetComponent<Rigidbody>().freezeRotation = true;

        float Dist = Vector3.Distance(Body.gameObject.transform.position, this.transform.position);

        
        Vector3 PushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);


        if (CanPush && Dist < PushDistance)
        {
            Body.isKinematic = false;
            PushingObject.ApplyForceToReachVelocity(Body, PushDirection.normalized * PushPower, 0.1f);
            ObjectMoving = true;
        }
        else
        {
            ObjectMoving = false;
            Body.isKinematic = true;
            Body.GetComponent<Rigidbody>().freezeRotation = false;
        }

        if (Body.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
        {
            ObjectMoving = true;
        }
        else
        {
            ObjectMoving = false;
        }
    }


    private static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Impulse)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}
