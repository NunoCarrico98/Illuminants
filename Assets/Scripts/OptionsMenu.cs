using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    //Volume: 1
    //Reset Game: 2
    //Mute: 3
    //Volume Cubes: 4 - 12  (Volume: 12.5  Distance: 3.56f)

    public AudioMixer mixer;
    public Transform volumeButton;
    public Transform[] volumeCubes;
    public Transform resetButton;
    public Transform muteButton;
    public Transform popUp;
    public Sprite muteSprite;
    public Sprite muteLightSprite;
    public float cubesRiseSpeed = 200f;
    public float fadeInTime = 300f;
    public float fadeOutTime = 300f;


    private SpriteRenderer[] sprites;
    private Sprite resetTopSprite;
    private Sprite resetLightSprite;
    private Transform cam;
    private Transform newToppings;
    private Color[] spritesColor;
    private int buttonNumber = 0;
    private int volumeNumber = 5;
    private int resetNumber = 0;
    private bool changeVolume = false;
    private bool timeStopped;
    private float angle = 90f;
    private float acceleration = 100;
    private float initPos;
    private float[] newPos;
    private bool hasPressed = false;
    private bool hasPressed2 = false;
    private bool hasPressed3 = false;
    private bool hasPressed4 = false;
    private float counter1 = 0;
    private float counter2 = 0;
    private float counter3 = 0;
    private float counter4 = 0;




    // Use this for initialization
    void Start()
    {
        cam = Camera.main.transform;
        resetTopSprite = muteButton.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        resetLightSprite = muteButton.GetChild(5).GetComponent<SpriteRenderer>().sprite;
        //sprites = new SpriteRenderer[cubes.Length, 6];
        //spritesColor = new Color[cubes.Length, 6];
        sprites = new SpriteRenderer[81];
        spritesColor = new Color[81];
        //newToppings = GameObject.Find("NewToppings").transform;
        volumeNumber = PlayerPrefs.GetInt("Volume", 11);
        Debug.Log(PlayerPrefs.GetInt("Volume"));
        Debug.Log(volumeNumber);
        newPos = new float[8];

        initPos = resetButton.transform.position.y;

        for (int i = 0; i < volumeCubes.Length; i++)
        {
            //if it's any cube between the 2nd and the 8th
            if (i > 0)
            {
                newPos[i] = newPos[i - 1] + 4f;
            }
            //if it's the first cube
            else
            {
                newPos[i] = initPos + 4f;
            }
        }

        buttonNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        RiseVolume();
    }

    private void FixedUpdate()
    {

    }

    private void OverlaySprites()
    {
        for (int i = 0; i < 81; i++)
        {
            sprites[i] = newToppings.GetChild(i).GetComponent<SpriteRenderer>();
            spritesColor[i] = sprites[i].color;
            spritesColor[i].a += fadeInTime / 1000;
            sprites[i].color = spritesColor[i];
        }

    }

    private void RiseVolume()
    {
        switch (buttonNumber)
        {
            //VOLUME
            case 1:
                volumeButton.GetComponent<MenuMouseScript>().goUp = true;
                break;

            //RESET
            case 2:
                resetButton.GetComponent<MenuMouseScript>().goUp = true;
                break;

            //MUTE
            case 3:
                muteButton.GetComponent<MenuMouseScript>().goUp = true;
                volumeNumber = buttonNumber;
                break;

            //All others
            case 4:
                volumeCubes[0].position = Vector3.MoveTowards(volumeCubes[0].position,
                new Vector3(volumeCubes[0].position.x, initPos + 32, volumeCubes[0].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
            case 5:
                volumeCubes[1].position = Vector3.MoveTowards(volumeCubes[1].position,
                new Vector3(volumeCubes[1].position.x, initPos + 32, volumeCubes[1].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
            case 6:
                volumeCubes[2].position = Vector3.MoveTowards(volumeCubes[2].position,
                new Vector3(volumeCubes[2].position.x, initPos + 32, volumeCubes[2].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
            case 7:
                volumeCubes[3].position = Vector3.MoveTowards(volumeCubes[3].position,
                new Vector3(volumeCubes[3].position.x, initPos + 32, volumeCubes[3].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
            case 8:
                volumeCubes[4].position = Vector3.MoveTowards(volumeCubes[4].position,
                new Vector3(volumeCubes[4].position.x, initPos + 32, volumeCubes[4].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
            case 9:
                volumeCubes[5].position = Vector3.MoveTowards(volumeCubes[5].position,
                new Vector3(volumeCubes[5].position.x, initPos + 32, volumeCubes[5].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
            case 10:
                volumeCubes[6].position = Vector3.MoveTowards(volumeCubes[6].position,
                new Vector3(volumeCubes[6].position.x, initPos + 32, volumeCubes[6].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
            case 11:
                volumeCubes[7].position = Vector3.MoveTowards(volumeCubes[7].position,
                new Vector3(volumeCubes[7].position.x, initPos + 32, volumeCubes[7].position.z), cubesRiseSpeed * Time.deltaTime);
                volumeNumber = buttonNumber;
                break;
        }

        //Change sprites on mute button
        if(volumeNumber == 3)
        {
            muteButton.GetChild(0).GetComponent<SpriteRenderer>().sprite = muteSprite;
            muteButton.GetChild(5).GetComponent<SpriteRenderer>().sprite = muteLightSprite;
            mixer.SetFloat("Volume", -80);
        } else
        {
            muteButton.GetChild(0).GetComponent<SpriteRenderer>().sprite = resetTopSprite;
            muteButton.GetChild(5).GetComponent<SpriteRenderer>().sprite = resetLightSprite;
        }

        //Get
        if (volumeNumber >= 4)
        {
            volumeCubes[0].position = Vector3.MoveTowards(volumeCubes[0].position,
                new Vector3(volumeCubes[0].position.x, newPos[0], volumeCubes[0].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 4) mixer.SetFloat("Volume", -35);
        }
        else
        {
            volumeCubes[0].position = Vector3.MoveTowards(volumeCubes[0].position,
                new Vector3(volumeCubes[0].position.x, initPos, volumeCubes[0].position.z), cubesRiseSpeed * Time.deltaTime);
        }

        if (volumeNumber >= 5)
        {
            volumeCubes[1].position = Vector3.MoveTowards(volumeCubes[1].position,
                new Vector3(volumeCubes[1].position.x, newPos[1], volumeCubes[1].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 5) mixer.SetFloat("Volume", -30);
        }
        else
        {
            volumeCubes[1].position = Vector3.MoveTowards(volumeCubes[1].position,
                new Vector3(volumeCubes[1].position.x, initPos, volumeCubes[1].position.z), cubesRiseSpeed * Time.deltaTime);
        }

        if (volumeNumber >= 6)
        {
            volumeCubes[2].position = Vector3.MoveTowards(volumeCubes[2].position,
                new Vector3(volumeCubes[2].position.x, newPos[2], volumeCubes[2].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 6) mixer.SetFloat("Volume", -25);
        }
        else
        {
            volumeCubes[2].position = Vector3.MoveTowards(volumeCubes[2].position,
                new Vector3(volumeCubes[2].position.x, initPos, volumeCubes[2].position.z), cubesRiseSpeed * Time.deltaTime);
        }

        if (volumeNumber >= 7)
        {
            volumeCubes[3].position = Vector3.MoveTowards(volumeCubes[3].position,
                new Vector3(volumeCubes[3].position.x, newPos[3], volumeCubes[3].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 7) mixer.SetFloat("Volume", -20);
        }
        else
        {
            volumeCubes[3].position = Vector3.MoveTowards(volumeCubes[3].position,
                new Vector3(volumeCubes[3].position.x, initPos, volumeCubes[3].position.z), cubesRiseSpeed * Time.deltaTime);
        }

        if (volumeNumber >= 8)
        {
            volumeCubes[4].position = Vector3.MoveTowards(volumeCubes[4].position,
                new Vector3(volumeCubes[4].position.x, newPos[4], volumeCubes[4].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 8) mixer.SetFloat("Volume", -15);
        }
        else
        {
            volumeCubes[4].position = Vector3.MoveTowards(volumeCubes[4].position,
                new Vector3(volumeCubes[4].position.x, initPos, volumeCubes[4].position.z), cubesRiseSpeed * Time.deltaTime);
        }

        if (volumeNumber >= 9)
        {
            volumeCubes[5].position = Vector3.MoveTowards(volumeCubes[5].position,
                new Vector3(volumeCubes[5].position.x, newPos[5], volumeCubes[5].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 9) mixer.SetFloat("Volume", -10);
        }
        else
        {
            volumeCubes[5].position = Vector3.MoveTowards(volumeCubes[5].position,
                new Vector3(volumeCubes[5].position.x, initPos, volumeCubes[5].position.z), cubesRiseSpeed * Time.deltaTime);
        }

        if (volumeNumber >= 10)
        {
            volumeCubes[6].position = Vector3.MoveTowards(volumeCubes[6].position,
                new Vector3(volumeCubes[6].position.x, newPos[6], volumeCubes[6].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 10) mixer.SetFloat("Volume", -5);
        }
        else
        {
            volumeCubes[6].position = Vector3.MoveTowards(volumeCubes[6].position,
                new Vector3(volumeCubes[6].position.x, initPos, volumeCubes[6].position.z), cubesRiseSpeed * Time.deltaTime);
        }

        if (volumeNumber >= 11)
        {
            volumeCubes[7].position = Vector3.MoveTowards(volumeCubes[7].position,
                new Vector3(volumeCubes[7].position.x, newPos[7], volumeCubes[7].position.z), cubesRiseSpeed * Time.deltaTime);
            if (volumeNumber == 11) mixer.SetFloat("Volume", 0);
        }
        else
        {
            volumeCubes[7].position = Vector3.MoveTowards(volumeCubes[7].position,
                new Vector3(volumeCubes[7].position.x, initPos, volumeCubes[7].position.z), cubesRiseSpeed * Time.deltaTime);
        }


    }

    private void CheckInput()
    {
        int counter = 0;

        if (changeVolume)
        {
            PlayerPrefs.SetInt("Volume", volumeNumber);

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)
                || Input.GetButtonDown("AButton") || Input.GetButtonDown("BButton"))
            {
                changeVolume = false;
                buttonNumber = 1;
                counter++;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("AButton"))
            && counter == 0)
        {
            if (buttonNumber == 1 && !changeVolume)
            {
                resetNumber = buttonNumber;
                buttonNumber = volumeNumber;
                changeVolume = true;

            }


            if (timeStopped)
            {
                popUp.GetComponent<SpriteRenderer>().enabled = false;
                Time.timeScale = 1f;
                timeStopped = false;
                ResetData();
                counter++;
            }

            if (buttonNumber == 2 && counter == 0)
            {
                timeStopped = true;
                popUp.GetComponent<SpriteRenderer>().enabled = true;
                Time.timeScale = 0f;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("BButton"))
            && counter == 0)
        {
            int counter2 = 0;

            if (timeStopped)
            {
                popUp.GetComponent<SpriteRenderer>().enabled = false;
                Time.timeScale = 1f;
                timeStopped = false;
                counter2++;
            }

            if(!timeStopped && !changeVolume && counter2 == 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (!timeStopped)
        {

            if (!changeVolume)
            {
                //Down
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    buttonNumber++;
                    if (buttonNumber > 2) buttonNumber = 1;

                }


                if ((Input.GetAxis("LeftJoystickVertical") <= -0.8
                    || Input.GetAxis("ArrowsVertical") <= -0.8) && (!hasPressed || counter1 > 12f))
                {
                    hasPressed = true;
                    counter1 = 0;

                    buttonNumber++;
                    if (buttonNumber > 2) buttonNumber = 1;

                }

                if (Input.GetAxis("LeftJoystickVertical") > -0.8
                        && Input.GetAxis("ArrowsVertical") > -0.8)
                {
                    hasPressed = false;
                }

                if (hasPressed)
                {
                    counter1++;
                }

                //Up
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    buttonNumber--;
                    if (buttonNumber < 1) buttonNumber = 2;
                }

                if ((Input.GetAxis("LeftJoystickVertical") >= 0.8
                    || Input.GetAxis("ArrowsVertical") >= 0.8) && (!hasPressed2 || counter2 > 12f))
                {
                    hasPressed2 = true;
                    counter2 = 0;

                    buttonNumber--;
                    if (buttonNumber < 1) buttonNumber = 2;
                }

                if (Input.GetAxis("LeftJoystickVertical") < 0.8
                        && Input.GetAxis("ArrowsVertical") < 0.8)
                {
                    hasPressed2 = false;
                }

                if (hasPressed2)
                {
                    counter2++;
                }
            }

            //Left and Right
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (changeVolume)
                {
                    buttonNumber++;
                    if (buttonNumber > 11) buttonNumber = 11;
                }

            }

            if ((Input.GetAxis("LeftJoystickHorizontal") >= 0.8
                || Input.GetAxis("ArrowsHorizontal") >= 0.8) && (!hasPressed3 || counter3 > 12f))
            {
                hasPressed3 = true;
                counter3 = 0;

                if (changeVolume)
                {
                    buttonNumber++;
                    if (buttonNumber > 11) buttonNumber = 11;
                }
            }

            if (Input.GetAxis("LeftJoystickHorizontal") < 0.8
                    && Input.GetAxis("ArrowsHorizontal") < 0.8)
            {
                hasPressed3 = false;
            }

            if (hasPressed3)
            {
                counter3++;
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (changeVolume)
                {
                    buttonNumber--;
                    if (buttonNumber < 3) buttonNumber = 3;
                }
            }

            if ((Input.GetAxis("LeftJoystickHorizontal") <= -0.8
                || Input.GetAxis("ArrowsHorizontal") <= -0.8) && (!hasPressed4 || counter4 > 12f))
            {
                hasPressed4 = true;
                counter4 = 0;

                if (changeVolume)
                {
                    buttonNumber--;
                    if (buttonNumber < 3) buttonNumber = 3;
                }
            }

            if (Input.GetAxis("LeftJoystickHorizontal") > -0.8
                    && Input.GetAxis("ArrowsHorizontal") > -0.8)
            {
                hasPressed4 = false;
            }

            if (hasPressed4)
            {
                counter4++;
            }
        }

        if (Input.anyKeyDown)
        {
            Debug.Log("PlayerPrefs Volume: " + PlayerPrefs.GetInt("Volume"));
            Debug.Log("Volume variable: " + volumeNumber);
            Debug.Log("Button number: " + buttonNumber);
        }
    }

    private void ResetData()
    {
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        PlayerPrefs.SetInt("FirstTrigger", 0);
                    PlayerPrefs.SetInt("FirstBlend", 0);
        PlayerPrefs.SetInt("Volume", 11);
        volumeNumber = PlayerPrefs.GetInt("Volume");
    }
}

