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
        //GetComponent<Animator>().SetFloat("AnimationTime", 0);
        GetComponent<Animator>().SetBool("Play", false);

        timerToChangeLevel = GameObject.FindGameObjectWithTag("Characters").GetComponent<NextLevel>().timerToChangeLevel;
        Debug.Log("Level changing time: " + timerToChangeLevel);

        if (timerToChangeLevel >= 6) animationTime = 4;
        if (timerToChangeLevel >= 5 && timerToChangeLevel < 6) animationTime = 4;
        if (timerToChangeLevel >= 4 && timerToChangeLevel < 5) animationTime = 3;
        if (timerToChangeLevel >= 3 && timerToChangeLevel < 4) animationTime = 2;
        if (timerToChangeLevel < 3) animationTime = 1;

        Debug.Log("Animation Time: " + animationTime);

        timeToStartAnim = 0.6f;
        //timeCounter = animationTime;

        sceneName = GameObject.FindGameObjectWithTag("Characters").GetComponent<NextLevel>().loadLevel;
        imageComponent = GetComponent<Image>();
        imageComponent.enabled = false;



        //Get Level Number
        LevelNumber();

        ChangeNumberSprite();
    }

    private void Update()
    {
        startFinalAnimations = GameObject.FindGameObjectWithTag("Cube").GetComponent<CubeController>().reset;

        if (startFinalAnimations)
        {
            StartAnimation();
        }
    }

    private void LevelNumber()
    {
        fiveFirstLetters = sceneName.Substring(0, 5);

        if (fiveFirstLetters == "Level")
        {
            levelNumber = Convert.ToInt32(sceneName.Substring(5));
        }
        else
        {
            levelNumber = 0;
        }
    }

    private void ChangeNumberSprite()
    {
        if (levelNumber != 0 && levelNumber < 82)
        {
            //Level number what the number of the next level
            imageComponent.sprite = sprites[levelNumber - 1];
            imageComponent.enabled = true;
        }
    }

    private void StartAnimation()
    {
        timeToStartAnim -= Time.deltaTime;
        if (timeToStartAnim < 0 && timeToStartAnim > -animationTime)
        {
            GetComponent<Animator>().SetBool("Play", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Play", false);
        }


    }


}
