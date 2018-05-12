﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public float fallingSpeedForCharacters = 2000f;
    public float startingHeightForCharacters = 3000f;
    public float fallingSpeedForObjects = 2000f;
    public float startingHeightForRedPortal = 1000f;
    public float startingHeightForGreenPortal = 1500f;
    public float startingHeightForBluePortal = 2000f;
    public float startingHeightForRedStart = 2500f;
    public float startingHeightForGreenStart = 3000f;
    public float startingHeightForBlueStart = 3500f;
    public float risingSpeed = 2000f;
    public float timeInterval = 0.1f;
    public float beginSpawnTimer = 0.0f;
    public float bifrostGrowTime = 0.5f;
    private float adder = 0;

    public float redPortalTimer = 1f;
    public float greenPortalTimer = 1.5f;
    public float bluePortalTimer = 2f;
    private float portalSpawnTimer = 0f;

    private Transform characters;
    private Transform redPortal;
    private Transform greenPortal;
    private Transform bluePortal;
    private Transform redSP;
    private Transform greenSP;
    private Transform blueSP;

    public bool objectsInPlace = false;
    public bool portalsInPlace = false;
    public bool charactersInPlace = false;
    public bool portalsGone = false;
    public bool bifrostActive = false;

    private float timer = 0;
    private float redPortalInitHeight;
    private float greenPortalInitHeight;
    private float bluePortalInitHeight;
    private bool activeFinalAnims;

    private SpriteRenderer spriteRed;
    private SpriteRenderer spriteGreen;
    private SpriteRenderer spriteBlue;
    private SpriteRenderer spriteRSP;
    private SpriteRenderer spriteGSP;
    private SpriteRenderer spriteBSP;
    private SpriteRenderer spriteRSP2;
    private SpriteRenderer spriteGSP2;
    private SpriteRenderer spriteBSP2;
    private Color redSpriteColor;
    private Color greenSpriteColor;
    private Color blueSpriteColor;
    private float alpha;

    private Transform bifrosts;
    private Transform redBifrost;
    private Transform blueBifrost;
    private Transform greenBifrost;

    private Transform redChar;
    private Transform greenChar;
    private Transform blueChar;
    private bool charactersInHeaven = false;

    public float fadeInTime = 2f;

    // Use this for initialization
    void Start()
    {

        characters = GameObject.FindGameObjectWithTag("Characters").transform;
        redChar = characters.Find("Red_Player");
        greenChar = characters.Find("Green_Player");
        blueChar = characters.Find("Blue_Player");

        redPortal = GameObject.FindGameObjectWithTag("RedPortal").transform;
        greenPortal = GameObject.FindGameObjectWithTag("GreenPortal").transform;
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal").transform;

        //Spawn points
        redSP = GameObject.FindGameObjectWithTag("RedStart").transform;
        greenSP = GameObject.FindGameObjectWithTag("GreenStart").transform;
        blueSP = GameObject.FindGameObjectWithTag("BlueStart").transform;

        //Sprites of the spawn points
        spriteRSP2 = redSP.Find("RedSP").GetComponent<SpriteRenderer>();
        spriteGSP2 = greenSP.Find("GreenSP").GetComponent<SpriteRenderer>();
        spriteBSP2 = blueSP.Find("BlueSP").GetComponent<SpriteRenderer>();

        //Bifrost beams
        bifrosts = GameObject.FindGameObjectWithTag("Bifrosts").transform;
        redBifrost = bifrosts.Find("Red").transform.Find("Sprite");
        greenBifrost = bifrosts.Find("Green").transform.Find("Sprite");
        blueBifrost = bifrosts.Find("Blue").transform.Find("Sprite");

        //Inicial heights for the portals
        redPortalInitHeight = redPortal.transform.position.y;
        greenPortalInitHeight = greenPortal.transform.position.y;
        bluePortalInitHeight = bluePortal.transform.position.y;

        //Sprite Renderers of the portals
        spriteRed = redPortal.GetComponent<SpriteRenderer>();
        spriteGreen = greenPortal.GetComponent<SpriteRenderer>();
        spriteBlue = bluePortal.GetComponent<SpriteRenderer>();

        //Sprites of the RGB Symbols of the portals of the portals
        spriteRSP = redPortal.Find("1").Find("RedSP").GetComponent<SpriteRenderer>();
        spriteGSP = greenPortal.Find("2").Find("GreenSP").GetComponent<SpriteRenderer>();
        spriteBSP = bluePortal.Find("3").Find("BlueSP").GetComponent<SpriteRenderer>();

        //Bifrosts start with 0 scale on X axis
        BifrostsStartSize();

        //Initialize portals with 0 opacity
        TransparentPortals();

        //We call this 2 methods to make the characters start in a hight Y position;
        CharactersStartPositionForFall();

        redSP.position = new Vector3(redSP.position.x,
            redSP.position.y + startingHeightForRedStart, redSP.position.z);

        greenSP.position = new Vector3(greenSP.position.x,
            greenSP.position.y + startingHeightForGreenStart, greenSP.position.z);

        blueSP.position = new Vector3(blueSP.position.x,
            blueSP.position.y + startingHeightForBlueStart, blueSP.position.z);
        //ObjectsStartPositionsForFall();

    }

    // Update is called once per frame
    void Update()
    {
        activeFinalAnims = characters.GetComponent<NextLevel>().activeFinalAnims;
        if (activeFinalAnims == false && objectsInPlace == false)
        {
            beginSpawnTimer -= Time.deltaTime;
            if (beginSpawnTimer <= 0)
            {
                Spawn();
            }
        }
        if (activeFinalAnims == true)
        {
            SendToHeaven();
        }

    }

    private void Spawn()
    {
        //Characters spawn with a bifrost-like effect
        if (portalsInPlace == true && charactersInPlace == false)
        {
            ActiveBifrost();
            FallingCharacters();
        }
        if (charactersInPlace == true && objectsInPlace == false)
        {
            ByeBifrost();
            if (adder == 0) objectsInPlace = true;
        }

        if (portalsInPlace == false)
        {
            //Portals spawn with a fade
            FadeInPortals();
        }

        //Objects spawn by falling from the sky
        //FallingObjects();
    }

    private void SendToHeaven()
    {
        //timer += Time.deltaTime;
        if (charactersInHeaven == false)
        {
            ActiveBifrost();
            if (adder >= 32)
            {
                CharactersGoToHeaven();
            }
        }

        if (charactersInHeaven == true /*&& adder > 0*/)
        {
            ByeBifrost();
        //}
        //if (adder == 0)
        //{
            FadeOutPortals();
        }


        //ObjectsGoToHeaven();
    }

    private void BifrostsStartSize()
    {
        redBifrost.transform.localScale = new Vector3(0, 0, 0);
        greenBifrost.transform.localScale = new Vector3(0, 0, 0);
        blueBifrost.transform.localScale = new Vector3(0, 0, 0);
    }

    private void ActiveBifrost()
    {
        bifrostActive = true;
        redBifrost.position = new Vector3(redChar.position.x, redBifrost.position.y, redChar.position.z);
        greenBifrost.position = new Vector3(greenChar.position.x, greenBifrost.position.y, greenChar.position.z);
        blueBifrost.position = new Vector3(blueChar.position.x, blueBifrost.position.y, blueChar.position.z);

        if (adder <= 32)
        {
            adder += bifrostGrowTime;
        }
        if (adder > 32) adder = 32;
        redBifrost.transform.localScale = new Vector3(adder, 480, 32);
        greenBifrost.transform.localScale = new Vector3(adder, 480, 32);
        blueBifrost.transform.localScale = new Vector3(adder, 480, 32);

    }

    private void ByeBifrost()
    {
        if (adder >= 0)
        {
            adder -= bifrostGrowTime;
        }
        if (adder <= 0)
        {
            adder = 0;
            //bifrostActive = false;
        }
        redBifrost.transform.localScale = new Vector3(adder, 480, 32);
        greenBifrost.transform.localScale = new Vector3(adder, 480, 32);
        blueBifrost.transform.localScale = new Vector3(adder, 480, 32);
        if (redBifrost.transform.localScale.x == 0) bifrostActive = false;
    }

    private void FadeInPortals()
    {

        portalSpawnTimer += Time.deltaTime;
        if (portalSpawnTimer >= redPortalTimer)
        {
            redSpriteColor = spriteRed.color;
            redSpriteColor.a += fadeInTime / 100;
            spriteRed.color = redSpriteColor;
            spriteRSP.color = redSpriteColor;
        }
        if (portalSpawnTimer >= greenPortalTimer)
        {
            greenSpriteColor = spriteGreen.color;
            greenSpriteColor.a += fadeInTime / 100;
            spriteGreen.color = greenSpriteColor;
            spriteGSP.color = greenSpriteColor;
        }
        if (portalSpawnTimer >= bluePortalTimer)
        {
            blueSpriteColor = spriteBlue.color;
            blueSpriteColor.a += fadeInTime / 100;
            spriteBlue.color = blueSpriteColor;
            spriteBSP.color = blueSpriteColor;
        }

        if(blueSpriteColor.a >= 0.7) portalsInPlace = true;
        if (blueSpriteColor.a >= 1)
        {
            portalSpawnTimer = 0;
        }
    }

    private void FadeOutPortals()
    {
        portalSpawnTimer += Time.deltaTime;
        if (portalSpawnTimer >= redPortalTimer)
        {
            redSpriteColor = spriteRed.color;
            redSpriteColor.a -= fadeInTime / 100;
            spriteRed.color = redSpriteColor;
            spriteRSP.color = redSpriteColor;
            spriteRSP2.color = redSpriteColor;
        }
        if (portalSpawnTimer >= greenPortalTimer)
        {
            greenSpriteColor = spriteGreen.color;
            greenSpriteColor.a -= fadeInTime / 100;
            spriteGreen.color = greenSpriteColor;
            spriteGSP.color = greenSpriteColor;
            spriteGSP2.color = greenSpriteColor;
        }
        if (portalSpawnTimer >= bluePortalTimer)
        {
            blueSpriteColor = spriteBlue.color;
            blueSpriteColor.a -= fadeInTime / 100;
            spriteBlue.color = blueSpriteColor;
            spriteBSP.color = blueSpriteColor;
            spriteBSP2.color = blueSpriteColor;
        }

        if (blueSpriteColor.a <= 0) portalsGone = true;
    }

    private void TransparentPortals()
    {
        redSpriteColor = spriteRed.color;
        greenSpriteColor = spriteGreen.color;
        blueSpriteColor = spriteBlue.color;
        redSpriteColor.a = 0;
        greenSpriteColor.a = 0;
        blueSpriteColor.a = 0;
        spriteRed.color = redSpriteColor;
        spriteGreen.color = greenSpriteColor;
        spriteBlue.color = blueSpriteColor;
        spriteRSP.color = redSpriteColor;
        spriteGSP.color = greenSpriteColor;
        spriteBSP.color = blueSpriteColor;
    }

    private void CharactersStartPositionForFall()
    {
        characters.position = new Vector3(characters.position.x,
            characters.position.y + startingHeightForCharacters, characters.position.z);
    }

    private void ObjectsStartPositionsForFall()
    {
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

    private void FallingCharacters()
    {
        //Create an accelaration on the fall
        fallingSpeedForCharacters += 4f;

        characters.position = Vector3.MoveTowards(characters.position,
                new Vector3(characters.position.x, 0, characters.position.z), fallingSpeedForCharacters * Time.deltaTime);

        if (characters.position.y == 0)
        {
            redSP.position = new Vector3(redSP.position.x, 31, redSP.position.z);
            greenSP.position = new Vector3(greenSP.position.x, 31, greenSP.position.z);
            blueSP.position = new Vector3(blueSP.position.x, 31, blueSP.position.z);

            charactersInPlace = true;
        }
    }

    private void FallingObjects()
    {
        //Create an acceleration on the fall
        fallingSpeedForObjects += 2f;

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
    }



    private void CharactersGoToHeaven()
    {
        //if (timer >= 0.1)

        //Move the players to this position
        characters.position = Vector3.MoveTowards(characters.position,
                new Vector3(characters.position.x, 2000, characters.position.z), risingSpeed * Time.deltaTime);

        if (characters.position.y >= 500)
        {
            charactersInHeaven = true;
        }

    }

    private void ObjectsGoToHeaven()
    {

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
        if (timer >= 0.1 + timeInterval * 4)
        {
            redSP.position = Vector3.MoveTowards(redSP.position,
                new Vector3(redSP.position.x, 2000, redSP.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval * 5)
        {
            greenSP.position = Vector3.MoveTowards(greenSP.position,
                new Vector3(greenSP.position.x, 2000, greenSP.position.z), risingSpeed * Time.deltaTime);
        }
        if (timer >= 0.1 + timeInterval * 6)
        {
            blueSP.position = Vector3.MoveTowards(blueSP.position,
                new Vector3(blueSP.position.x, 2000, blueSP.position.z), risingSpeed * Time.deltaTime);
        }

    }
}
