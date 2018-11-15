using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBlendPopUp : MonoBehaviour
{

    public float popUpTime = 0.4f;
    public float fadeInTime = 1f;
    public float fadeOutTime = 0.5f;

    private float timer = 0f;
    private GameObject spaceButton;
    private SpriteRenderer sprite;
    private Color sColor;
    private bool goAway = false;
    private int counter;

    // Use this for initialization
    void Start()
    {
        spaceButton = GameObject.FindGameObjectWithTag("SpaceButton");
        if (spaceButton != null)
        {
            sprite = spaceButton.GetComponent<SpriteRenderer>();
            sColor = sprite.color;
            sColor.a = 0;
            sprite.color = sColor;

            if (PlayerPrefs.GetInt("FirstBlend") == 2)
            {
                sprite.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("FirstBlend") == 1)
        {
            PopUp();
        }
        //Press Enter to check if FirstBlend's value
        if (Input.GetKeyDown(KeyCode.Return)) Debug.Log(PlayerPrefs.GetInt("FirstBlend"));
    }

    private void PopUp()
    {

        if (timer < popUpTime) timer += Time.deltaTime;
        if (timer >= popUpTime)
        {
            //Time.timeScale = 1f;
            if (spaceButton != null)
            {
                sprite.enabled = true;
                if (!goAway) FadeEffect();

                if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonDown("AButton") 
                    || FindObjectOfType<NextLevel>().activeFinalAnims)
                {
                    goAway = true;
                }
                if (goAway) StopPopUp();
            }
        }
    }

    private void FadeEffect()
    {
        if (sColor.a >= 0 && sColor.a <= 1 && counter == 0)
        {
            sColor.a += fadeInTime / 100;
            sprite.color = sColor;
            if (sColor.a >= 0.9f)
            {
                sColor.a = 1;
                counter++;
            }
        }

        if (sColor.a >= 0.4 && sColor.a <= 1 && counter == 1)
        {
            sColor.a += fadeInTime / 100;
            sprite.color = sColor;
        }
    }

    private void StopPopUp()
    {

        if (sColor.a >= -1)
        {
            sColor.a -= fadeOutTime / 100;
            sprite.color = sColor;
            if (sColor.a <= 0)
            {
                //Time.timeScale = 1f;
                sprite.enabled = false;
                PlayerPrefs.SetInt("FirstBlend", 2);
            }
        }

    }
}
