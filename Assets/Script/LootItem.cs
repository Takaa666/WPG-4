using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootItem : MonoBehaviour
{
    [SerializeField] float interactRadius;
    [SerializeField] private LayerMask pickToInventoryLayerMask;
    [SerializeField] private GameObject pickToInventoryUI;
    [SerializeField] private InputActionReference pickToInventory;
    private RaycastHit hit;
    [SerializeField][Min(1)] private float hitRange = 3;
    public GameObject sourceRaycast;
    private CharacterController characterController;

    private Collider[] hitColliders;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

    }
    // Start is called before the first frame update
    void Start()
    {
        pickToInventory.action.performed += Loot;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = sourceRaycast.transform.position;

        hitColliders = Physics.OverlapSphere(playerPosition, interactRadius, pickToInventoryLayerMask);
        //Debug.DrawRay(sourceRaycast.position, sourceRaycast.forward * hitRange, Color.red);
        if (hitColliders.Length > 0)
        {
            Collider hit = hitColliders[0];

            QuestLog questLog = FindObjectOfType<QuestLog>();

            // Check if the object is associated with an inProgress quest
            if (questLog != null && questLog.CanLoot(hit.gameObject))
            {
                //hit.GetComponent<Highlight>()?.ToggleHighlight(true);
                pickToInventoryUI.SetActive(true);
            }
            else
            {
                // Hide UI if the object is not part of an inProgress quest
                //hit.GetComponent<Highlight>()?.ToggleHighlight(false);
                pickToInventoryUI.SetActive(false);
            }
        }
        else
        {
            // Hide UI if no object is detected in the area
            pickToInventoryUI.SetActive(false);
        }
    }

    public void Loot(InputAction.CallbackContext obj)
    {
        if (hitColliders.Length == 0) return;
        Collider hit = hitColliders[0];

        if (hit == null || hit.gameObject == null) return;
        Rigidbody rb = hit.GetComponent<Rigidbody>();

        if (hit.GetComponent<PickToInventory>().enabled)
        {
            QuestLog questLog = FindObjectOfType<QuestLog>();
            if (questLog != null && !questLog.CanLoot(hit.gameObject))
            {
                Debug.Log("Cannot loot item: " + hit.gameObject.name + " because the quest is not in progress or already completed.");
                return;
            }

            Debug.Log("Claiming Item: " + hit.gameObject.name);


            if (questLog != null)
            {
                questLog.CheckTargetObjectDestruction(hit.gameObject);
            }

            /*if (gameObject.CompareTag("Drop"))
            {
                Destroy(GetComponentInParent<Highlight>());
            }*/
            DestroyImmediate(hit.gameObject); // Destroy the item after adding to inventory
            itemScore();
            hit = null;
            //totalItemText.text = TotalItem.totalItem.ToString();
        }
    }

    private void OnDrawGizmos()
    {
        if (characterController == null) return;

        // Draw the sphere around the player to visualize the interactRadius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sourceRaycast.transform.position, interactRadius);
    }

    

    public static void itemScore()
    {
        TotalItem.totalItem += 1;
    }
}
