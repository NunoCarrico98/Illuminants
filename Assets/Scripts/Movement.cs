using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 6f;
    public float stopSpeed = 100f;
    public Rigidbody myRigidBody;
    public GameObject portalDestination;
    public GameObject canvas;
    public bool characterStopsOnPortal = true;
    public bool activeHorizontal = false;
    public bool activeVertical = false;
    public bool onPortal;

    private Vector3 currentPos;
    private Vector3 lastPos;
    private Vector3 forward;
    private Vector3 left;
    private Vector3 backward;
    private Vector3 right;
    private Vector3 v3;

    private bool isCanvasRotating = false;
    private bool isRewinding = false;
    private bool up;
    private bool down;
    private bool leftside;
    private bool rightside;
    private bool slowing;
    private bool stop = false;
    private bool cubesInPlace = false;

    private float resetSpeed;

    private Animator myAnim;

    internal bool moving = false;
    internal bool isGoingUp;
    internal bool isGoingDown;
    internal bool isGoingSideways;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        currentPos = transform.position;

        resetSpeed = speed;

        myAnim = transform.Find("Face").GetComponent<Animator>();
        v3 = new Vector3(0, 0, 0);

    }
    // Update is called once per frame
    private void Update()
    {
        currentPos = transform.position;

        isCanvasRotating = canvas.GetComponent<CanvasRotation>().isCanvasRotating;

        isRewinding = transform.GetComponent<RewindTime>().isRewinding;

        cubesInPlace = CubeController.cubesInPlace;

        v3 = new Vector3(0, 0, 0);

        //Movement with WASD or Arrows v1

        /*if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)) 
            && !Input.anyKey && stop == false && isCanvasRotating == false)
        {
            myRigidBody.velocity = forward * speed;
            isGoingUp = true;
            //myRigidBody.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow))
            && !Input.anyKey && stop == false && isCanvasRotating == false)
        {
            myRigidBody.velocity = left * speed;
            //myRigidBody.rotation = Quaternion.AngleAxis(90, Vector3.up);
            isGoingSideways = true;
        }
        if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow))
            && !Input.anyKey  && stop == false && isCanvasRotating == false)
        {
            myRigidBody.velocity = backward * speed;
            //myRigidBody.rotation = Quaternion.AngleAxis(0, Vector3.up);
            isGoingDown = true;
        }
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow))
            && !Input.anyKey && stop == false && isCanvasRotating == false)
        {
            myRigidBody.velocity = right * speed;
            //myRigidBody.rotation = Quaternion.AngleAxis(-90, Vector3.up);
            isGoingSideways = true;
        }*/

        //if (myRigidBody.position.y == 48 && cubesInPlace == true)
        //{
         //Move();
        //}
        if(cubesInPlace == true) Move();
        CheckDirection();

        if (isRewinding == false)
        {
            myAnim.SetBool("Up", isGoingUp);
            myAnim.SetBool("Down", isGoingDown);
            myAnim.SetBool("Vertical", isGoingSideways);
        }

        if (!Input.anyKey || (myRigidBody.velocity.x == 0 && myRigidBody.velocity.z == 0))
        {
            isGoingUp = false;
            isGoingDown = false;
            isGoingSideways = false;
        }

        LockOnPortal();

        lastPos = transform.position;
    }

    void Move()
    {
        //Movement with WASD or Arrows v2

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && stop == false && isCanvasRotating == false)
        {
            moving = true;

            down = false;
            leftside = false;
            rightside = false;
            up = true;

            //v3 += Vector3.forward * 32;
            //isGoingUp = true;
        }
        if (up == true)
        {
            v3 += Vector3.forward * 32;

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) slowing = true;
            if (slowing == true)
            {
                speed -= stopSpeed * Time.deltaTime;
                if (speed <= 0)
                {
                    up = false;
                    slowing = false;
                    speed = resetSpeed;
                    moving = false;
                }
            }
        }


        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && stop == false && isCanvasRotating == false)
        {
            moving = true;

            up = false;
            leftside = false;
            rightside = false;
            down = true;
            //v3 += Vector3.back * 32;
            //isGoingDown = true;
        }
        if (down == true)
        {
            v3 += Vector3.back * 32;

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) slowing = true;
            if (slowing == true)
            {
                speed -= stopSpeed * Time.deltaTime;
                if (speed <= 0)
                {
                    down = false;
                    slowing = false;
                    speed = resetSpeed;
                    moving = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && stop == false && isCanvasRotating == false)
        {
            moving = true;

            up = false;
            down = false;
            rightside = false;
            leftside = true;
            //v3 += Vector3.left * 32;
            //isGoingSideways = true;
        }
        if (leftside == true)
        {
            v3 += Vector3.left * 32;

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) slowing = true;
            if (slowing == true)
            {
                speed -= stopSpeed * Time.deltaTime;
                if (speed <= 0)
                {
                    leftside = false;
                    slowing = false;
                    speed = resetSpeed;
                    moving = false;
                }
            }
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) && stop == false && isCanvasRotating == false)
        {
            moving = true;

            up = false;
            down = false;
            leftside = false;
            rightside = true;
            //v3 += Vector3.right * 32;
            //isGoingSideways = true;
        }
        if (rightside == true)
        {
            v3 += Vector3.right * 32;

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) slowing = true;
            if (slowing == true)
            {
                speed -= stopSpeed * Time.deltaTime;
                if (speed <= 0)
                {
                    rightside = false;
                    slowing = false;
                    speed = resetSpeed;
                    moving = false;
                }
            }
        }

        myRigidBody.velocity = speed * v3;
    }
    void LockOnPortal()
    {
        onPortal = false;

        //Check if characters are inside the portals
        if (myRigidBody.transform.position.x <= portalDestination.transform.position.x + 10
            && myRigidBody.transform.position.x >= portalDestination.transform.position.x - 10
            && myRigidBody.transform.position.z <= portalDestination.transform.position.z + 10
            && myRigidBody.transform.position.z >= portalDestination.transform.position.z - 10)
        {
            //Stops the character when it enters the portal
            if (characterStopsOnPortal == true)
            {
                stop = true;
                myRigidBody.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.y, 0);
                speed = 0;
                myRigidBody.transform.position = Vector3.MoveTowards(myRigidBody.transform.position,
                    new Vector3(portalDestination.transform.position.x,
                    myRigidBody.transform.position.y,
                    portalDestination.transform.position.z), 50 * Time.deltaTime);
                if (myRigidBody.transform.position.x == portalDestination.transform.position.x
                    && myRigidBody.transform.position.z == portalDestination.transform.position.z)
                {
                    onPortal = true;
                }
            }

            if (characterStopsOnPortal == false) onPortal = true;
        }
    }

    void CheckDirection() 
    {
        //When player is controlling the character
        if (Input.anyKey)
        {

            if (Mathf.Abs(lastPos.z - currentPos.z) > Mathf.Abs(lastPos.x - currentPos.x)
                && lastPos.z - currentPos.z < 0 && myRigidBody.velocity.z > 0) //up
            {
                isGoingUp = true;
                isGoingDown = false;
                isGoingSideways = false;
            }
            if (Mathf.Abs(lastPos.z - currentPos.z) > Mathf.Abs(lastPos.x - currentPos.x)
                && lastPos.z - currentPos.z > 0 && myRigidBody.velocity.z < 0) //down
            {
                isGoingUp = false;
                isGoingDown = true;
                isGoingSideways = false;
            }
            if (Mathf.Abs(lastPos.z - currentPos.z) < Mathf.Abs(lastPos.x - currentPos.x)
                && myRigidBody.velocity.x != 0) //sideways
            {
                isGoingUp = false;
                isGoingDown = false;
                isGoingSideways = true;
            }
        }
        //When character is moving by itself
        else
        {
            if (myRigidBody.velocity.z > 0) isGoingUp = true;
            if (myRigidBody.velocity.z < 0) isGoingDown = true;
            if (myRigidBody.velocity.x != 0) isGoingSideways = true;
        }
    }

}
