using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabWorld : MonoBehaviour {
    
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject Body;
    //public Rigidbody cameraBody;
    public Transform cameraRigTransform;
    public Transform headTransform;
    private Vector3 controllerLastPosition;
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
            Vector3 controllerPositionDifference = Controller.velocity;
            controllerPositionDifference.y = 0;
            cameraRigTransform.position -= controllerPositionDifference * playerSpeed;

            //cameraBody.velocity = controllerPositionDifference;
        }
        

    }
}
