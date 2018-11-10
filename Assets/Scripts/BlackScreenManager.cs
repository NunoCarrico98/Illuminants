using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenManager : MonoBehaviour
{

    private int currentLevel;
    private int maxLevelsAvailable;


    // Use this for initialization
    void Start()
    {
        maxLevelsAvailable = GetComponentInParent<GameManagement>().maxNumberOfLevels;
        currentLevel = GetComponentInParent<GameManagement>().currentLevel;

        Color sColor = GetComponent<SpriteRenderer>().color;
        sColor.a = 0;
        GetComponent<SpriteRenderer>().color = sColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnimation()
    {
        GetComponent<Animator>().SetTrigger("Fade");
        Debug.Log("Worked2");
    }
}
