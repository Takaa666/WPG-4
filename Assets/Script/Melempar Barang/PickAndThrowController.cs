using UnityEngine;
using ProjectileCurveVisualizerSystem;
using UnityEngine.InputSystem;

public class PickAndThrowController : MonoBehaviour
{
    public Transform holdPosition; // Posisi untuk menyimpan objek sebelum dilempar
    public ProjectileCurveVisualizer projectileCurveVisualizer; // Visualisasi lintasan lempar

    private GameObject heldObject = null; // Objek yang sedang dipegang
    private bool inProjectileMode = false;
    private float throwForce = 10.0f;
    private Vector3 launchVelocity;
    private Vector3 updatedProjectileStartPosition;
    private RaycastHit hit;

    void Update()
    {
        if (heldObject != null)
        {
            // Masuk/Keluar mode projectile
            if (Input.GetKeyDown(KeyCode.R))
            {
                inProjectileMode = !inProjectileMode;
                if (!inProjectileMode)
                    projectileCurveVisualizer.HideProjectileCurve();
            }

            // Jika dalam mode projectile, atur lintasan
            if (inProjectileMode)
            {
                AdjustProjectileStrength();
                HandleProjectileMode();

                // Lempar objek jika klik kiri
                if (Input.GetMouseButtonUp(0))
                {
                    ThrowObject();
                }
            }
        }
    }

    void AdjustProjectileStrength()
    {
        // Mengubah kekuatan lempar dengan scroll mouse
        throwForce = Mathf.Clamp(throwForce + Input.GetAxis("Mouse ScrollWheel") * 6.0f, 1.0f, 100.0f);
    }

    void HandleProjectileMode()
    {
        launchVelocity = transform.forward * throwForce;

        projectileCurveVisualizer.VisualizeProjectileCurve(
            holdPosition.position,
            1.0f,
            launchVelocity,
            0.25f,
            0.1f,
            true,
            out updatedProjectileStartPosition,
            out hit
        );
    }

    public void PickUpObject(GameObject obj)
    {
        if (heldObject == null)
        {
            heldObject = obj;
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true; // Matikan physics sementara
                rb.velocity = Vector3.zero;  // Hentikan gerakan sebelumnya
            }

            heldObject.transform.SetParent(holdPosition);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;
        }
    }

    void ThrowObject()
    {
        if (heldObject != null)
        {
            inProjectileMode = false;
            projectileCurveVisualizer.HideProjectileCurve();

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Aktifkan physics
                rb.AddForce(launchVelocity, ForceMode.Impulse);
            }

            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
