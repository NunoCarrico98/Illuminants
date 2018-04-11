using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{

    public Rigidbody myRigidBody;
    public GameObject portalDestination;
    public float speed = 1f;
    public float movementInYZ = 16f;
    private Vector3 forward = new Vector3(0, 0, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 backward = new Vector3(0, 0, 0);
    private Vector3 right = new Vector3(1, 0, 0);




    // Use this for initialization
    void Start()
    {
        forward = (new Vector3(0, movementInYZ, 0) + new Vector3(0, 0, movementInYZ));
        backward = (new Vector3(0, -movementInYZ, 0) + new Vector3(0, 0, -movementInYZ));
    }

    // Update is called once per frame
    void Update()
    {

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
        if (myRigidBody.transform.position.x <= portalDestination.transform.position.x + 0.1
            && myRigidBody.transform.position.x >= portalDestination.transform.position.x - 0.1
            && myRigidBody.transform.position.z <= portalDestination.transform.position.z + 0.1
            && myRigidBody.transform.position.z >= portalDestination.transform.position.z - 0.1)
        {
            speed = 0;
            myRigidBody.transform.position = portalDestination.transform.position;
            //myRigidBody.GetComponent<NextLevel>().OnTriggerEnter();
        }


    }
}
