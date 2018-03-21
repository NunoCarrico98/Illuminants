using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePerspective : MonoBehaviour {
    
    public bool initCam;
	// Use this for initialization
	void Start () {
        initCam = Camera.main.orthographic;

    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.Space))
        {
            Camera.main.orthographic = !initCam;
        }
        initCam = Camera.main.orthographic;

    }
}
