using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [Header("Raycast Configuration")]
    [Description("Reach distance which determines maximum raycast lenght.")]
    [SerializeField] private float raycastDistance = 3.5f;
    [Description("Sphere radius which determines the raycast sphere's radius.")]
    [SerializeField] private float raycastRadius = 0.2f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("UI References")]
    [SerializeField] private UIDocument hudDocument;

    private IInteractable currentInteractable;
    private Label interactionPromptLabel;

    private void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (hudDocument != null)
        {
            VisualElement root = hudDocument.rootVisualElement;
            interactionPromptLabel = root.Q<Label>("InteractionPromptLabel");

            HidePrompt();
        }
    }

    private void Update()
    {
        // Check if interactable exists
        PerformInteractionCheck();

        if (currentInteractable != null && InputManager.Instance.playerActions.Interact.WasPressedThisFrame())
        {
            currentInteractable.Interact();
            UpdatePrompt();
        }
    }

    private void PerformInteractionCheck()
    {
        Ray ray = new(cameraTransform.position, cameraTransform.forward);

        if (Physics.SphereCast(ray, raycastRadius, out RaycastHit raycastHit, raycastDistance, interactableLayer))
        {
            IInteractable iinteractable = raycastHit.collider.GetComponent<IInteractable>();

            if (iinteractable is not null)
            {
                if (iinteractable == currentInteractable)
                    return;

                currentInteractable = iinteractable;
                ShowPrompt();

                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable = null;
            HidePrompt();
        }
    }

    private void ShowPrompt()
    {
        if (interactionPromptLabel == null || currentInteractable == null)
        {
            Debug.LogError("Visual element \"InteractionPromptLabel\" or IInteractable \"currentInteractable\" was null.");
            return;
        }

        UpdatePrompt();
        interactionPromptLabel.style.visibility = Visibility.Visible;
    }

    private void HidePrompt() => interactionPromptLabel.style.visibility = Visibility.Hidden;

    private void UpdatePrompt() => interactionPromptLabel.text = currentInteractable.GetInteractPrompt();

    private void OnDrawGizmosSelected()
    {
        if (cameraTransform == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * raycastDistance);
        Gizmos.DrawWireSphere(cameraTransform.position + (cameraTransform.forward * raycastDistance), raycastRadius);
    }
}