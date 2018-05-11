using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopInMiddleOld : MonoBehaviour
{
    private RaycastHit hit;

    public LayerMask layerMask;
    public float stopSpeed = 100f;
    public GameObject canvas;

    public bool ray1;
    public bool ray2;
    public bool ray3;
    public bool ray4;
    public bool isStepped;
    private bool isCanvasRotating;
    private bool isRewinding = false;
    public bool activeVertical = false;
    public bool activeHorizontal = false;
    public GameObject r;
    public GameObject g;
    public GameObject b;

    private bool isMovingR = false;
    private bool isMovingG = false;
    private bool isMovingB = false;




    // Use this for initialization
    void Start()
    {
        isCanvasRotating = canvas.GetComponent<CanvasRotation>().isCanvasRotating;
        layerMask = LayerMask.GetMask("Player");
        isStepped = Physics.Raycast(transform.position, Vector3.up, out hit, 64);
        Vector3 forward = transform.TransformDirection(Vector3.up) * 64;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        isRewinding = GameObject.FindGameObjectWithTag("Player").GetComponent<RewindTime>().isRewinding;

        isMovingR = r.GetComponent<Movement>().moving;
        isMovingG = g.GetComponent<Movement>().moving;
        isMovingB = b.GetComponent<Movement>().moving;
        //isMoving = hit.transform.GetComponent<Movement>().moving;

        ray1 = Physics.Raycast(new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), Vector3.up, out hit, 64, layerMask);
        ray2 = Physics.Raycast(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Vector3.up, out hit, 64, layerMask);
        ray3 = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Vector3.up, out hit, 64, layerMask);
        ray4 = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), Vector3.up, out hit, 64, layerMask);
        if (ray1 || ray2 || ray3 || ray4)
        {
            isStepped = true;
        }
        else
        {
            isStepped = false;
            activeHorizontal = false;
            activeVertical = false;
        }

        if (isStepped)
        {

            //Up or Down Movement
            /*if (isMoving == false)
            {
                activeVertical = true;
                activeHorizontal = true;
            }*/

            //Up or Down Movement
            if ((!(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            && (isMovingR == false && isMovingG == false && isMovingB == false))
            {
                activeVertical = true;
            }

            if (((!(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
            || (!(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))))
            && (isMovingR == false && isMovingG == false && isMovingB == false))
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
                hit.transform.position = Vector3.MoveTowards(hit.transform.position,
                new Vector3(transform.position.x, transform.position.y + 46, transform.position.z), stopSpeed * Time.deltaTime);
                if (hit.transform.position == new Vector3(transform.position.x, transform.position.y + 46, transform.position.z)
                    || hit.rigidbody.velocity == new Vector3(0, 0, 0))
                {
                    activeVertical = false;
                }
            }

            if (activeHorizontal == true || isCanvasRotating == true)
            {
                hit.transform.position = Vector3.MoveTowards(hit.transform.position,
                new Vector3(transform.position.x, transform.position.y + 46, transform.position.z), stopSpeed * Time.deltaTime);
                if (hit.transform.position == new Vector3(transform.position.x, transform.position.y + 46, transform.position.z)
                    || hit.rigidbody.velocity == new Vector3(0, 0, 0))
                {
                    activeHorizontal = false;
                }
            }

            //Just a precaution in case the character doesn't get forced to the middle
            if (!Input.anyKey && (isMovingR == false && isMovingG == false && isMovingB == false) && isCanvasRotating == true)
            {
                activeHorizontal = true;
                activeVertical = true;
            }
            else
            {
                isStepped = false;
                activeVertical = false;
                activeHorizontal = false;
            }

        }
        else
        {
            isStepped = false;
            activeVertical = false;
            activeHorizontal = false;
        }
    }
}
