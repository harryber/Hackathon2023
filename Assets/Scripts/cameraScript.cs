using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was made with help from this article: https://sharpcoderblog.com/blog/third-person-camera-in-unity-3d

public class cameraScript : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public GameObject target;
    private float targetDistance;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;
    public float minTurnAngleY = -90.0f;
    public float maxTurnAngleY = 0.0f;
    private float rotX;
    private float rotY;
    void Start()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
    }
    void Update()
    {
        // get the mouse inputs
        rotY = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        rotY = Mathf.Clamp(rotY, minTurnAngleY, maxTurnAngleY);
        // rotate the camera
        //transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        // move the camera position
        transform.position = target.transform.position - (transform.forward * targetDistance);
    }
}