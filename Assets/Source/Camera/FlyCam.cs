using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour
{
    public float sensitivity;
    public float movementSpeed;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private void Start()
    {
        Screen.lockCursor = true; 
    }

    private void Update()
    {
        MouseMovement();
        CameraMovement();
    }

    private void MouseMovement()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
    }

    private void CameraMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * movementSpeed;
        }
    }
}

