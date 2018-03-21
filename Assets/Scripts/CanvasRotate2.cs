using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotate2 : MonoBehaviour
{
    public GameObject myGO;
    public Transform myTransform;
    public Vector3 lookPos;
    public Quaternion rotation;
    public int keyCount;
    public float speed = 3f;
    public float angleY;
    // Use this for initialization
    void Start()
    {
        myTransform = myGO.transform;
        lookPos = myTransform.position - transform.position;
        lookPos.y = 0;
        rotation = Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(0, -90, 0); // this adds a 90 degrees Y rotation
        angleY = myTransform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            keyCount = 1;
        {
            if (keyCount == 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
                if (angleY >= 89.9 && angleY <= 90.1)
                {
                    keyCount = 0;
                }

            }
        }
    }
}

/*
 *  var lookPos = target.position - transform.position;
 lookPos.y = 0;
 var rotation = Quaternion.LookRotation(lookPos);
 rotation *= Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation
 var adjustRotation = transform.rotation.y + rotationAdjust; //<- this is wrong!
 transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
 */
