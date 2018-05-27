using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByeTitle : MonoBehaviour {

    public Light[] lights = new Light[9];
    public float fadeOutTime = 2;

    private PlayButton playButton;
    private SpriteRenderer titleSprite;
    private Color color;

	// Use this for initialization
	void Start () {
        playButton = FindObjectOfType<PlayButton>();
        titleSprite = transform.GetComponent<SpriteRenderer>();
        color = titleSprite.color;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(playButton.play == true)
        {
            FadeOutTitle();
            FadeOutLights();
        }
	}

    private void FadeOutTitle()
    {
        titleSprite.color = color;
        color.a -= fadeOutTime/100;
    }

    private void FadeOutLights()
    {
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].intensity -= fadeOutTime / 50;
        }
    }
}
