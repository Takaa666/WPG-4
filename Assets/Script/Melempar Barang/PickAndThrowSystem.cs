using System.Collections;
using System.Collections.Generic;
using ProjectileCurveVisualizerSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickAndThrowSystem : MonoBehaviour
{
    public Transform holdPosition; // Posisi objek yang dipegang
    public float pickUpRange = 2.0f; // Jarak maksimal untuk mengambil objek
    public float throwForce = 10.0f; // Kekuatan lemparan
    public ProjectileCurveVisualizer projectileCurveVisualizer;

    private GameObject heldObject;
    private bool inProjectileMode = false;
    private Vector3 launchVelocity;
    private Vector3 updatedProjectileStartPosition;
    private RaycastHit hit;
    private Collider objCollider;

    [Header("Layer Mask")]
    public LayerMask pickableLayer; // Opsional: Layer objek yang bisa diambil

    // 🔹 Input untuk mengambil / melepaskan objek
    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Tombol Pick Up ditekan!");
            if (heldObject == null)
                TryPickup();
            else
                DropObject();
        }
    }

    // 🔹 Input untuk masuk ke mode lempar
    public void OnEnterThrowMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Right Mouse Button ditekan! OnEnterThrowMode dipanggil.");

            if (heldObject != null)
            {
                inProjectileMode = !inProjectileMode;
                Debug.Log("Mode Lempar Sekarang: " + inProjectileMode);

                if (!inProjectileMode)
                    projectileCurveVisualizer.HideProjectileCurve();
            }
            else
            {
                Debug.Log("Tidak ada objek yang sedang dipegang, tidak bisa masuk ke mode lempar.");
            }
        }
    }

    // 🔹 Input untuk melempar objek
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed && heldObject != null && inProjectileMode)
        {
            inProjectileMode = false;
            projectileCurveVisualizer.HideProjectileCurve();
            ThrowObject();
        }
    }

    // 🔹 Input untuk menyesuaikan kekuatan lemparan
    public void OnAdjustThrowStrength(InputAction.CallbackContext context)
    {
        if (inProjectileMode)
        {
            float value = context.ReadValue<float>();
            throwForce = Mathf.Clamp(throwForce + value * 6.0f, 1.0f, 100.0f);
            Debug.Log("Kekuatan lempar sekarang: " + throwForce);
        }
    }

    // 🔹 Mencoba mengambil objek di depan pemain
    void TryPickup()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 1.5f; // Geser sedikit ke atas
        Debug.DrawRay(rayOrigin, transform.forward * pickUpRange, Color.red, 5f); // Debugging garis raycast

        // Menggunakan SphereCast untuk area deteksi lebih besar
        if (
            Physics.SphereCast(
                rayOrigin,
                0.5f,
                transform.forward,
                out hit,
                pickUpRange,
                pickableLayer
            )
        )
        {
            Debug.Log("Raycast mengenai: " + hit.collider.name);

            if (hit.collider.CompareTag("Throwable"))
            {
                Debug.Log("Objek valid untuk diambil!");
                heldObject = hit.collider.gameObject;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                objCollider = heldObject.GetComponent<Collider>();

                if (rb == null)
                {
                    Debug.LogWarning("Objek tidak memiliki Rigidbody! Tidak bisa diambil.");
                    heldObject = null;
                    return;
                }

                // Nonaktifkan fisika agar tidak jatuh
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;

                if (objCollider != null)
                    objCollider.enabled = false; // Matikan collider sementara agar tidak bertabrakan

                // Atur posisi dan rotasi objek yang dipegang
                heldObject.transform.SetParent(holdPosition);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;

                Debug.Log("Mengambil objek: " + heldObject.name);
            }
            else
            {
                Debug.Log("Objek terkena raycast tetapi bukan Throwable. Tag: " + hit.collider.tag);
            }
        }
        else
        {
            Debug.Log("Raycast tidak mengenai objek.");
        }
    }

    // 🔹 Melepaskan objek yang sedang dipegang
    void DropObject()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = false;

            if (objCollider != null)
                objCollider.enabled = true; // Aktifkan kembali collider setelah dilepas

            heldObject.transform.SetParent(null);
            heldObject = null;

            Debug.Log("Objek dilepas.");
        }
    }

    // 🔹 Update mode lempar saat aktif
    void Update()
    {
        if (heldObject != null && inProjectileMode)
        {
            HandleProjectileMode();
        }
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

    // 🔹 Melempar objek
    void ThrowObject()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                heldObject.transform.SetParent(null);
                rb.AddForce(launchVelocity, ForceMode.Impulse);
            }
            heldObject = null;

            Debug.Log("Objek dilempar.");
        }
    }
}
