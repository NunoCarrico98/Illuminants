using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam2 : MonoBehaviour
{

    public bool camera2D1;
    public bool camera2D2;
    public bool camera3D1;

    private GameObject[] cams;
    private Camera cam2D1;
    private Camera cam2D2;
    private Camera cam3D;
    // Use this for initialization
    void Start()
    {

        cams = GameObject.FindGameObjectsWithTag("MainCamera");

        cam2D1 = cams[0].GetComponent<Camera>();
        cam2D2 = cams[1].GetComponent<Camera>();
        cam3D = cams[2].GetComponent<Camera>();


        if (camera2D1)
        {
            cam2D1.enabled = true;
            cam2D2.enabled = false;
            cam3D.enabled = false;
        }
        else if (camera2D2)
        {
            cam2D1.enabled = false;
            cam2D2.enabled = true;
            cam3D.enabled = false;
        }
        else if (camera3D1)
        {
            cam2D1.enabled = false;
            cam2D2.enabled = false;
            cam3D.enabled = true;
        }
        else
        {
            Debug.Log("NO CAMERA SELECTED!!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
