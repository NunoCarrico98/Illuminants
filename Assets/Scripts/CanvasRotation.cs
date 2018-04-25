using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    public GameObject[] character = new GameObject[3];

    public GameObject myGO;
    public float speed = 3f;
    public float timer;
    public bool isCanvasRotating;

    private Transform myTransform;
    private Vector3 lookPos;
    private Quaternion rotationQ;
    private Quaternion rotationE;
    private int qKeyCount;
    private int eKeyCount;
    private float angleY;
    private float resetTimer;
    private float resetSpeed;
    private float angleQ = -90;
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
        resetSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && timer == resetTimer && !Input.anyKey)
        {
            lookPos = myTransform.position - transform.position;
            lookPos.y = 0;
            rotationQ = Quaternion.LookRotation(lookPos);
            rotationQ *= Quaternion.Euler(0, angleY - 90, 0);
            qKeyCount = 1;
        }
        if (qKeyCount == 1)
        {
            isCanvasRotating = true;
            timer -= Time.deltaTime;
            //FreezeCharacter();
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationQ, Time.deltaTime * speed);
            if(timer/2 < resetTimer/3 ) speed += 100 * Time.deltaTime; //accelarate when a two thirds of time have passed
            character[0].transform.rotation = Quaternion.Slerp(transform.rotation, rotationQ, Time.deltaTime * speed);
            character[1].transform.rotation = Quaternion.Slerp(transform.rotation, rotationQ, Time.deltaTime * speed);
            character[2].transform.rotation = Quaternion.Slerp(transform.rotation, rotationQ, Time.deltaTime * speed);
            if (timer <= 0)
            {
                timer = resetTimer;
                //UnfreezeCharacter();
                angleY = myTransform.rotation.eulerAngles.y;
                lookPos = myTransform.position - transform.position;
                lookPos.y = 0;
                rotationQ = Quaternion.LookRotation(lookPos);
                rotationQ *= Quaternion.Euler(0, angleY - 90, 0);
                qKeyCount = 0;
                isCanvasRotating = false;
                speed = resetSpeed;
            }

        }
        if (Input.GetKeyUp(KeyCode.E) && timer == resetTimer && !Input.anyKey)
        {
            lookPos = myTransform.position - transform.position;
            lookPos.y = 0;
            rotationE = Quaternion.LookRotation(lookPos);
            rotationE *= Quaternion.Euler(0, angleY + 90, 0);
            eKeyCount = 1;
        }
        if (eKeyCount == 1)
        {
            isCanvasRotating = true;
            timer -= Time.deltaTime;
            //FreezeCharacter();
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationE, Time.deltaTime * speed);
            if (timer / 2 < resetTimer / 3) speed += 100 * Time.deltaTime; //accelarate when a two thirds of time have passed
            character[0].transform.rotation = Quaternion.Slerp(transform.rotation, rotationE, Time.deltaTime * speed);
            character[1].transform.rotation = Quaternion.Slerp(transform.rotation, rotationE, Time.deltaTime * speed);
            character[2].transform.rotation = Quaternion.Slerp(transform.rotation, rotationE, Time.deltaTime * speed);

            if (timer <= 0)
            {
                timer = resetTimer;
                //UnfreezeCharacter();
                angleY = myTransform.rotation.eulerAngles.y;
                lookPos = myTransform.position - transform.position;
                lookPos.y = 0;
                rotationE = Quaternion.LookRotation(lookPos);
                rotationE *= Quaternion.Euler(0, angleY + 90, 0);
                eKeyCount = 0;
                isCanvasRotating = false;
                speed = resetSpeed;
            }

        }
    }


    void FreezeCharacter()
    {
        character[0].GetComponent<Rigidbody>().isKinematic = true;
        character[1].GetComponent<Rigidbody>().isKinematic = true;
        character[2].GetComponent<Rigidbody>().isKinematic = true;
    }

    void UnfreezeCharacter()
    {
        character[0].GetComponent<Rigidbody>().isKinematic = false;
        character[1].GetComponent<Rigidbody>().isKinematic = false;
        character[2].GetComponent<Rigidbody>().isKinematic = false;
    }
}


