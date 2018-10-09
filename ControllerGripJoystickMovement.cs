using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGripJoystickMovement : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject Body;
    public Transform cameraRigTransform;
    public Transform headTransform;
    public float playerSpeed;

    private SteamVR_Controller.Device Controller //Device property
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    // Update is called once per frame
    void Update()
    {
        // 1
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            Vector3 controllerPositionDifference = trackedObj.transform.forward;
            controllerPositionDifference.y = 0;
            //update position based on the rotation of the controller at a specific rate in that axis 
            cameraRigTransform.position += controllerPositionDifference * playerSpeed;
        }

    }
}
