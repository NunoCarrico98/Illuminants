using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour {

    public GameObject myGO;
    private Camera cam1, cam2, cam3, cam4;
    public int keyCount = 0;

	// Use this for initialization
	void Start () {
        cam1 = myGO.transform.Find("cam1").GetComponent<Camera>();
        cam2 = myGO.transform.Find("cam2").GetComponent<Camera>();
        cam3 = myGO.transform.Find("cam3").GetComponent<Camera>();
        cam4 = myGO.transform.Find("cam4").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Tab))
        {
            keyCount += 1;
        }
        if (keyCount == 1)
        {
            cam2.enabled = true;
            cam1.enabled = false;
        }
        if (keyCount == 2)
        {
            cam3.enabled = true;
            cam2.enabled = false;
        }
        if (keyCount == 3)
        {
            cam4.enabled = true;
            cam3.enabled = false;
        }
        if (keyCount == 4)
        {
            cam1.enabled = true;
            cam4.enabled = false;
            keyCount = 0;
        }
    }
}
