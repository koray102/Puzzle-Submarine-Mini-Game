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


    [Header("Idle Settings")]
    public float bobSpeed = 2f;
    public float bobAmount = 0.1f; 
    public float tiltSpeed = 1.5f;
    public float tiltAmount = 2f;


    private Rigidbody rb;
    private Vector2 moveInput;
    private Quaternion targetRotation;
    private Vector3 modelInitialLocalPos;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (modelTransform != null)
        {
            targetRotation = modelTransform.rotation;
            modelInitialLocalPos = modelTransform.localPosition;
        }
        else
            Debug.LogError("Model atanmamis");
    }

    void Update()
    {
        if(SubmarineManager.Instance.isChestsDone)
            return;

        if (modelTransform == null)
            return;

        // Movement ve donus kismi
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.x != 0)
        {
            float yRotation = moveInput.x > 0 ? 0 : 180;
            targetRotation = Quaternion.Euler(0, yRotation, 0);
        }

        // sinus dalgasi ile su dalgasu efekti vermek icin
        float newY = modelInitialLocalPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        modelTransform.localPosition = new Vector3(modelInitialLocalPos.x, newY, modelInitialLocalPos.z);

        // bu da ayni sekilde ama rotasyon icin
        float tiltZ = Mathf.Sin(Time.time * tiltSpeed) * tiltAmount;
        Quaternion tiltRotation = Quaternion.Euler(0, 0, tiltZ);

        Quaternion finalTargetRotation = targetRotation * tiltRotation;

        modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, finalTargetRotation, Time.deltaTime * turnSpeed);
        
        // Interaction kismi
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void FixedUpdate()
    {
        if(SubmarineManager.Instance.isChestsDone)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

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