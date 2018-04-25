using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorOrWall : MonoBehaviour
{
    public float speed = 20f;
    public float timer = 2f;
    public bool cubesInPlace = false;

    private Transform myCube;
    private float height;
    private float initHeight;
    private int firstCheck = 0;
    private GameObject[] myCubes;
    private float[] myCubeHeight;
    private bool activeFinalAnims = false;
    private GameObject characters;
    private float velocity;
    private float currentPos;
    private float lastPos;
    private float resetTimer;

    // Use this for initialization
    void Start()
    {
        characters = GameObject.FindGameObjectWithTag("Characters");
        myCube = gameObject.transform;
        height = myCube.position.y;
        initHeight = height;
        resetTimer = timer;
        lastPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position.y;
        cubesInPlace = false;
        RiseToPosition();


        activeFinalAnims = characters.GetComponent<NextLevel>().activeFinalAnims;
        if (activeFinalAnims == true) ResetLevel();

        velocity = (lastPos - currentPos);

        lastPos = transform.position.y;
    }

    public void ResetLevel()
    {
        if (height >= 0)
        {
            //myCube.Translate(Vector3.down * Time.deltaTime * speed * 10);
           transform.position = Vector3.MoveTowards(myCube.position,
                new Vector3(transform.position.x, 0, transform.position.z), speed * Time.deltaTime);
        }

    }


    public void RiseToPosition()
    {
        height = myCube.position.y;
        if (initHeight == 32 && firstCheck == 0)
        {
            firstCheck = 1;
            myCube.position = (new Vector3(myCube.position.x, 0, myCube.position.z));
        }
        if (height >= 0 && height <= 32 && firstCheck == 1)
        {
            timer -= Time.deltaTime;

            //Wait x seconds for the cubes to start rising
            if (timer <= 0)
            {
                transform.position = Vector3.MoveTowards(myCube.position,
                new Vector3(transform.position.x, 32, transform.position.z), speed * Time.deltaTime);
            }
            if (height == 32 && velocity == 0) cubesInPlace = true;
        }
    }
}
