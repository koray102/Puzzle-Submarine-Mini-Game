using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SubmarineController : MonoBehaviour
{
    public Transform modelTransform;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;


    [Header("Interaction Settings")]
    public float interactionRadius = 3f;
    public KeyCode interactKey = KeyCode.E;

    private Rigidbody rb;
    private Vector2 moveInput;
    private Quaternion targetRotation;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (modelTransform != null)
            targetRotation = modelTransform.rotation;
        else
            Debug.LogError("Model atanmamis");
    }

    void Update()
    {
        if(SubmarineManager.Instance.isChestsDone)
            return;

        // Movement ve donus kismi
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.x != 0)
        {
            float yRotation = moveInput.x > 0 ? 0 : 180;
            targetRotation = Quaternion.Euler(0, yRotation, 0);
        }

        if (modelTransform != null)
        {
            modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        // Interaction kismi
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void FixedUpdate()
    {
        // collision kontrolu icin movement fiziksel yaptim
        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, moveInput.y * moveSpeed, 0);
    }


    #region Interaction Part
    void TryInteract()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IInteractable interactableObject))
            {
                interactableObject.OnInteract();
            }else if (hit.transform.parent != null && hit.transform.parent.TryGetComponent(out IInteractable parentInteractable))
            {
                 parentInteractable.OnInteract();
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
    #endregion

}