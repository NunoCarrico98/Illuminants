using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot3 : MonoBehaviour
{

    public GameObject myGO;
    public Transform myTransform;
    public Vector3 lookPos;
    public Quaternion rotationQ;
    public Quaternion rotationE;
    public int qKeyCount;
    public int qKeyCount2 = 2;
    public int eKeyCount;
    public float speed = 3f;
    public float angleY;
    public float timer;
    private float resetTimer;
    public float angleQ = -90;
    public float angleE = 90;
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
        angleY = myTransform.rotation.eulerAngles.y;
        resetTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            qKeyCount = 1;
            qKeyCount2 += 1;
            //angleY = myTransform.rotation.eulerAngles.y;
        }
        if (qKeyCount == 1)
        {
            timer -= Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationQ, Time.deltaTime * speed);
            if (timer <= 0)
            {
                timer = resetTimer;
                lookPos = myTransform.position - transform.position;
                lookPos.y = 0;
                rotationQ = Quaternion.LookRotation(lookPos);
                Debug.Log("STEP1");
                rotationQ *= Quaternion.Euler(0, -angleQ * qKeyCount2, 0);
                Debug.Log("STEP2");
                qKeyCount = 0;
            }

        }
    /* if (Input.GetKeyUp(KeyCode.E))
        eKeyCount += 1;
    angleY = myTransform.rotation.eulerAngles.y;
    if (qKeyCount >= 1)
    {
        timer -= Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationE, Time.deltaTime * speed);
        if (timer <= 0)
        {
            timer = resetTimer;
            lookPos = myTransform.position - transform.position;
            lookPos.y = 0;
            rotationQ = Quaternion.LookRotation(lookPos);
            rotationQ *= Quaternion.Euler(0, angleQ * eKeyCount, 0);
        }*/
    }
}

