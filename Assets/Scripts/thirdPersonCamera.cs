using UnityEngine;

public class thirdPersonCamera: MonoBehaviour
{
    public Transform playerTransform;
    public AIController ai;
    public float cameraMoveSpeed = 120.0f;
    public float clampAngle = 80.0f;
    public float clampMinY = 90f;
    public float clampMaxY = 180f;
    public float inputSensitivity = 150.0f;
    public float cameraDistance = 3.0f;

    private float rotY = 0.0f; // rotation around the y-axis
    private float rotX = 0.0f; // rotation around the x-axis

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * inputSensitivity * Time.deltaTime;
        rotX -= mouseY * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, 0f, clampAngle);
        //rotY = Mathf.Clamp(rotY, clampMinY, clampMaxY);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.position = playerTransform.position - (localRotation * Vector3.forward * cameraDistance);
        if (ai.t.ready) transform.rotation = localRotation;
    }
}