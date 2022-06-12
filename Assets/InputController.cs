using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputActionAsset MainControlsActionsAsset;
    private InputActionMap mainControlsActionMap;
    private InputAction cameraMovementAction;
    private InputAction cameraZoomAction;
    private InputAction cameraRotateAction;

    #region InputEvents
        public static Action<InputAction.CallbackContext> OnCameraMovementAction;
        public static Action<InputAction.CallbackContext> OnCameraZoomAction;
        public static Action<InputAction.CallbackContext> OnCameraRotateAction;

    #endregion

    void OnEnable()
    {
        mainControlsActionMap = MainControlsActionsAsset.FindActionMap("Gameplay");
        mainControlsActionMap.Enable();

        cameraMovementAction = mainControlsActionMap.FindAction("Pan");
        cameraZoomAction = mainControlsActionMap.FindAction("Zoom");
        cameraRotateAction = mainControlsActionMap.FindAction("Rotate");

        cameraMovementAction.performed += (context) => OnCameraMovementAction?.Invoke(context);
        cameraZoomAction.performed += (context) => OnCameraZoomAction.Invoke(context);
        cameraRotateAction.performed += (context) => OnCameraRotateAction.Invoke(context);
    }

    
    void OnDisable()
    {
        mainControlsActionMap.Enable();
    }

}
