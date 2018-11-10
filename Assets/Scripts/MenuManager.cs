using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool toCreditsFromMenu;

    public Transform playButton;
    public Transform optionsButton;
    public Transform creditsButton;
    public Transform quitButton;
    public bool resetLevels = false;

    private int firstTrigger = 0;
    private bool allow = true;
    private int buttonNumber = 0;

    // Use this for initialization
    void Start()
    {
        buttonNumber = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Reset saved levels
        if (resetLevels == true)
        {
            PlayerPrefs.SetInt("UnlockedLevels", 0);
            PlayerPrefs.SetInt("FirstTrigger", 0);
            PlayerPrefs.SetInt("FirstBlend", 0);
        }

        //Load unlocked levels stored in player's computer data
        NextLevel.unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels");
        //Use a variable to check if it's the first time the player hits "Play"
        firstTrigger = PlayerPrefs.GetInt("FirstTrigger");

        if (NextLevel.unlockedLevels == 0) NextLevel.unlockedLevels = 1;
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

                    creditsButton.GetComponent<MenuMouseScript>().goUp = false;
                    optionsButton.GetComponent<MenuMouseScript>().goUp = false;
                    quitButton.GetComponent<QuitButton>().goUp = false;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        if (firstTrigger == 0)
                        {
                            playButton.GetComponent<PlayButton>().playWasPressed = true;
                            PlayerPrefs.SetInt("FirstTrigger", 1);
                        }
                        else
                        {
                            SceneManager.LoadScene("LevelSelector");
                        }
                    }
                    break;

                //OPTIONS
                case 1:
                    optionsButton.GetComponent<MenuMouseScript>().goUp = true;

                    playButton.GetComponent<PlayButton>().goUp = false;
                    creditsButton.GetComponent<MenuMouseScript>().goUp = false;
                    quitButton.GetComponent<QuitButton>().goUp = false;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene("OptionsMenu");
                    }
                        break;

                //CREDITS
                case 2:
                    creditsButton.GetComponent<MenuMouseScript>().goUp = true;

                    playButton.GetComponent<PlayButton>().goUp = false;
                    optionsButton.GetComponent<MenuMouseScript>().goUp = false;
                    quitButton.GetComponent<QuitButton>().goUp = false;

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        toCreditsFromMenu = true;
                        SceneManager.LoadScene("CreditsScene");
                    }
                    break;

                //QUIT
                case 3:
                    quitButton.GetComponent<QuitButton>().goUp = true;

                    playButton.GetComponent<PlayButton>().goUp = false;
                    creditsButton.GetComponent<MenuMouseScript>().goUp = false;
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
