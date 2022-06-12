using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float movementSpeed = 1;
    public float zoomSpeed = 0.1f;
    public float RotateSpeed = 1;
    public float panScreenPercentage = 1f;

    public float MaxZoomY = 100f;
    public float MinZoomY = 30f;


    public float currentZoom;
    public Vector3 currentMovementVector;
    public float currentRotateInput;

    public CinemachineVirtualCamera virtualCamera;

    

    void Start()
    {
        InputController.OnCameraMovementAction += OnCameraMovementActionHandler;
        InputController.OnCameraZoomAction += OnCameraZoomActionHandler;
        InputController.OnCameraRotateAction += OnRotateActionHandler;
        currentMovementVector = Vector3.zero;
        currentZoom = MinZoomY;

    }

    private void OnRotateActionHandler(InputAction.CallbackContext context)
    {
        currentRotateInput = 0;
        float mouseX = context.ReadValue<float>();
        if (mouseX >= Screen.width * (1-panScreenPercentage))
        {
            Debug.Log("right rotate");
            currentRotateInput = 1;
        }
        if (mouseX <= Screen.width * panScreenPercentage)
        {
            Debug.Log("left Rotate");
            currentRotateInput = -1;
        }

    }

    private void OnCameraZoomActionHandler(InputAction.CallbackContext context)
    {
        float zoomInput = -1*context.ReadValue<float>();
        currentZoom = Math.Clamp(currentZoom+zoomInput*zoomSpeed,MinZoomY,MaxZoomY);
    }

    private void OnCameraMovementActionHandler(InputAction.CallbackContext context)
    {
        currentMovementVector = Vector3.zero;
        if(context.control.path == "/Mouse/position")
        {
            Vector2 mousePosition = context.ReadValue<Vector2>();
            if (mousePosition.x >= Screen.width * (1-panScreenPercentage))
            {
                Debug.Log("right");
                currentMovementVector.x = 1;
            }
            if (mousePosition.x <= Screen.width * panScreenPercentage)
            {
                Debug.Log("left");
                currentMovementVector.x = -1;
            }
            if (mousePosition.y >= Screen.height * (1-panScreenPercentage))
            {
                Debug.Log("up");
                currentMovementVector.z = 1;
            }
            if (mousePosition.y <= Screen.height * panScreenPercentage)
            {
                Debug.Log("down");
                currentMovementVector.z = -1;
            }
        }
        else{
            currentMovementVector = context.ReadValue<Vector2>();
            currentMovementVector.z = currentMovementVector.y;
            currentMovementVector.y = 0;
        }
        currentMovementVector.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputMovement();
    }

    private void HandleInputMovement()
    {
        // float horizontal = inputProvider.GetAxisValue(0);
        // float vertical = inputProvider.GetAxisValue(1);
        // // float forwardinput = inputProvider.GetAxisValue(2);
        // if (forwardinput != 0 || vertical !=0 || horizontal !=0)
        // {
        //     if (forwardinput > 0)
        //     {
        //         Debug.Log("front");
        //         newPos += (virtualCamera.transform.forward * movementSpeed);
        //     }
        //     if(forwardinput<0)
        //     {
        //         Debug.Log("back");
        //         newPos += (virtualCamera.transform.forward * -movementSpeed);
        //     }

        //     if (horizontal >= Screen.width * panPercential)
        //     {
        //         Debug.Log("right");
        //         newPos += (virtualCamera.transform.right * movementSpeed);
        //     }
        //     if (horizontal <= Screen.width - (Screen.width * panPercential))
        //     {
        //         Debug.Log("left");
        //         newPos += (virtualCamera.transform.right * -movementSpeed);
        //     }
        //     if (vertical >= Screen.height * panPercential)
        //     {
        //         Debug.Log("up");
        //         newPos += (virtualCamera.transform.up * movementSpeed);
        //     }
        //     if (vertical <= Screen.height - (Screen.height * panPercential))
        //     {
        //         Debug.Log("down");
        //         newPos += (virtualCamera.transform.up * -movementSpeed);
        //     }
        // }
        transform.position = transform.position + currentMovementVector*movementSpeed;
        virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position, transform.position - currentZoom*virtualCamera.transform.forward, Time.deltaTime); 
        transform.RotateAround(transform.position,Vector3.up,currentRotateInput*RotateSpeed);
        
        // virtualCamera.transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movementTime); 
    }
}
