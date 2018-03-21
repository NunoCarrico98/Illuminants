using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotation : MonoBehaviour
{

    public GameObject canvas;
    public int keyCount;
    private Transform canvasRotation;
    private Transform rotationAngle;
    public float speed = -3;
    private Quaternion startRot;
    private Quaternion endRot;
    public float angleX, angleY, angleZ;
    // Use this for initialization
    void Start()
    {
        //rotationAngle = Quaternion.AngleAxis(-90, Vector3.up);
        keyCount = 0;
        canvasRotation = canvas.transform;
        //startRot = Quaternion.LookRotation(transform.forward);
        //endRot = Quaternion.LookRotation(transform.right);
        //angleX = canvasRotation.rotation.eulerAngles.x;
        angleY = canvasRotation.rotation.eulerAngles.y;
        //angleZ = canvasRotation.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        //rotationAngle = Quaternion.AngleAxis(-90, Vector3.up * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.Q))
        {
            keyCount = 1;
            if (keyCount == 1 && angleY <= 0 && angleY > -90)
            {

                //canvasRotation.Rotate(0, -speed, 0);
                canvasRotation.Rotate(Vector3.up * -speed);
                //canvasRotation.Rotate(Vector3.up * speeed * Time.deltaTime , Space.World);
                //canvasRotation = Quaternion.Slerp(canvasRotation, rotationAngle, Time.time * speed);
                //transform.rotation = Quaternion.Slerp(startRot, endRot, Time.time * speed);
                //canvasRotation = rotationAngle;

                Debug.Log("Está tudo bem 1");
                if (angleY <= -90)
                {
                    keyCount = 0;
                }
            }
            if (keyCount == 1 && angleY <= -90 && angleY > -180)
            {
                canvasRotation.Rotate(Vector3.up * -speed);
                if (canvasRotation.rotation.y == -180)
                {
                    keyCount = 0;
                }

            }
            if (keyCount == 1 && angleY == -180 && angleY > -270)
            {
                canvasRotation.Rotate(Vector3.up * -speed);
                if (canvasRotation.rotation.y == -270)
                {
                    keyCount = 0;
                }

            }
            if (keyCount == 1 && angleY == -270 && angleY > -360)
            {
                canvasRotation.Rotate(Vector3.up * -speed);
                if (angleY == -360)
                {
                    keyCount = 0;
                    canvasRotation.Rotate(0, 360, 0);       //Reset canvas rotation to 0
                }
                keyCount = 0;

            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            keyCount = 1;
            if (keyCount == 1 && angleY <= 0 && angleY > -90)
            {

                //canvasRotation.Rotate(0, -speed, 0);
                canvasRotation.Rotate(Vector3.up * speed);
                //canvasRotation.Rotate(Vector3.up * speeed * Time.deltaTime , Space.World);
                //canvasRotation = Quaternion.Slerp(canvasRotation, rotationAngle, Time.time * speed);
                //transform.rotation = Quaternion.Slerp(startRot, endRot, Time.time * speed);
                //canvasRotation = rotationAngle;

                Debug.Log("Está tudo bem 1");
                if (angleY <= -90)
                {
                    keyCount = 0;
                }
            }
            if (keyCount == 1 && angleY <= -90 && angleY > -180)
            {
                canvasRotation.Rotate(Vector3.up * speed);
                if (canvasRotation.rotation.y == -180)
                {
                    keyCount = 0;
                }

            }
            if (keyCount == 1 && angleY == -180 && angleY > -270)
            {
                canvasRotation.Rotate(Vector3.up * speed);
                if (canvasRotation.rotation.y == -270)
                {
                    keyCount = 0;
                }

            }
            if (keyCount == 1 && angleY == -270 && angleY > -360)
            {
                canvasRotation.Rotate(Vector3.up * speed);
                if (angleY == -360)
                {
                    keyCount = 0;
                    canvasRotation.Rotate(0, 360, 0);       //Reset canvas rotation to 0
                }
                keyCount = 0;

            }
        }
    }
}
