using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour {

    public Rigidbody myRigidBody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(myRigidBody.position.x, transform.position.y, myRigidBody.position.z);
	}
}
