using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float sensitivity = 2f;
    float rotationX = 0f;
    float rotationY = 0f;

    float angleRad = 0f;
    

    float df = 0f;
    float dlr = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	df = 0.0f;
	dlr = 0.0f;

	if(Input.GetKey(KeyCode.W)) df = 1.0f;
	if(Input.GetKey(KeyCode.S)) df = -1.0f;
	if(Input.GetKey(KeyCode.A)) dlr = -1.0f;
	if(Input.GetKey(KeyCode.D)) dlr = 1.0f;

	angleRad = rotationY * Mathf.Deg2Rad;

		        // Forward vector (based on rotation)
        Vector3 forward = new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad));
        // Right vector (90° offset)
        Vector3 right = new Vector3(Mathf.Cos(angleRad), 0, -Mathf.Sin(angleRad));

        Vector3 moveDir = (forward * df + right * dlr).normalized;

        // Apply movement
        transform.position += moveDir * 5.0f * Time.deltaTime;

				
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate object left/right
	rotationY += mouseX;
	
        // Rotate object up/down (clamped)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        //transform.Rotate(Vector3.up * mouseX);

    }
}