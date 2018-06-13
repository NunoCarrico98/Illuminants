using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendCharacters : MonoBehaviour
{

    public GameObject[] characters = new GameObject[7];

    public bool isMagent = false;
    public bool isYellow = false;
    public bool isCyan = false;
    public bool isWhite = false;


    private GameObject ahr, gee, bee, magent, yellow, cyan, whitey;
    private SpriteRenderer ahrSprite, geeSprite, beeSprite, magentSprite, yellowSprite, cyanSprite, whiteySprite;

    private bool isRewinding;
    private bool isBlended = false;

    // Use this for initialization
    void Start()
    {

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


        PlayerPrefs.GetInt("FirstBlend", 0);

    }

    // Update is called once per frame
    void Update()
    {

        isRewinding = ahr.GetComponent<RewindTime>().isRewinding;

        if (isRewinding == false)
        {
            VerifyPositions();
        }

        if (isRewinding)
        {
            isMagent = false;
            isYellow = false;
            isCyan = false;
            isWhite = false;
        }
    }

    private void VerifyPositions()
    {

        //If none of them have the same positions
        if (Input.GetKeyUp(KeyCode.Space) &&
        (((ahr.transform.position.x > gee.transform.position.x + 25)
        || (ahr.transform.position.x < gee.transform.position.x - 25)
        || (ahr.transform.position.y > gee.transform.position.y + 25)
        || (ahr.transform.position.y < gee.transform.position.y - 25)
        || (ahr.transform.position.z > gee.transform.position.z + 25)
        || (ahr.transform.position.z < gee.transform.position.z - 25))
        && (ahr.transform.position.x > bee.transform.position.x + 25)
        || (ahr.transform.position.x < bee.transform.position.x - 25)
        || (ahr.transform.position.y > bee.transform.position.y + 25)
        || (ahr.transform.position.y < bee.transform.position.y - 25)
        || (ahr.transform.position.z > bee.transform.position.z + 25)
        || (ahr.transform.position.z < bee.transform.position.z - 25)))
        {
            DontBlend();
        }
        //Red and Green
        if (((ahr.transform.position.x >= gee.transform.position.x - 5
            && ahr.transform.position.x <= gee.transform.position.x + 5)
            && (ahr.transform.position.y >= gee.transform.position.y - 5
            && ahr.transform.position.y <= gee.transform.position.y + 5)
            && (ahr.transform.position.z >= gee.transform.position.z - 5)
            && ahr.transform.position.z <= gee.transform.position.z + 5)
            && isWhite == false)
        {
            BlendYellow();
        }

        //Red and Blue
        if (((ahr.transform.position.x >= bee.transform.position.x - 5
            && ahr.transform.position.x <= bee.transform.position.x + 5)
            && (ahr.transform.position.y >= bee.transform.position.y - 5
            && ahr.transform.position.y <= bee.transform.position.y + 5)
            && (ahr.transform.position.z >= bee.transform.position.z - 5)
            && ahr.transform.position.z <= bee.transform.position.z + 5)
            && isWhite == false)
        {
            BlendMagent();
        }

        //Green and Blue
        if (((bee.transform.position.x >= gee.transform.position.x - 5
            && bee.transform.position.x <= gee.transform.position.x + 5)
            && (bee.transform.position.y >= gee.transform.position.y - 5
            && bee.transform.position.y <= gee.transform.position.y + 5)
            && (bee.transform.position.z >= gee.transform.position.z - 5)
            && bee.transform.position.z <= gee.transform.position.z + 5)
            && isWhite == false)
        {
            BlendCyan();
        }

        //Red, Green and Blue
        if (((ahr.transform.position.x >= bee.transform.position.x - 5
            && ahr.transform.position.x <= bee.transform.position.x + 5)
            && (ahr.transform.position.y >= bee.transform.position.y - 5
            && ahr.transform.position.y <= bee.transform.position.y + 5)
            && (ahr.transform.position.z >= bee.transform.position.z - 5)
            && ahr.transform.position.z <= bee.transform.position.z + 5)
            && ((ahr.transform.position.x >= gee.transform.position.x - 5
            && ahr.transform.position.x <= gee.transform.position.x + 5)
            && (ahr.transform.position.y >= gee.transform.position.y - 5
            && ahr.transform.position.y <= gee.transform.position.y + 5)
            && (ahr.transform.position.z >= gee.transform.position.z - 5)
            && ahr.transform.position.z <= gee.transform.position.z + 5))
        {
            BlendWhitey();
        }
    }

    private void DontBlend()
    {
        ahrSprite.enabled = true;
        geeSprite.enabled = true;
        beeSprite.enabled = true;
        magentSprite.enabled = false;
        yellowSprite.enabled = false;
        cyanSprite.enabled = false;
        whiteySprite.enabled = false;
        isBlended = false;
        isMagent = false;
        isYellow = false;
        isCyan = false;
        isWhite = false;
    }

    private void BlendMagent()
    {
        magentSprite.enabled = true;
        ahrSprite.enabled = false;
        beeSprite.enabled = false;
        isBlended = true;
        isMagent = true;
        isYellow = false;
        isCyan = false;
        isWhite = false;

        if (PlayerPrefs.GetInt("FirstBlend") == 0) PlayerPrefs.SetInt("FirstBlend", 1);
    }

    private void BlendYellow()
    {
        yellowSprite.enabled = true;
        ahrSprite.enabled = false;
        geeSprite.enabled = false;
        isBlended = true;
        isMagent = false;
        isYellow = true;
        isCyan = false;
        isWhite = false;

        if (PlayerPrefs.GetInt("FirstBlend") == 0) PlayerPrefs.SetInt("FirstBlend", 1);
    }

    private void BlendCyan()
    {
        cyanSprite.enabled = true;
        beeSprite.enabled = false;
        geeSprite.enabled = false;
        isBlended = true;
        isMagent = false;
        isYellow = false;
        isCyan = true;
        isWhite = false;

        if (PlayerPrefs.GetInt("FirstBlend") == 0) PlayerPrefs.SetInt("FirstBlend", 1);
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
        isBlended = true;
        isWhite = true;

        if (PlayerPrefs.GetInt("FirstBlend") == 0) PlayerPrefs.SetInt("FirstBlend", 1);
    }
}
