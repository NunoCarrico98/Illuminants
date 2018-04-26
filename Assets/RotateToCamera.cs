using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.AngleAxis(Camera.main.transform.rotation.y, Vector3.up);
    }
}
