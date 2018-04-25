using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public float fallingSpeed = 100f;
    private bool cubesInPlace = false;


    // Use this for initialization
    void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y + 400, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

        fallingSpeed += 2f;
        cubesInPlace = GameObject.FindGameObjectWithTag("Line").transform.Find("MyCube (1)").GetComponent<FloorOrWall>().cubesInPlace;
            transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x, -5, transform.position.z), fallingSpeed * Time.deltaTime);

	}
}
