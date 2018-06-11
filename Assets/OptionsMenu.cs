using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    //Volume: 1
    //Reset Game: 2
    //Mute: 3
    //Volume Cubes: 4 - 12  (Volume: 12.5  Distance: 3.56f)


    public Transform volumeButton;
    public Transform[] volumeCubes;
    public Transform resetButton;
    public Transform muteButton;
    public Transform popUp;
    public float cubesRiseSpeed = 200f;
    public float fadeInTime = 300f;
    public float fadeOutTime = 300f;


    private SpriteRenderer[] sprites;
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




    // Use this for initialization
    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main.transform;
        //sprites = new SpriteRenderer[cubes.Length, 6];
        //spritesColor = new Color[cubes.Length, 6];
        sprites = new SpriteRenderer[81];
        spritesColor = new Color[81];
        //newToppings = GameObject.Find("NewToppings").transform;
        volumeNumber = PlayerPrefs.GetInt("Volume", 8);
        Debug.Log(PlayerPrefs.GetInt("Volume"));
        Debug.Log(volumeNumber);
        newPos = new float[8];

        initPos = resetButton.transform.position.y;

        for (int i = 0; i < volumeCubes.Length; i++)
        {
            //if it's any cube between the 2nd and the 8th
            if (i > 0)
            {
                newPos[i] = newPos[i - 1] + 3.56f;
            }
            //if it's the first cube
            else
            {
                newPos[i] = initPos + 3.56f;
            }
        }

        buttonNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        RiseVolume();
        CheckInput();
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

        if (volumeNumber >= 4)
        {
            volumeCubes[0].position = Vector3.MoveTowards(volumeCubes[0].position,
                new Vector3(volumeCubes[0].position.x, newPos[0], volumeCubes[0].position.z), cubesRiseSpeed * Time.deltaTime);
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

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                changeVolume = false;
                buttonNumber = 1;
                counter++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && counter == 0)
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (timeStopped)
            {
                popUp.GetComponent<SpriteRenderer>().enabled = false;
                Time.timeScale = 1f;
                timeStopped = false;
            }

            if(!timeStopped && !changeVolume)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        if (!timeStopped)
        {

            if (!changeVolume)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    buttonNumber++;
                    if (buttonNumber > 2) buttonNumber = 1;

                }

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    buttonNumber--;
                    if (buttonNumber < 1) buttonNumber = 2;
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

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (changeVolume)
                {
                    buttonNumber--;
                    if (buttonNumber < 3) buttonNumber = 3;
                }
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
        PlayerPrefs.SetInt("Volume", 8);
        volumeNumber = PlayerPrefs.GetInt("Volume");
    }
}

