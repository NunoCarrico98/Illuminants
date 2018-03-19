using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 1f;
    public Rigidbody2D myRigidBody;
    public GameObject portalDestination;
    public Vector2 forward = new Vector2(0, 1);
    public Vector2 left = new Vector2(-1, 0);
    public Vector2 backward = new Vector2(0, -1);
    public Vector2 right = new Vector2(1, 0);

  

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
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
            && myRigidBody.transform.position.y <= portalDestination.transform.position.y + 0.1
            && myRigidBody.transform.position.y >= portalDestination.transform.position.y - 0.1)
        {
            speed = 0;
            myRigidBody.transform.position = portalDestination.transform.position;
            myRigidBody.GetComponent<NextLevel>().OnTriggerEnter();
        }


    }
}