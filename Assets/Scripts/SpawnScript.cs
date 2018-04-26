using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public float fallingSpeed = 100f;
    public float startingHeight = 500f;
    private bool cubesInPlace = false;
    private GameObject characters;
    private bool activeFinalAnims;


    // Use this for initialization
    void Start () {
        characters = GameObject.FindGameObjectWithTag("Characters");
        transform.position = new Vector3(transform.position.x, transform.position.y + startingHeight, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {


        activeFinalAnims = characters.GetComponent<NextLevel>().activeFinalAnims;
        if (activeFinalAnims == false)
        {
            fallingSpeed += 2f;
            cubesInPlace = GameObject.FindGameObjectWithTag("Line").transform.Find("MyCube (1)").GetComponent<CubeController>().cubesInPlace;
            transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x, -5, transform.position.z), fallingSpeed * Time.deltaTime);
        }

	}
}
