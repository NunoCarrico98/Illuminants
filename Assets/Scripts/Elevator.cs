using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public Transform myElevator;
    public float elevatorSpeed = 10;
    public float waitingTime = 2;

    private bool cubesInPlace;
    private bool elevatorOn;
    private bool lapCompleted = false;
    private Vector3 topPosition;
    private Vector3 downPosition;
    private bool ascend = false;
    private bool descend = false;
    private float timer = 0;
    private float timer2 = 0;

	// Use this for initialization
	void Start () {
        topPosition = new Vector3(myElevator.position.x, 32, myElevator.position.z);
        downPosition = new Vector3(myElevator.position.x, 0, myElevator.position.z);
        ChangeLayer();


    }
	
	// Update is called once per frame
	void Update ()
    {
        cubesInPlace = CubeController.cubesInPlace;

        if (cubesInPlace == true) StartElevator();

        if(elevatorOn == true)
        {
            ElevatorOn();
        }
    }

    private void StartElevator()
    {
        elevatorOn = true;
    }

    private void ElevatorOn()
    {
        if (lapCompleted == true)
        {
            ResetVars();
        }

        timer += Time.deltaTime;

        if(timer >= waitingTime)
        {
            ascend = true;
        }

        if(ascend == true)
        {
            myElevator.position = Vector3.MoveTowards(myElevator.position, topPosition, elevatorSpeed * Time.deltaTime);
            if (myElevator.position == topPosition)
            {
                timer2 += Time.deltaTime;
                if (timer2 >= waitingTime)
                {
                    ascend = false;
                    timer = 0;
                    descend = true;
                }
            }
        }

        if(descend == true)
        {
            myElevator.position = Vector3.MoveTowards(myElevator.position, downPosition, elevatorSpeed * Time.deltaTime);
            if (myElevator.position == downPosition)
            {
                lapCompleted = true;
            }
        }
    }

    private void ResetVars()
    {
        timer = 0;
        timer2 = 0;
        ascend = false;
        descend = false;
        lapCompleted = false;
    }

    private void ChangeLayer()
    {
        SpriteRenderer sprite = myElevator.Find("New Sprite (0)").GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 2;
    }

}
