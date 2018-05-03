using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewindTime : MonoBehaviour
{

    public bool isRewinding;

    private List<PointInTime> pointsInTime;
    private Rigidbody myRigidBody;
    private Animator myAnim;
    private Transform canvas;
    private float acceleration = 0;
    private bool cubesInPlace;
    private bool up = false;
    private bool down = false;
    private bool sideways = false;
    private bool isMoving = false;
    private bool isGoingUp = false;
    private bool isGoingDown = false;
    private bool isGoingSideways = false;
    


    // Use this for initialization
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        myRigidBody = GetComponent<Rigidbody>();
        myAnim = transform.Find("Face").GetComponent<Animator>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        isMoving = transform.GetComponent<Movement>().moving;
        isGoingUp = transform.GetComponent<Movement>().isGoingUp;
        isGoingDown = transform.GetComponent<Movement>().isGoingDown;
        isGoingSideways = transform.GetComponent<Movement>().isGoingSideways;

        cubesInPlace = CubeController.cubesInPlace;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopRewind();
        }

        if (cubesInPlace == true)
        {

            if (isRewinding)
            {
                Rewind();
            }
            else
            {
                Record();
            }
        }
    }

    void Rewind()
    {

        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            canvas.rotation = pointInTime.rotation;
            myRigidBody.velocity = pointInTime.velocity;
            up = pointInTime.up;
            down = pointInTime.down;
            sideways = pointInTime.sideways;
            if (myRigidBody.velocity.z < 0)
            {
                up = true;
                down = false;
                sideways = false;
            }
            if (myRigidBody.velocity.z > 0)
            {
                up = false;
                down = true;
                sideways = false;
            }
            if (myRigidBody.velocity.x != 0)
            {
                up = false;
                down = false;
                sideways = true;
            }

            myAnim.SetBool("Up", up);
            myAnim.SetBool("Down", down);
            myAnim.SetBool("Vertical", sideways);
            pointsInTime.RemoveAt(0);
            acceleration += Time.deltaTime;
            if (acceleration > 0.8 && pointsInTime.Count > 0) pointsInTime.RemoveAt(0);
            if (acceleration > 1.4 && pointsInTime.Count > 1) pointsInTime.RemoveAt(0);
            if (acceleration > 1.8 && pointsInTime.Count > 2) pointsInTime.RemoveAt(0);
            if (acceleration > 2 && pointsInTime.Count > 3) pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (isMoving == true)
        {
            pointsInTime.Insert(0, new PointInTime(transform, canvas,
                myRigidBody.velocity, isGoingUp, isGoingDown, isGoingSideways));
        }
    }

    void StartRewind()
    {
        isRewinding = true;
        myRigidBody.isKinematic = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        acceleration = 0;
        myRigidBody.isKinematic = false;

    }

}
