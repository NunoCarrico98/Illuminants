using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

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
    private float ySpritePos;

    //sprite related variables
    public int floorSortingOrderUp = 4;
    public int wallsSortingOrderUp = 2;
    public int floorSortingOrderDown = 0;
    public int wallsSortingOrderDown = -1;
    private SpriteRenderer sprite;      //sprites from the walls and top of the cube
    private SpriteRenderer sprite_verify;

    // Use this for initialization
    void Start()
    {
        topPosition = new Vector3(myElevator.position.x, 32, myElevator.position.z);
        downPosition = new Vector3(myElevator.position.x, 0, myElevator.position.z);
        ChangeLayer();


    }

    // Update is called once per frame
    void Update()
    {
        cubesInPlace = CubeController.cubesInPlace;

        if (cubesInPlace == true) StartElevator();

        if (elevatorOn == true)
        {
            ElevatorOn();
        }

        ChangeLayer();
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

        if (timer >= waitingTime)
        {
            ascend = true;
        }

        if (ascend == true)
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

        if (descend == true)
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
        //sprite_verify = myElevator.Find("New Sprite (0)").GetComponent<SpriteRenderer>();
        //sprite_verify.sortingOrder = 2;

        sprite_verify = myElevator.transform.Find("New Sprite (0)").GetComponent<SpriteRenderer>();
        ySpritePos = sprite_verify.transform.position.y;

        for (int i = 1; i < 6; i++)
        {

            if (ySpritePos == 31)
            {
                sprite = myElevator.transform.Find("New Sprite (" + i + ")").GetComponent<SpriteRenderer>();
                sprite_verify.sortingOrder = floorSortingOrderDown;
                sprite.sortingOrder = wallsSortingOrderDown;
            }
            if (ySpritePos > 31 && ySpritePos <= 64)
            {
                sprite = myElevator.transform.Find("New Sprite (" + i + ")").GetComponent<SpriteRenderer>();
                sprite_verify.sortingOrder = floorSortingOrderUp;
                sprite.sortingOrder = wallsSortingOrderUp;

            }
        }

    }

}
