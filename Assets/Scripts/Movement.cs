using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 1f;
    public Rigidbody myRigidBody;
    public GameObject portalDestination;
    private Vector3 forward = new Vector3(0, 0, 32);
    private Vector3 left = new Vector3(-32, 0, 0);
    private Vector3 backward = new Vector3(0,0, -32);
    private Vector3 right = new Vector3(32, 0, 0);

    // Update is called once per frame
    private void Update()
    {
        forward = new Vector3(0, 0, 32);
        left = new Vector3(-32, 0, 0);
        backward = new Vector3(0, 0, -32);
        right = new Vector3(32, 0, 0);

        
        if (Input.GetKey(KeyCode.W))
        {
            myRigidBody.velocity = forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            myRigidBody.velocity = left * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            myRigidBody.velocity = backward * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myRigidBody.velocity = right * speed;
        }


        if (myRigidBody.transform.position.x <= portalDestination.transform.position.x + 10
            && myRigidBody.transform.position.x >= portalDestination.transform.position.x - 10
            && myRigidBody.transform.position.z <= portalDestination.transform.position.z + 10
            && myRigidBody.transform.position.z >= portalDestination.transform.position.z - 10)
        {
            speed = 0;
            myRigidBody.transform.position = new Vector3(portalDestination.transform.position.x,
                myRigidBody.transform.position.y, 
                portalDestination.transform.position.z);
            myRigidBody.GetComponent<NextLevel>().OnTriggerEnter();
        }


    }
}