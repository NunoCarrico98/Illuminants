using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public Transform playButton;
    public Transform levelsButton;
    public Transform optionsButton;
    public Transform quitButton;

    private bool firstTrigger = false;
    private bool allow = true;
    private int buttonNumber = 0;

    // Use this for initialization
    void Start()
    {
        buttonNumber = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        ControllButtons();
    }

    private void ControllButtons()
    {
        if (!playButton.GetComponent<PlayButton>().playWasPressed && !quitButton.GetComponent<QuitButton>().quit)
        {
            switch (buttonNumber)
            {
                //PLAY
                case 0:
                    playButton.GetComponent<PlayButton>().goUp = true;

                    levelsButton.GetComponent<MenuMouseScript>().goUp = false;
                    optionsButton.GetComponent<MenuMouseScript>().goUp = false;
                    quitButton.GetComponent<QuitButton>().goUp = false;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        playButton.GetComponent<PlayButton>().playWasPressed = true;
                    }
                    break;

                //LEVELS
                case 1:
                    levelsButton.GetComponent<MenuMouseScript>().goUp = true;

                    playButton.GetComponent<PlayButton>().goUp = false;
                    optionsButton.GetComponent<MenuMouseScript>().goUp = false;
                    quitButton.GetComponent<QuitButton>().goUp = false;

                    break;

                //OPTIONS
                case 2:
                    optionsButton.GetComponent<MenuMouseScript>().goUp = true;

                    playButton.GetComponent<PlayButton>().goUp = false;
                    levelsButton.GetComponent<MenuMouseScript>().goUp = false;
                    quitButton.GetComponent<QuitButton>().goUp = false;

                    break;

                //QUIT
                case 3:
                    quitButton.GetComponent<QuitButton>().goUp = true;

                    playButton.GetComponent<PlayButton>().goUp = false;
                    levelsButton.GetComponent<MenuMouseScript>().goUp = false;
                    optionsButton.GetComponent<MenuMouseScript>().goUp = false;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        quitButton.GetComponent<QuitButton>().quit = true;
                    }
                    break;
            }
        }
    }

    private void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {

            if (buttonNumber < 4)
            {
                buttonNumber++;
            }

            if (buttonNumber == 4)
            {
                buttonNumber = 0;
            }


        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {

            if (buttonNumber > -1)
            {
                buttonNumber--;
            }
            if (buttonNumber == -1)
            {
                buttonNumber = 3;
            }

        }
    }
}
