using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotation : MonoBehaviour {

    public GameObject canvas;
    public int keyCount = 0;
    private Transform canvasRotation;
    public float speed;
	// Use this for initialization
	void Start () {
        canvasRotation = canvas.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            keyCount = 1;
            if (keyCount == 1 && canvasRotation.rotation.y <= 0 && canvasRotation.rotation.y > -90)
            {

                canvasRotation.Rotate(0, -speed, 0);
                if (canvasRotation.rotation.y == -90)
                {
                    keyCount = 0;
                }
            }
            if (keyCount == 1 && canvasRotation.rotation.y <= -90 && canvasRotation.rotation.y > -180)
            {
                canvasRotation.Rotate(0, -speed, 0);
                if (canvasRotation.rotation.y == -180)
                {
                    keyCount = 0;
                }

            }
            if (keyCount == 1 && canvasRotation.rotation.y == -180 && canvasRotation.rotation.y > -270)
            {
                canvasRotation.Rotate(0, -speed, 0);
                if (canvasRotation.rotation.y == -270)
                {
                    keyCount = 0;
                }

            }
            if (keyCount == 1 && canvasRotation.rotation.y == -270 && canvasRotation.rotation.y > -360)
            {
                canvasRotation.Rotate(0, -speed, 0);
                if (canvasRotation.rotation.y == -360)
                {
                    keyCount = 0;
                    canvasRotation.Rotate(0, 360, 0);       //Reset canvas rotation to 0
                }
                keyCount = 0;

            }
        }
	}
}
