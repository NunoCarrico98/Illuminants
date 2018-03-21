using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotate2 : MonoBehaviour
{
    public GameObject myGO;
    public Transform myTransform;
    public Vector3 lookPos;
    public Quaternion rotationQ;
    public Quaternion rotationE;
    public int qKeyCount;
    public int eKeyCount;
    public float speed = 3f;
    public float angleYQ;
    public float angleYE;
    public float timer;
    private float resetTimer;
    public float angleQ = 90;
    public float angleE = -90;
    // Use this for initialization
    void Start()
    {
        myTransform = myGO.transform;
        lookPos = myTransform.position - transform.position;
        lookPos.y = 0;
        rotationQ = Quaternion.LookRotation(lookPos);
        rotationQ *= Quaternion.Euler(0, -90, 0); // this adds a -90 degrees Y rotation
        rotationE = Quaternion.LookRotation(lookPos);
        rotationE *= Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation
        angleYQ = myTransform.rotation.eulerAngles.y;
        angleYE = myTransform.rotation.eulerAngles.y;
        resetTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            qKeyCount = 1;
        }
        if (qKeyCount == 1)
        {
            angleYQ = myTransform.rotation.eulerAngles.y;
            //angleYE = 0;
            timer -= Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationQ, Time.deltaTime * speed);
            if (timer <= 0)
            {
                qKeyCount = 0;
                timer = resetTimer;
                lookPos = myTransform.position - transform.position;
                lookPos.y = 0;
                rotationQ = Quaternion.LookRotation(lookPos);
                if ((angleYQ >= -1 && angleYQ <= 1) || (angleYQ >= 359 && angleYQ <= 361))
                {
                    angleQ = -90;
                }
                if (angleYQ >= 269 && angleYQ <= 271)
                {
                    angleQ = -180;
                }
                if (angleYQ >= 179 && angleYQ <= 181)
                {
                    angleQ = -270;
                }
                if (angleYQ >= 89 && angleYQ <= 91)
                {
                    angleQ = -360;
                }
                rotationQ *= Quaternion.Euler(0, angleQ, 0);
            }
            angleE = angleQ + 90;
            angleYE = angleYQ + 90;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            eKeyCount = 1;
        }
        if (eKeyCount == 1)
        {
            timer -= Time.deltaTime;
            angleYE = myTransform.rotation.eulerAngles.y;
            //angleYQ = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationE, Time.deltaTime * (speed));
            if (timer <= 0)
            {
                eKeyCount = 0;
                timer = resetTimer;
                lookPos = myTransform.position - transform.position;
                lookPos.y = 0;
                rotationE = Quaternion.LookRotation(lookPos);
                if ((angleYE >= -1 && angleYE <= 1) || (angleYE >= 359 && angleYE <= 361))
                {
                    angleE = 90;
                }
                if (angleYE >= 89 && angleYE <= 91)
                {
                    angleE = 180;
                }
                if (angleYE >= 179 && angleYE <= 181)
                {
                    angleE = 270;
                }
                if (angleYE >= 269 && angleYE <= 271)
                {
                    angleE = 360;
                }
                rotationE *= Quaternion.Euler(0, angleE, 0);
            }
            angleQ = angleE - 90;
            angleYQ = angleYE - 90;
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
