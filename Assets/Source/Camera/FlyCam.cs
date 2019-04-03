using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour
{
    public float sensitivity;
    public float normalSpeed;
    public float climbSpeed;
    public float slowMove;
    public float fastMove;

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
        CameraSpin();
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
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.position += transform.forward * (normalSpeed * fastMove) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (normalSpeed * fastMove) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            transform.position += transform.forward * (normalSpeed * slowMove) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * (normalSpeed * slowMove) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * normalSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * normalSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
    }

    private void CameraSpin()
    {
        if (Input.GetKey(KeyCode.E))
        {
 
        }
    }


}

