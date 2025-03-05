using UnityEngine;
using UnityEngine.InputSystem;
using ProjectileCurveVisualizerSystem;


public class ThrowableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        if (rb == null)
        {
            Debug.LogError(gameObject.name + " needs a Rigidbody component!");
        }
        if (col == null)
        {
            Debug.LogError(gameObject.name + " needs a Collider component!");
        }
    }

    public void PickUp(Transform holdPosition)
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero; // Hentikan pergerakan
        }

        if (col != null)
        {
            col.enabled = false; // Nonaktifkan collider sementara
        }

        transform.SetParent(holdPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        if (col != null)
        {
            col.enabled = true; // Aktifkan kembali collider
        }

        transform.SetParent(null);
    }

    public void Throw(Vector3 force)
    {
        Drop();
        if (rb != null)
        {
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
