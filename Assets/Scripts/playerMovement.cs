using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float lookSensitivity = 2.0f;

    private bool onGround;
    private Rigidbody rb;
    private thirdPersonCamera cameraScript;
    private float verticalLookRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraScript = FindObjectOfType<thirdPersonCamera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.rotation = Quaternion.Euler(0.0f, cameraScript.transform.rotation.eulerAngles.y, 0.0f);

        // Vertical look rotation
        verticalLookRotation += mouseY * lookSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraScript.transform.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);

        // Movement
        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;
        moveDir = transform.TransformDirection(moveDir);
        moveDir *= moveSpeed;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        // Checking on ground
        // (This technically allows the player to double jump when they're at the vertex, but the odds of this are very low.)
        if (rb.velocity.y == 0)
        {
            onGround = true;
        }
                

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }
}