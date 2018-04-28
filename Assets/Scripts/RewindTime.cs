using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewindTime : MonoBehaviour {

    public bool isRewinding;

    private List<PointInTime> pointsInTime;
    private Rigidbody myRigidBody;
    private Animation animationInspector;
    private AnimationClip anim;
    private bool cubesInPlace;
    private bool isMoving;


    // Use this for initialization
    void Start () {
        pointsInTime = new List<PointInTime>();
        myRigidBody = GetComponent<Rigidbody>();
        animationInspector = GameObject.Find("Face").GetComponent<Animation>();
        anim = animationInspector.clip;
    }
	
	// Update is called once per frame
	void Update () {

        anim = animationInspector.clip;

        isMoving = transform.GetComponent<Movement>().moving;
        cubesInPlace = CubeController.cubesInPlace;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopRewind();
        }
    }

    private void FixedUpdate()
    {
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
            transform.rotation = pointInTime.rotation;
            myRigidBody.velocity = pointInTime.velocity;
            myRigidBody.angularVelocity = pointInTime.angularVelocity;
            anim = pointInTime.animation;
            pointsInTime.RemoveAt(0);
            pointsInTime.RemoveAt(1);
            pointsInTime.RemoveAt(2);
            pointsInTime.RemoveAt(3);
            pointsInTime.RemoveAt(4);
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
            pointsInTime.Insert(0, new PointInTime(transform,
                myRigidBody.velocity, myRigidBody.angularVelocity, anim));
        }
    }

    void StartRewind()
    {
        isRewinding = true;
        //myRigidBody.isKinematic = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        //myRigidBody.isKinematic = false;
        ReapplyForces();
    }

    public void ReapplyForces()
    {
        myRigidBody.position = pointsInTime[0].position;
        myRigidBody.rotation = pointsInTime[0].rotation;
        myRigidBody.velocity = pointsInTime[0].velocity;
        myRigidBody.angularVelocity = pointsInTime[0].angularVelocity;
    }
}
