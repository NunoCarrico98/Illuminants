using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    private Movement arrived;
    public Rigidbody myRigidBody_Red;
    public Rigidbody myRigidBody_Green;
    public Rigidbody myRigidBody_Blue;
    public GameObject portal_Red;
    public GameObject portal_Green;
    public GameObject portal_Blue;
    public string loadLevel;


    // Use this for initialization
    public void OnTriggerEnter()
    {
        if (myRigidBody_Red.transform.position.x == portal_Red.transform.position.x
            && myRigidBody_Green.transform.position.x == portal_Green.transform.position.x
            && myRigidBody_Blue.transform.position.x == portal_Blue.transform.position.x
            && myRigidBody_Red.transform.position.z == portal_Red.transform.position.z
            && myRigidBody_Green.transform.position.z == portal_Green.transform.position.z
            && myRigidBody_Blue.transform.position.z == portal_Blue.transform.position.z)
        {
            SceneManager.LoadScene(loadLevel);
            //Application.LoadLevel(Level2);
        }
    }
}