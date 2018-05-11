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
        }
        if(activeFinalAnims == true)
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