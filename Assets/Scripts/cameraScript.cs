using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script was made with help from this article: https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230
public class cameraScript : MonoBehaviour
{
    public GameObject target;
    public float damping = 1;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;
        transform.LookAt(target.transform.position);
    }
}
