using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    private Movement arrived;
    public Rigidbody2D myRigidBody_Red;
    public Rigidbody2D myRigidBody_Green;
    public Rigidbody2D myRigidBody_Blue;
    public GameObject portal_Red;
    public GameObject portal_Green;
    public GameObject portal_Blue;
    public string loadLevel;


    // Use this for initialization
    public void OnTriggerEnter()
    {
        if (myRigidBody_Red.transform.position == portal_Red.transform.position
            && myRigidBody_Green.transform.position == portal_Green.transform.position
            && myRigidBody_Blue.transform.position == portal_Blue.transform.position)
        {
            SceneManager.LoadScene(loadLevel);
            //Application.LoadLevel(Level2);
        }
    }
}