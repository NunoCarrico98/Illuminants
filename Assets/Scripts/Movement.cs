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
    private bool stop = false;

    // Update is called once per frame
    private void Update()
    {
        forward = new Vector3(0, 0, 32);
        left = new Vector3(-32, 0, 0);
        backward = new Vector3(0, 0, -32);
        right = new Vector3(32, 0, 0);

        
        if (Input.GetKey(KeyCode.W) && stop == false)
        {
            myRigidBody.velocity = forward * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKey(KeyCode.A) && stop == false)
        {
            myRigidBody.velocity = left * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
        if (Input.GetKey(KeyCode.S) && stop == false)
        {
            myRigidBody.velocity = backward * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        if (Input.GetKey(KeyCode.D) && stop == false)
        {
            myRigidBody.velocity = right * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(-90, Vector3.up);
        }


        if (myRigidBody.transform.position.x <= portalDestination.transform.position.x + 10
            && myRigidBody.transform.position.x >= portalDestination.transform.position.x - 10
            && myRigidBody.transform.position.z <= portalDestination.transform.position.z + 10
            && myRigidBody.transform.position.z >= portalDestination.transform.position.z - 10)
        {
            stop = true;
            myRigidBody.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.y, 0);
            speed = 0;
            myRigidBody.transform.position = new Vector3(portalDestination.transform.position.x,
                myRigidBody.transform.position.y, 
                portalDestination.transform.position.z);
            myRigidBody.GetComponent<NextLevel>().OnTriggerEnter();
        }


    }
}