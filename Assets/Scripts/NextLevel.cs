using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public bool activeFinalAnims = false;
    public Rigidbody myRigidBody_Red;
    public Rigidbody myRigidBody_Green;
    public Rigidbody myRigidBody_Blue;
    public float timerToChangeLevel = 4f;
    public bool playTransitionAnim = false;
    public float timeBetweenTransitions = 1f;
    public string loadLevel;

    private Transform redPortal;
    private Transform greenPortal;
    private Transform bluePortal;

    private void Start()
    {
        redPortal = GameObject.FindGameObjectWithTag("RedPortal").transform;
        greenPortal = GameObject.FindGameObjectWithTag("GreenPortal").transform;
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal").transform;
    }

    private void Update()
    {
        OnTriggerEnter();
    }
    // Use this for initialization
    public void OnTriggerEnter()
    {
        /*if (myRigidBody_Red.transform.position.x == portal_Red.transform.position.x
            && myRigidBody_Green.transform.position.x == portal_Green.transform.position.x
            && myRigidBody_Blue.transform.position.x == portal_Blue.transform.position.x
            && myRigidBody_Red.transform.position.z + 16 == portal_Red.transform.position.z
            && myRigidBody_Green.transform.position.z + 16 == portal_Green.transform.position.z
            && myRigidBody_Blue.transform.position.z + 16 == portal_Blue.transform.position.z)
        {
            SceneManager.LoadScene(loadLevel);
            //Application.LoadLevel(Level2);
        }*/

        if (myRigidBody_Red.GetComponent<Movement>().onPortal == true
            && myRigidBody_Green.GetComponent<Movement>().onPortal == true
            && myRigidBody_Blue.GetComponent<Movement>().onPortal == true)
        {
            activeFinalAnims = true;

            myRigidBody_Red.velocity = new Vector3(0,0,0);
            myRigidBody_Green.velocity = new Vector3(0, 0, 0);
            myRigidBody_Blue.velocity = new Vector3(0, 0, 0);

            myRigidBody_Red.position = Vector3.MoveTowards(myRigidBody_Red.position, 
                new Vector3(redPortal.position.x, myRigidBody_Red.position.y, redPortal.position.z), 50 * Time.deltaTime);
            myRigidBody_Green.position = Vector3.MoveTowards(myRigidBody_Green.position,
                new Vector3(greenPortal.position.x, myRigidBody_Green.position.y, greenPortal.position.z), 50 * Time.deltaTime);
            myRigidBody_Blue.position = Vector3.MoveTowards(myRigidBody_Blue.position,
                new Vector3(bluePortal.position.x, myRigidBody_Blue.position.y, bluePortal.position.z), 50 * Time.deltaTime);


        }
        if(activeFinalAnims == true && playTransitionAnim == false)
        {
            timerToChangeLevel -= Time.deltaTime;
            if (timerToChangeLevel <= 0)
            {
                activeFinalAnims = false;
                SceneManager.LoadScene(loadLevel);
            }
        }
        if (playTransitionAnim == true)
        {
            activeFinalAnims = true;
        }
    }
}