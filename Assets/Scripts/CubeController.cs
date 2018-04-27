using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float speed = 20f;
    public float animSpeed = 50f;
    public float timer = 2f;
    public float timerForReset = 2f;
    public float animTimer = 2f;
    public int numberOfReps = 0;
    public bool cubesInPlace = false;

    private float height;
    private float initHeight;
    private int firstCheck = 0;
    private bool activeFinalAnims = false;
    private GameObject characters;
    private float velocity;
    private float currentPos;
    private float lastPos;
    private float resetTimer;
    private bool imAnUpCube = false;
    private float timer2 = 2.5f;
    private bool reset = false;
    private bool up = true;
    private bool down = false;
    private bool normal = false;
    private bool objectsInPlace = false;

    // Use this for initialization
    void Start()
    {
        characters = GameObject.FindGameObjectWithTag("Characters");
        height = transform.position.y;
        initHeight = height;
        resetTimer = timer;
        lastPos = transform.position.y;
        up = true;
        down = false;
        normal = false;

        if (initHeight == 32 && firstCheck == 0)
        {
            firstCheck = 1;
            imAnUpCube = true;
            transform.position = (new Vector3(transform.position.x, 0, transform.position.z));
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position.y;

        objectsInPlace = GameObject.FindGameObjectWithTag("Objects").GetComponent<SpawnScript>().objectsInPlace;

        cubesInPlace = false;

        //after x seconds, this method will no longer be called;
        if (timer2 >= 0 && objectsInPlace == true)
        {
            timer2 -= Time.deltaTime;
            RiseToPosition();
        }


        activeFinalAnims = characters.GetComponent<NextLevel>().activeFinalAnims;
    }

    private void FixedUpdate()
    {
        if (activeFinalAnims == true)
        {
            ResetLevel();
            if (reset) TransitionAnimation();
        }

        velocity = (lastPos - currentPos);

        lastPos = transform.position.y;
    }

    public void ResetLevel()
    {
        timerForReset -= Time.deltaTime;
        if (imAnUpCube == true && reset == false)
        {
            //transform.GetComponent<Collider>().enabled = false;
            //transform.Translate(Vector3.down * Time.deltaTime * 10 * speed);
            transform.position = Vector3.MoveTowards(transform.position,
                 new Vector3(transform.position.x, 0, transform.position.z), speed * 10 * Time.deltaTime);
        }
        if (timerForReset <= 0)
        {
            reset = true;
        }

    }


    public void RiseToPosition()
    {
        height = transform.position.y;
        if (height >= 0 && height <= 32 && firstCheck == 1)
        {
            timer -= Time.deltaTime;

            //Wait x seconds for the cubes to start rising
            if (timer <= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, 32, transform.position.z), speed * Time.deltaTime);
            }
            if (height == 32 && velocity == 0) cubesInPlace = true;
        }
    }

    public void TransitionAnimation()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        players[0].transform.GetComponent<Rigidbody>().isKinematic = true;
        players[1].transform.GetComponent<Rigidbody>().isKinematic = true;
        players[2].transform.GetComponent<Rigidbody>().isKinematic = true;

        if (up)
        {
            animTimer -= Time.deltaTime;
            if (animTimer <= 1)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                         new Vector3(transform.position.x, 32, transform.position.z), animSpeed * Time.deltaTime);
                if (transform.position.y == 32)
                {
                    up = false;
                    down = true;
                    normal = true;
                }
            }
        }
        if (down)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                 new Vector3(transform.position.x, -32, transform.position.z), animSpeed * Time.deltaTime);
            if (transform.position.y == -32)
            {
                down = false;
                normal = true;
            }
        }
        if (normal)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                 new Vector3(transform.position.x, 0, transform.position.z), animSpeed * Time.deltaTime);
            if(transform.position.y == 0 && numberOfReps > 0)
            {
                numberOfReps--;
                up = true;
                down = false;
                normal = false;
            }
        }

    }
}
