using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLevelNumber : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;

    private Image imageComponent;
    private string sceneName;
    private string fiveFirstLetters;
    private int levelNumber;
    private float timerToChangeLevel;
    private float animationTime;
    private float timeToStartAnim = 1;
    private bool startFinalAnimations;


    // Use this for initialization
    void Start()
    {
        timerToChangeLevel = GameObject.FindGameObjectWithTag("Characters").GetComponent<NextLevel>().timerToChangeLevel;

        if (timerToChangeLevel >= 6) animationTime = 4;
        if (timerToChangeLevel >= 5 && timerToChangeLevel < 6) animationTime = 4;
        if (timerToChangeLevel >= 4 && timerToChangeLevel < 5) animationTime = 3;
        if (timerToChangeLevel >= 3 && timerToChangeLevel < 4) animationTime = 2;
        if (timerToChangeLevel < 3) animationTime = 1;

        timeToStartAnim = 1f;
        //timeCounter = animationTime;
        
        //Get Level Number
        LevelNumber();
    }

    private void Update()
    {
        startFinalAnimations = GameObject.FindGameObjectWithTag("Cube").GetComponent<CubeController>().reset;

        if (startFinalAnimations)
        {
            StartAnimation();
        }
    }

    public void LevelNumber()
    {
        GetComponent<Animator>().SetBool("Play", false);

        sceneName = GameObject.FindGameObjectWithTag("Characters").GetComponent<NextLevel>().loadLevel;

        imageComponent = GetComponent<Image>();
        imageComponent.enabled = false;

        fiveFirstLetters = sceneName.Substring(0, 5);

        if (fiveFirstLetters == "Level")
        {
            levelNumber = Convert.ToInt32(sceneName.Substring(5));
        }
        else
        {
            levelNumber = 0;
        }

        ChangeNumberSprite();
    }

    public void ChangeNumberSprite()
    {
        if (levelNumber != 0 && levelNumber < 82)
        {
            //Level number what the number of the next level
            imageComponent.sprite = sprites[levelNumber - 2];
            imageComponent.enabled = true;
        }
    }

    public void StartAnimation()
    {
        timeToStartAnim -= Time.deltaTime;
        if (timeToStartAnim <= 1 && timeToStartAnim >= -1)
        {
            GetComponent<Animator>().SetBool("Play", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Play", false);
        }


    }


}
