using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBuilding : MonoBehaviour {

    Vector3 originalPosition;
	// Use this for initialization
	void Start () {
        originalPosition = this.transform.position;
        //Debug.Log(originalPosition);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Transform Position: " + this.transform.position);
        //Debug.Log("Distance: " + Vector3.Distance(this.transform.position, originalPosition));
        if (Vector3.Distance(this.transform.position, originalPosition) > 0.75f)
        {
            Destroy(this.gameObject, 30.0f);
        }
		
	}
}
