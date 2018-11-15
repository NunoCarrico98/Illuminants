using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 6f;
    public float stopSpeed = 100f;
    public Rigidbody myRigidBody;
    public GameObject portalDestination;
    public GameObject portalWalls;
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
    private GameObject newPortalWalls;
    private GameObject characters;
    private Animator myAnim;
    private SpriteRenderer sprite;
    private NextLevel nextLevel;
    private SpawnScript spawnScript;


    private bool isCanvasRotating = false;
    private bool isRewinding = false;
    private bool up;
    private bool down;
    private bool leftside;
    private bool rightside;
    private bool slowing;
    private bool stop = false;
    private bool cubesInPlace = false;
    private bool done = false;
    private bool activateIntensity = false;
    private float prisonSpeed = 0.2f;
    private float reason = 0f;
    private float resetSpeed;
    private float prisonTimer = 0f;
    private float intensity = 0f;
    private float range = 100f;
    private int counter = 0;



    internal bool moving = false;
    internal bool isGoingUp;
    internal bool isGoingDown;
    internal bool isGoingSideways;
    internal bool isSpriteActive = false;

    private void Start()
    {
        nextLevel = FindObjectOfType<NextLevel>();
        spawnScript = FindObjectOfType<SpawnScript>();

        characters = GameObject.FindGameObjectWithTag("Characters");

        canvas = GameObject.FindGameObjectWithTag("Canvas");

        currentPos = transform.position;

        resetSpeed = speed;

        myAnim = transform.Find("Face").GetComponent<Animator>();
        v3 = new Vector3(0, 0, 0);

    }
    // Update is called once per frame
    private void Update()
    {
        sprite = transform.Find("Face").GetComponent<SpriteRenderer>();
        currentPos = transform.position;


        isCanvasRotating = canvas.GetComponent<CanvasRotation>().isCanvasRotating;

        isRewinding = transform.GetComponent<RewindTime>().isRewinding;


        cubesInPlace = CubeController.cubesInPlace;

        v3 = new Vector3(0, 0, 0);

        CheckIfSpriteOn();

        //Movement with WASD or Arrows
        if (cubesInPlace == true && nextLevel.activeFinalAnims == false)
        {
            Move();
        }
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

        if (spawnScript.charactersInPlace
            && portalWalls != null
            && transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == true)
        {
            IncreaseIntensity();
        }


        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (portalDestination != null)
        {
            LockOnPortal();
        }

    }

    void Move()
    {

        //Movement with WASD or Arrows v2
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)
            || Input.GetAxis("LeftJoystickVertical") >= 0.8
            || Input.GetAxis("ArrowsVertical") >= 0.8) 
            && stop == false && isCanvasRotating == false)
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

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)
                || ((Input.GetAxis("LeftJoystickVertical") < 0.8
                && Input.GetAxis("ArrowsVertical") < 0.8) 
                && !Input.anyKey)) slowing = true;
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


        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)
            || Input.GetAxis("LeftJoystickVertical") <= -0.8
            || Input.GetAxis("ArrowsVertical") <= -0.8)
            && stop == false && isCanvasRotating == false)
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

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)
                || ((Input.GetAxis("LeftJoystickVertical") > -0.8
                && Input.GetAxis("ArrowsVertical") > -0.8)
                && !Input.anyKey)) slowing = true;
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

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetAxis("LeftJoystickHorizontal") <= -0.8
            || Input.GetAxis("ArrowsHorizontal") <= -0.8)
                && stop == false && isCanvasRotating == false)
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

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)
                || ((Input.GetAxis("LeftJoystickHorizontal") > -0.8
                && Input.GetAxis("ArrowsHorizontal") > -0.8)
                && !Input.anyKey)) slowing = true;
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

        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)
            || Input.GetAxis("LeftJoystickHorizontal") >= 0.8
            || Input.GetAxis("ArrowsHorizontal") >= 0.8)
                && stop == false && isCanvasRotating == false)
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

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)
                || ((Input.GetAxis("LeftJoystickHorizontal") < 0.8
                && Input.GetAxis("ArrowsHorizontal") < 0.8)
                && !Input.anyKey)) slowing = true;
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
                    if (transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == true)
                    {
                        LightPrison();
                    }
                }
            }
        }

        //Check if characters are inside the portals
        if (myRigidBody.transform.position.x <= portalDestination.transform.position.x + 3
            && myRigidBody.transform.position.x >= portalDestination.transform.position.x - 3
            && myRigidBody.transform.position.z <= portalDestination.transform.position.z + 3
            && myRigidBody.transform.position.z >= portalDestination.transform.position.z - 3)
        {
            if (characterStopsOnPortal == false)
            {
                onPortal = true;
            }
        }

    }

    private void IncreaseIntensity()
    {
        if (!characterStopsOnPortal && portalDestination != null)
        {
            if (counter == 0)
            {
                newPortalWalls = Instantiate(portalWalls, portalDestination.transform.position, canvas.transform.rotation, portalDestination.transform);
                newPortalWalls.transform.localScale = new Vector3(1, 0, 1);
                counter++;
            }

            if (nextLevel.activeFinalAnims && newPortalWalls.transform.localScale == new Vector3(1, 0.4f, 1))
            {
                done = true;
            }

            if (done || !onPortal)
            {
                if (reason > 0)
                {
                    reason -= prisonSpeed / 5;
                }
                if (reason < 0) reason = 0;
            }
            if (!done && onPortal)
            {
                if (reason < 0.4f)
                {
                    reason += prisonSpeed / 5;
                }
                if (reason > 0.4f) reason = 0.4f;
            }
            newPortalWalls.transform.localScale = new Vector3(1, reason, 1);
        }
    }

    private void LightPrison()
    {
        if (counter == 0)
        {
            newPortalWalls = Instantiate(portalWalls, portalDestination.transform.position, canvas.transform.rotation, portalDestination.transform);
            newPortalWalls.transform.localScale = new Vector3(1, 0, 1);
            counter++;
        }

        if (nextLevel.activeFinalAnims
            && (newPortalWalls.transform.localScale == new Vector3(1, 1, 1)
            || newPortalWalls.transform.localScale == new Vector3(1, 1, 1)))
        {
            done = true;
        }

        if (done)
        {
            if (reason > 0)
            {
                reason -= prisonSpeed / 2;
            }
            if (reason < 0) reason = 0;
        }
        else
        {
            if (reason < 1)
            {
                reason += prisonSpeed / 2;
            }
            if (reason > 1) reason = 1;
        }
        newPortalWalls.transform.localScale = new Vector3(1, reason, 1);


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

    void CheckIfSpriteOn()
    {
        if (sprite.enabled == true)
        {
            isSpriteActive = true;
        }
        else
        {
            isSpriteActive = false;
        }
    }
}
