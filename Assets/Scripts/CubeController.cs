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


    public static bool cubesInPlace = false;

    private float height;
    private float initHeight;
    private int firstCheck = 0;
    private bool activeFinalAnims = false;
    private bool playTransitionAnim = false;
    private GameObject characters;
    private float velocity;
    private float currentPos;
    private float lastPos;
    private float resetTimer;
    private bool imAnUpCube = false;
    private float timer2 = 1f;
    private float timeBetweenTransitions;
    private float resetTransitionTimer;
    private int resetRepsNum;
    private bool reset = false;
    private bool noReps = false;
    private bool up = true;
    private bool down = false;
    private bool normal = false;
    private bool objectsInPlace = false;
    private bool isDefaultLevel = false;

    // Use this for initialization
    void Start()
    {
        characters = GameObject.FindGameObjectWithTag("Characters");
        height = transform.position.y;
        initHeight = height;
        resetTimer = timer;
        resetRepsNum = numberOfReps;
        lastPos = transform.position.y;

        timeBetweenTransitions = characters.GetComponent<NextLevel>().timeBetweenTransitions;
        playTransitionAnim = characters.GetComponent<NextLevel>().playTransitionAnim;

        resetTransitionTimer = timeBetweenTransitions;

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
        activeFinalAnims = characters.GetComponent<NextLevel>().activeFinalAnims;

        //after x seconds, this method will no longer be called;
        if (timer2 >= 0 && objectsInPlace == true)
        {
            timer2 -= Time.deltaTime;
            RiseToPosition();
        }


        if (activeFinalAnims == true)
        {
            ResetLevel();
            if (reset) TransitionAnimation();
        }
    }

    private void FixedUpdate()
    {
        velocity = (lastPos - currentPos);

        lastPos = transform.position.y;
    }

    public void ResetLevel()
    {
        timerForReset -= Time.deltaTime;
        if (imAnUpCube == true && reset == false)
        {
            cubesInPlace = false;
            transform.position = Vector3.MoveTowards(transform.position,
                 new Vector3(transform.position.x, 0, transform.position.z), speed * 10 * Time.deltaTime);
        }
        if (timerForReset <= 0 && velocity == 0)
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
            if (timer <= 1)
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
        players[3].transform.GetComponent<Rigidbody>().isKinematic = true;
        players[4].transform.GetComponent<Rigidbody>().isKinematic = true;
        players[5].transform.GetComponent<Rigidbody>().isKinematic = true;
        players[6].transform.GetComponent<Rigidbody>().isKinematic = true;

        if (up)
        {
            animTimer -= Time.deltaTime;
            if (animTimer <= 0.4)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                         new Vector3(transform.position.x, 32, transform.position.z), animSpeed * Time.deltaTime);
                if (transform.position.y == 32)
                {
                    up = false;
                    down = false;
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
            if (transform.position.y == 0 && numberOfReps >= 0)
            {
                noReps = false;
                if (playTransitionAnim == true && numberOfReps == 0)
                {
                    timeBetweenTransitions -= Time.deltaTime;
                    if (timeBetweenTransitions < 0)
                    {
                        up = true;
                        down = false;
                        normal = false;
                        numberOfReps = resetRepsNum;
                        timeBetweenTransitions = resetTransitionTimer;
                    }
                }
                else if (numberOfReps > 0)
                {
                    numberOfReps--;
                    up = true;
                    down = false;
                    normal = false;
                }

            }
            //Play animation infinitly if user activates that option (used for testing the animation)
            if (transform.position.y == 0 && playTransitionAnim == true && noReps == true)
            {
                //float reseter;
                //reseter = timeBetweenTransitions;

                timeBetweenTransitions -= Time.deltaTime;
                if (timeBetweenTransitions < 0)
                {
                    up = true;
                    down = false;
                    normal = false;
                    timeBetweenTransitions = resetTransitionTimer;
                }
            }
        }

    }
}

