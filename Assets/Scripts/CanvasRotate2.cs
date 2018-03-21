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
    public float timer;
    private float resetTimer;
    public float angle = 90;
    // Use this for initialization
    void Start()
    {
        myTransform = myGO.transform;
        lookPos = myTransform.position - transform.position;
        lookPos.y = 0;
        rotation = Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(0, -90, 0); // this adds a 90 degrees Y rotation
        angleY = myTransform.rotation.eulerAngles.y;
        resetTimer = timer;
    }
    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            keyCount = 1;
            angleY = myTransform.rotation.eulerAngles.y;
            if (keyCount == 1)
            {
                timer -= Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
                if (timer <= 0)
                {
                    keyCount = 0;
                    timer = resetTimer;
                    lookPos = myTransform.position - transform.position;
                    lookPos.y = 0;
                    rotation = Quaternion.LookRotation(lookPos);
                    if (angleY >= -1 && angleY <= 1)
                    {
                        angle = -90;
                    }
                    if (angleY >=269 && angleY <= 271)
                    {
                        angle = -180;
                    }
                    if (angleY >= 179 && angleY <= 181)
                    {
                        angle = -270;
                    }
                    if (angleY >= 89 && angleY <= 91)
                    {
                        angle = -360;
                    }
                rotation *= Quaternion.Euler(0, angle, 0);
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
