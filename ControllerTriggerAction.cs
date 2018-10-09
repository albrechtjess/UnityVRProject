using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTriggerAction : MonoBehaviour {

    public float velocityMultiplier;
    private SteamVR_TrackedObject trackedObj;

    // 1
    private GameObject collidingObject;
    // 2
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller //Device property
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //velocityMultiplier = 2;
    }

    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            //Debug.Log(velocityMultiplier + " total velocity " + Controller.velocity * velocityMultiplier);
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity * velocityMultiplier;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity * velocityMultiplier;
        }
        // 4
        objectInHand = null;
    }

    private void MakeFist()
    {
        
    }

    // Update is called once per frame
    void Update () {
        // 1
        
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
            else
            {
                GetComponent<BoxCollider>().isTrigger = false;
            }
        }

        // 2
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
            else GetComponent<BoxCollider>().isTrigger = true;
        }

        if (Controller.GetHairTrigger())
        {

            Debug.Log("Trigger held");
            if (collidingObject)
            {
                //DoDamage
                Destructible destructible = collidingObject.GetComponent<Destructible>();
                if (destructible)
                {
                    Debug.Log("Bam...Pow");
                    destructible.TakeDamage(25.0f);
                }
            }
        }

    }
}
