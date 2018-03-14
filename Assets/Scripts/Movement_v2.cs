using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_v2 : MonoBehaviour {

 // Normal Movements Variables
     public float speed = 1f;
    public Vector2 playerPos;
     public GameObject player;
 
     void Start()
     {
        playerPos = player.transform.position;
     }
 
     void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerPos = new Vector2 (0, 2) * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerPos = new Vector2(-2, 0) * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerPos = new Vector2(0, -2) * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerPos = new Vector2(2, 0) * speed;
        }


    }
}