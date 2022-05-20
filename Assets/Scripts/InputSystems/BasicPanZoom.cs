using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Refrence https://www.youtube.com/watch?v=PsAbHoB85hM
/// Only works for top down we can make it more planer based on getting it relitive to cam forward we shall disuss 
/// </summary>
public class BasicPanZoom : MonoBehaviour
{
    [SerializeField]
    private float panSpeed = 2.0f;
    [SerializeField]
    private float zoomSpped = 3.0f;
    [SerializeField]
    private float zoomInMax = 40f;
    [SerializeField]
    private float zoomOutMax = 90f; 

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform camraTransform; 

    private float magnitude = 0.95f; // This is to decide at which point you want it to pan 

    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>(); 
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        camraTransform = virtualCamera.gameObject.transform; 
    }

    private void Update()
    {
        float x = inputProvider.GetAxisValue(0); 
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2); 

        if(x !=0 || y !=0 )
        {
            PanScreen(x, y); 
        }
        if(z!=0)
        {
            ZoomInScreen(z); 
        }
    }

    public void ZoomInScreen(float incriment)
    {
        float fov = virtualCamera.m_Lens.FieldOfView;
        float target = Mathf.Clamp(fov + incriment, zoomInMax, zoomOutMax);
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, target, zoomSpped * Time.deltaTime);
    }

    // TODO : i think if we base it more on the circle diameter later it's a better option as it gives us more room to work with
    public Vector2 PanDirection(float x , float y)
    {
        Vector2 direction = Vector2.zero;
        if (y >= Screen.height * magnitude)
        {
            direction.y += 1;
        }
        else if(y <= Screen.height)
        {
            direction.y -= 1; 
        }
        else if (x >= Screen.width * magnitude)
        {
            direction.x += 1; 
        }
        else if(x <= Screen.width * magnitude) 
        {
            direction.x -= 1; 
        }
        return direction; 
    }
    public void PanScreen(float x , float y)
    {
        Vector2 direction =  PanDirection(x , y);
        camraTransform.position = Vector3.Lerp(camraTransform.position, camraTransform.position + (Vector3)direction * panSpeed, Time.deltaTime); 
    }
}
