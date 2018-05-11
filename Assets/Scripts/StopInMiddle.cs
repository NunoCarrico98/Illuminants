using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopInMiddle : MonoBehaviour
{
    private RaycastHit hit;

    public LayerMask layerMask;
    public float stopSpeed = 100f;
    private GameObject canvas;

    public bool ray;
    public bool onCube;
    private bool isCanvasRotating;
    private bool isRewinding = false;
    public bool activeVertical = false;
    public bool activeHorizontal = false;

    private bool isMoving = false;




    // Use this for initialization
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        isCanvasRotating = canvas.GetComponent<CanvasRotation>().isCanvasRotating;
        layerMask = LayerMask.GetMask("Default");
        onCube = Physics.Raycast(transform.position, Vector3.up, out hit, 64);
        Vector3 forward = transform.TransformDirection(Vector3.up) * 64;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        isRewinding = GameObject.FindGameObjectWithTag("Player").GetComponent<RewindTime>().isRewinding;

        isMoving = transform.GetComponent<Movement>().moving;
        //isMoving = hit.transform.GetComponent<Movement>().moving;

        ray = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down, out hit, 64, layerMask);
        if (ray)
        {
            onCube = true;
        }
        else
        {
            onCube = false;
            activeHorizontal = false;
            activeVertical = false;
        }

        if (onCube)
        {

            //Up or Down Movement
            /*if (isMoving == false)
            {
                activeVertical = true;
                activeHorizontal = true;
            }*/

            //Up or Down Movement
            if ((!(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            && (isMoving == false)))
            {
                activeVertical = true;
            }

            //Left or Right Movement
            if ((!(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            && (isMoving == false)))
            {
                activeHorizontal = true;
            }



            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            || (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                activeVertical = false;
            }

            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                || (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                activeHorizontal = false;
            }

            if ((activeVertical == true || isCanvasRotating == true) && isRewinding == false)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(hit.transform.position.x, hit.transform.position.y + 46, hit.transform.position.z), stopSpeed * Time.deltaTime);
                if (transform.position == new Vector3(hit.transform.position.x, hit.transform.position.y + 46, hit.transform.position.z)
                    || transform.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
                {
                    activeVertical = false;
                }
            }

            if (activeHorizontal == true || isCanvasRotating == true)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(hit.transform.position.x, hit.transform.position.y + 46, hit.transform.position.z), stopSpeed * Time.deltaTime);
                if (transform.position == new Vector3(hit.transform.position.x, hit.transform.position.y + 46, hit.transform.position.z)
                    || transform.GetComponent<Rigidbody>().velocity == new Vector3(0, 0, 0))
                {
                    activeHorizontal = false;
                }
            }

            //Just a precaution in case the character doesn't get forced to the middle
            if (!Input.anyKey && (isMoving == false) && isCanvasRotating == true)
            {
                activeHorizontal = true;
                activeVertical = true;
            }
            else
            {
                onCube = false;
                activeVertical = false;
                activeHorizontal = false;
            }

        }
        else
        {
            onCube = false;
            activeVertical = false;
            activeHorizontal = false;
        }
    }
}
