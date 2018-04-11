using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 6f;
    public float stopSpeed = 100f;
    public float slowingSpeed = 0.01f;
    public Rigidbody myRigidBody;
    public GameObject portalDestination;
    public bool characterStopsOnPortal = true;
    public bool activeHorizontal = false;
    public bool activeVertical = false;
    public bool onPortal;
    private Vector3 forward = new Vector3(0, 0, 32);
    private Vector3 left = new Vector3(-32, 0, 0);
    private Vector3 backward = new Vector3(0, 0, -32);
    private Vector3 right = new Vector3(32, 0, 0);
    private bool stop = false;

    private void Start()
    {
        forward = new Vector3(0, 0, 32);
        left = new Vector3(-32, 0, 0);
        backward = new Vector3(0, 0, -32);
        right = new Vector3(32, 0, 0);

    }
    // Update is called once per frame
    private void Update()
    {

        //Movement with WASD
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)) && stop == false)
        {
            myRigidBody.velocity = forward * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)) && stop == false)
        {
            myRigidBody.velocity = left * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(90, Vector3.up);
        }
        if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow)) && stop == false)
        {
            myRigidBody.velocity = backward * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)) && stop == false)
        {
            myRigidBody.velocity = right * speed;
            myRigidBody.rotation = Quaternion.AngleAxis(-90, Vector3.up);
        }

        onPortal = false;
        //Check if characters are inside the portals
        if (myRigidBody.transform.position.x <= portalDestination.transform.position.x + 16
            && myRigidBody.transform.position.x >= portalDestination.transform.position.x - 16
            && myRigidBody.transform.position.z <= portalDestination.transform.position.z + 16
            && myRigidBody.transform.position.z >= portalDestination.transform.position.z - 16)
        {
            //Stops the character when it enters the portal
            if (characterStopsOnPortal == true)
            {
                stop = true;
                myRigidBody.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.y, 0);
                speed = 0;
                //myRigidBody.transform.position = new Vector3(portalDestination.transform.position.x,
                //myRigidBody.transform.position.y + ((myRigidBody.transform.position.y - 16) - portalDestination.transform.position.y),
                //portalDestination.transform.position.z);
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(portalDestination.transform.position.x, 
                    myRigidBody.transform.position.y, 
                    portalDestination.transform.position.z), 300 * Time.deltaTime);
                if (myRigidBody.transform.position.x == portalDestination.transform.position.x
                    && myRigidBody.transform.position.z == portalDestination.transform.position.z)
                {
                    onPortal = true;
                }
            }

            if (characterStopsOnPortal == false) onPortal = true;
        }

        MovementRestrictions();
    }

    //This method stops the characters in the center of the squares in the grid
    void MovementRestrictions()
    {

        //Up or Down Movement
        if (((Input.GetKeyUp(KeyCode.W) && !Input.anyKeyDown) || (Input.GetKeyUp(KeyCode.S) && !Input.anyKeyDown))
            && (myRigidBody.velocity.z < slowingSpeed || myRigidBody.velocity.z > -slowingSpeed))
        {
            activeHorizontal = true;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            activeHorizontal = false;
        }

        if (activeHorizontal == true)
        {
            //if on block 9
            if (myRigidBody.transform.position.z >= -256.5 && myRigidBody.transform.position.z <= -240)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -256), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -256 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 8
            if (myRigidBody.transform.position.z > -240 && myRigidBody.transform.position.z <= -208)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -224), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -224 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 7
            if (myRigidBody.transform.position.z > -208 && myRigidBody.transform.position.z <= -176)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -192), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -192 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 6
            if (myRigidBody.transform.position.z > -176 && myRigidBody.transform.position.z <= -144)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -160), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -160 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 5
            if (myRigidBody.transform.position.z > -144 && myRigidBody.transform.position.z <= -112)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -128), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -128 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 4
            if (myRigidBody.transform.position.z > -112 && myRigidBody.transform.position.z <= -80)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -96), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -96 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 3
            if (myRigidBody.transform.position.z > -80 && myRigidBody.transform.position.z <= 48)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -64), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -64 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 2
            if (myRigidBody.transform.position.z > -48 && myRigidBody.transform.position.z <= -16)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, -32), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == -32 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if on block 1
            if (myRigidBody.transform.position.z > -16 && myRigidBody.transform.position.z <= 1)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(myRigidBody.transform.position.x, myRigidBody.transform.position.y, 0), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.z == 0 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
            }
            //if (myRigidBody.velocity == new Vector3(0, 0, 0)) activeHorizontal = false;
        }

        //Left or Right Movement
        if (((Input.GetKeyUp(KeyCode.A) && !Input.anyKeyDown) || (Input.GetKeyUp(KeyCode.D) && !Input.anyKeyDown))
            && (myRigidBody.velocity.x < slowingSpeed || myRigidBody.velocity.x > -slowingSpeed))
        {
            activeVertical = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            activeVertical= false;
        }
        if (activeVertical == true)
        {
            //if on block 9
            if (myRigidBody.transform.position.x >= -128.5 && myRigidBody.transform.position.x <= -112)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(-128, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == -128) activeVertical = false;
            }
            //if on block 8
            if (myRigidBody.transform.position.x > -112 && myRigidBody.transform.position.x <= -80)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(-96, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == -96 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
            //if on block 7
            if (myRigidBody.transform.position.x > -80 && myRigidBody.transform.position.x <= -48)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(-64, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == -64 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
            //if on block 6
            if (myRigidBody.transform.position.x > -48 && myRigidBody.transform.position.x <= -16)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(-32, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == -32 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
            //if on block 5
            if (myRigidBody.transform.position.x > -16 && myRigidBody.transform.position.x <= 16)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(0, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == 0 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
            //if on block 4
            if (myRigidBody.transform.position.x > 16 && myRigidBody.transform.position.x <= 48)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(32, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == 32 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
            //if on block 3
            if (myRigidBody.transform.position.x > 48 && myRigidBody.transform.position.x <= 80)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(64, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == 64 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
            //if on block 2
            if (myRigidBody.transform.position.x > 80 && myRigidBody.transform.position.x <= 112)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(96, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == 96 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
            //if on block 1
            if (myRigidBody.transform.position.x > 112 && myRigidBody.transform.position.x <= 128.5)
            {
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(128, myRigidBody.transform.position.y, myRigidBody.transform.position.z), stopSpeed * Time.deltaTime);
                if (myRigidBody.transform.position.x == 128 && myRigidBody.velocity == new Vector3(0, 0, 0)) activeVertical = false;
            }
        }
    }
}