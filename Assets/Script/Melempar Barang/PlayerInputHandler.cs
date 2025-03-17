using UnityEngine;
using UnityEngine.InputSystem;
using ProjectileCurveVisualizerSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float pickUpRange = 2.0f; // Jarak pengambilan objek
    private PickAndThrowController throwController;

    void Start()
    {
        throwController = GetComponent<PickAndThrowController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("Throwable"))
            {
                Debug.Log("Mengambil objek: " + hit.collider.name);
                throwController.PickUpObject(hit.collider.gameObject);
            }
        }
    }
}
