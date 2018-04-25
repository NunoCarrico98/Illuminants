using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopInMiddle : MonoBehaviour
{
    private RaycastHit hit;

    public LayerMask layerMask;
    public float stopSpeed = 100f;
    public GameObject canvas;
    private bool isCanvasRotating;

    public bool ray1;
    public bool ray2;
    public bool ray3;
    public bool ray4;
    public bool isStepped;
    public bool activeVertical = false;
    public bool activeHorizontal = false;




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
            if (((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && !Input.anyKeyDown)
            || ((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) && !Input.anyKeyDown))
            {
                activeVertical = true;
            }

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            || (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                activeVertical = false;
            }

            //Left or Right Movement
            if (((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) && !Input.anyKeyDown)
                || ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) && !Input.anyKeyDown))
            {
                activeHorizontal = true;
            }
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                || (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                activeHorizontal = false;
            }

            if (activeVertical == true || isCanvasRotating == true)
            {
                hit.transform.position = Vector3.MoveTowards(hit.collider.transform.position,
                new Vector3(transform.position.x, transform.position.y + 48, transform.position.z ), stopSpeed * Time.deltaTime);
                if (hit.collider.transform.position == new Vector3(transform.position.x, transform.position.y + 48, transform.position.z)
                    || hit.rigidbody.velocity == new Vector3(0, 0, 0))
                {
                    activeVertical = false;
                }
            }

            if (activeHorizontal == true || isCanvasRotating == true)
            {
                hit.collider.transform.position = Vector3.MoveTowards(hit.collider.transform.position,
                new Vector3(transform.position.x, transform.position.y + 48, transform.position.z), stopSpeed * Time.deltaTime);
                if (hit.collider.transform.position == new Vector3(transform.position.x, transform.position.y + 48, transform.position.z)
                    || hit.rigidbody.velocity == new Vector3(0, 0, 0))
                {
                    activeHorizontal = false;
                }
            }

            //Just a precaution in case the character doesn't get forced to the middle
            if(!Input.anyKey)
            {
                activeHorizontal = true;
                activeVertical = true;
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
