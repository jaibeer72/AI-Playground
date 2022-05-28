using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float movementTime = 5;

    public Vector3 newPos;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;

    private float panPercential = 1f;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();  
        inputProvider = GetComponent<CinemachineInputProvider>();
        newPos = virtualCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandelInputMovement();
    }

    private void HandelInputMovement()
    {
        float horizontal = inputProvider.GetAxisValue(0);
        float vertical = inputProvider.GetAxisValue(1);
        float forwardinput = inputProvider.GetAxisValue(2);
        if(forwardinput > 0)
        {
            Debug.Log("forward");
            newPos += (virtualCamera.transform.forward * movementSpeed); 
        }
        if(forwardinput < 0)
        {
            Debug.Log("forward");
            newPos += (virtualCamera.transform.forward * -movementSpeed); 
        }
        if(horizontal >= Screen.width * panPercential)
        {
            Debug.Log("horizontal");
            newPos += (virtualCamera.transform.right * movementSpeed);
        }
        if (horizontal <= Screen.width - (Screen.width * panPercential))
        {
            Debug.Log("horizontal");
            newPos += (virtualCamera.transform.right * -movementSpeed);
        }
        if (vertical >= Screen.height * panPercential)
        {
            Debug.Log("vertical");
            newPos += (virtualCamera.transform.up * movementSpeed); 
        }
        if (vertical <= Screen.height - (Screen.height * panPercential))
        {
            Debug.Log("vertical");
            newPos += (virtualCamera.transform.up * -movementSpeed);
        }

        virtualCamera.transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movementTime); 
    }
}
