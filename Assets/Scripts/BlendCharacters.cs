using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendCharacters : MonoBehaviour {

    public GameObject[] characters = new GameObject[7];
    private GameObject ahr, gee, bee, magent, yellow, cyan, whitey;
    private SpriteRenderer ahrSprite, geeSprite, beeSprite, magentSprite, yellowSprite, cyanSprite, whiteySprite;
    private bool isWhite = false;

    // Use this for initialization
    void Start () {

        //Invoke the respective characters
        ahr = characters[0];
        gee = characters[1];
        bee = characters[2];
        magent = characters[3];
        yellow = characters[4];
        cyan = characters[5];
        whitey = characters[6];

        ahrSprite = ahr.transform.Find("Face").GetComponent<SpriteRenderer>();
        geeSprite = gee.transform.Find("Face").GetComponent<SpriteRenderer>();
        beeSprite = bee.transform.Find("Face").GetComponent<SpriteRenderer>();
        magentSprite = magent.transform.Find("Face").GetComponent<SpriteRenderer>();
        yellowSprite = yellow.transform.Find("Face").GetComponent<SpriteRenderer>();
        cyanSprite = cyan.transform.Find("Face").GetComponent<SpriteRenderer>();
        whiteySprite = whitey.transform.Find("Face").GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        VerifyPositions();
	}

    private void VerifyPositions()
    {
        //Red and Green
        if (((ahr.transform.position.x == gee.transform.position.x)
            && (ahr.transform.position.y == gee.transform.position.y)
            && (ahr.transform.position.z == gee.transform.position.z)) && isWhite == false)
        {
            BlendYellow();
            Debug.Log("Mudou po amarelo");
        }

        //Red and Blue
        if (((ahr.transform.position.x == bee.transform.position.x)
            && (ahr.transform.position.y == bee.transform.position.y)
            && (ahr.transform.position.z == bee.transform.position.z))&& isWhite == false)
        {
            BlendMagent();
        }

        //Green and Blue
        if (((bee.transform.position.x == gee.transform.position.x)
            && (bee.transform.position.y == gee.transform.position.y)
            && (bee.transform.position.z == gee.transform.position.z)) && isWhite == false)
        {
            BlendCyan();
        }

        //Red, Green and Blue
        if (((ahr.transform.position.x == gee.transform.position.x)
            && (ahr.transform.position.y == gee.transform.position.y)
            && (ahr.transform.position.z == gee.transform.position.z))
            && ((ahr.transform.position.x == bee.transform.position.x)
            && (ahr.transform.position.y == bee.transform.position.y)
            && (ahr.transform.position.z == bee.transform.position.z)))
        {
            BlendWhitey();
        }
    }

    private void BlendMagent()
    {
        magentSprite.enabled = true;
        ahrSprite.enabled = false;
        beeSprite.enabled = false;

    }

    private void BlendYellow()
    {
        yellowSprite.enabled = true;
        ahrSprite.enabled = false;
        geeSprite.enabled = false;
    }

    private void BlendCyan()
    {
        cyanSprite.enabled = true;
        beeSprite.enabled = false;
        geeSprite.enabled = false;
    }

    private void BlendWhitey()
    {
        whiteySprite.enabled = true;
        ahrSprite.enabled = false;
        geeSprite.enabled = false;
        beeSprite.enabled = false;
        magentSprite.enabled = false;
        yellowSprite.enabled = false;
        cyanSprite.enabled = false;
        isWhite = true;
    }
}
