using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorSprite : MonoBehaviour {

    public GameObject mySprite;
    public SpriteRenderer spriteR;
    public Sprite sprite0, sprite1, sprite2, sprite3, sprite4;
    private Color spriteColor;
    private float initPos;
    private float counter = 0;
    private float alpha = 0;


    // Use this for initialization
    void Start () {
        spriteR = gameObject.GetComponent<SpriteRenderer>();

        spriteColor = spriteR.color;
        spriteColor.a = 0;
        spriteR.color = spriteColor;


        initPos = transform.position.y;
    }

    private void Update()
    {
        //ChangeSprite();
        ChangeLightAlpha();
    }

    void ChangeSprite()
    {
        if (mySprite.transform.position.y == 31)        //Minumum height for the top of the cubes
        {
            spriteR.sprite = sprite0;
        }

        if (mySprite.transform.position.y > 31 && mySprite.transform.position.y <= 38)
        {
            spriteR.sprite = sprite1;
        }

        if (mySprite.transform.position.y > 38 && mySprite.transform.position.y <= 48) 
        {
            spriteR.sprite = sprite2;
        }

        if (mySprite.transform.position.y > 48 && mySprite.transform.position.y <= 58)
        {
            spriteR.sprite = sprite3;
        }

        if (mySprite.transform.position.y > 58 && mySprite.transform.position.y <= 63)     //Max height for the top of the cubes
        {
            spriteR.sprite = sprite4;
        }
    }

        void ChangeLightAlpha()
    {
        //= new Color(1, 1, 1, alpha);
        //spriteColor.a = transform.position.y * (1/(transform.position.y - 31));
        spriteColor.a = (transform.position.y - 31) * (0.03125f);
        //counter += (255/32);
        Debug.Log("Position is: " + (transform.position.y - 31));
        spriteR.color = spriteColor;

    }
}
