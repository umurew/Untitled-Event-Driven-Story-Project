using System.ComponentModel;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [Header("Raycast Configuration")]
    [Description("Reach distance which determines maximum raycast lenght.")]
    [SerializeField] private float raycastDistance = 3.5f;
    [Description("Sphere radius which determines the raycast sphere's radius.")]
    [SerializeField] private float raycastRadius = 0.2f;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable currentInteractable;

    private void Update()
    {
        // Check if interactable exists
        PerformInteractionCheck();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
            currentInteractable.Interact();
    }

    private void PerformInteractionCheck()
    {
        Ray ray = new(cameraTransform.position, cameraTransform.forward);
        RaycastHit raycastHit;

        if (Physics.SphereCast(ray, raycastRadius, out raycastHit, raycastDistance, interactableLayer))
        {
            IInteractable iinteractable = raycastHit.collider.GetComponent<IInteractable>();

            if (iinteractable is not null)
            {
                if (iinteractable == currentInteractable)
                    return;

                currentInteractable = iinteractable;
                return;
            }

            currentInteractable = null;
            return;
        }
    }

    private void OnDrawGizmos()
    {
        if (cameraTransform is null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * raycastDistance);
        Gizmos.DrawWireSphere(cameraTransform.position + (cameraTransform.forward * raycastDistance), raycastRadius);
    }
}