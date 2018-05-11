using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public float fallingSpeedForCharacters = 100f;
    public float startingHeightForCharacters = 500f;
    public float fallingSpeedForObjects = 200f;
    public float startingHeightForRedPortal = 200f;
    public float startingHeightForGreenPortal = 225f;
    public float startingHeightForBluePortal = 250f;
    public float startingHeightForRedStart = 200f;
    public float startingHeightForGreenStart = 225f;
    public float startingHeightForBlueStart = 250f;
    public float risingSpeed = 400f;
    public float timeInterval = 0.1f;

    private Transform characters;
    private Transform redPortal;
    private Transform greenPortal;
    private Transform bluePortal;
    private Transform redSP;
    private Transform greenSP;
    private Transform blueSP;

    public bool objectsInPlace = false;

    private float timer = 0;
    private bool activeFinalAnims;
    private float redPortalInitHeight;
    private float greenPortalInitHeight;
    private float bluePortalInitHeight;

    // Use this for initialization
    void Start () {

        characters = GameObject.FindGameObjectWithTag("Characters").transform;
        redPortal = GameObject.FindGameObjectWithTag("RedPortal").transform;
        greenPortal = GameObject.FindGameObjectWithTag("GreenPortal").transform;
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal").transform;
        redSP = GameObject.FindGameObjectWithTag("RedStart").transform;
        greenSP = GameObject.FindGameObjectWithTag("GreenStart").transform;
        blueSP = GameObject.FindGameObjectWithTag("BlueStart").transform;

        redPortalInitHeight = redPortal.transform.position.y;
        greenPortalInitHeight = greenPortal.transform.position.y;
        bluePortalInitHeight = bluePortal.transform.position.y;

        characters.position = new Vector3(characters.position.x,
            characters.position.y + startingHeightForCharacters, characters.position.z);

        redPortal.position = new Vector3(redPortal.position.x,
            redPortal.position.y + startingHeightForRedPortal, redPortal.position.z);

        greenPortal.position = new Vector3(greenPortal.position.x,
            greenPortal.position.y + startingHeightForGreenPortal, greenPortal.position.z);

        bluePortal.position = new Vector3(bluePortal.position.x,
            bluePortal.position.y + startingHeightForBluePortal, bluePortal.position.z);

        redSP.position = new Vector3(redSP.position.x,
            redSP.position.y + startingHeightForRedStart, redSP.position.z);

        greenSP.position = new Vector3(greenSP.position.x,
            greenSP.position.y + startingHeightForGreenStart, greenSP.position.z);

        blueSP.position = new Vector3(blueSP.position.x,
            blueSP.position.y + startingHeightForBlueStart, blueSP.position.z);

    }
	
	// Update is called once per frame
	void Update () {

        activeFinalAnims = characters.GetComponent<NextLevel>().activeFinalAnims;
        if (activeFinalAnims == false && objectsInPlace == false)
        {
            Spawn();
        }
        else if (activeFinalAnims == true)
        {
            SendToHeaven();
        }

    }

    void Spawn()
    {
        fallingSpeedForCharacters += 4f;
        fallingSpeedForObjects += 2f;

        //Move the players to this position
        characters.position = Vector3.MoveTowards(characters.position,
                new Vector3(characters.position.x, 0, characters.position.z), fallingSpeedForCharacters * Time.deltaTime);

        //Move other objects
        redPortal.position = Vector3.MoveTowards(redPortal.position, 
            new Vector3(redPortal.position.x, 31, redPortal.position.z), fallingSpeedForObjects * Time.deltaTime);

        greenPortal.position = Vector3.MoveTowards(greenPortal.position,
                new Vector3(greenPortal.position.x, 31, greenPortal.position.z), fallingSpeedForObjects * Time.deltaTime);

        bluePortal.position = Vector3.MoveTowards(bluePortal.position,
                new Vector3(bluePortal.position.x, 31, bluePortal.position.z), fallingSpeedForObjects * Time.deltaTime);

        redSP.position = Vector3.MoveTowards(redSP.position,
                new Vector3(redSP.position.x, 31, redSP.position.z), fallingSpeedForObjects * Time.deltaTime);

        greenSP.position = Vector3.MoveTowards(greenSP.position,
                new Vector3(greenSP.position.x, 31, greenSP.position.z), fallingSpeedForObjects * Time.deltaTime);

        blueSP.position = Vector3.MoveTowards(blueSP.position,
                new Vector3(blueSP.position.x, 31, blueSP.position.z), fallingSpeedForObjects * Time.deltaTime);

        if (characters.position.y == 0) objectsInPlace = true;
    }

    void SendToHeaven()
    {
        timer += Time.deltaTime;

        if (timer >= 0.1)
        {
            //Move the players to this position
            characters.position = Vector3.MoveTowards(characters.position,
                    new Vector3(characters.position.x, 2000, characters.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval)
        {
            //Move other objects
            redPortal.position = Vector3.MoveTowards(redPortal.position,
                    new Vector3(redPortal.position.x, 2000, redPortal.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval * 2)
        {
            greenPortal.position = Vector3.MoveTowards(greenPortal.position,
                new Vector3(greenPortal.position.x, 2000, greenPortal.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval * 3)
        {
            bluePortal.position = Vector3.MoveTowards(bluePortal.position,
                new Vector3(bluePortal.position.x, 2000, bluePortal.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval*4)
        {
            redSP.position = Vector3.MoveTowards(redSP.position,
                new Vector3(redSP.position.x, 2000, redSP.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval*5)
        {
            greenSP.position = Vector3.MoveTowards(greenSP.position,
                new Vector3(greenSP.position.x, 2000, greenSP.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval*6)
        {
            blueSP.position = Vector3.MoveTowards(blueSP.position,
                new Vector3(blueSP.position.x, 2000, blueSP.position.z), risingSpeed * Time.deltaTime);
        }
    }
}
