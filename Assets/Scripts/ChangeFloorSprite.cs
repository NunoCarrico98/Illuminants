using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorSprite : MonoBehaviour {

    public GameObject mySprite;
    public SpriteRenderer spriteR;
    public Sprite sprite0, sprite1, sprite2, sprite3, sprite4;

    // Use this for initialization
    void Start () {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        ChangeSprite();
    }

    private void Update()
    {
        ChangeSprite();
    }

    void ChangeSprite()
    {
        if (mySprite.transform.position.y == 32)        //Minumum height for the top of the cubes
        {
            spriteR.sprite = sprite0;
        }

        if (mySprite.transform.position.y > 32 && mySprite.transform.position.y <= 38)
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

        if (mySprite.transform.position.y > 58 && mySprite.transform.position.y <= 64)     //Max height for the top of the cubes
        {
            spriteR.sprite = sprite4;
        }
    }
}
