using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorSprite : MonoBehaviour {

    public SpriteRenderer spriteR;
    private Color spriteColor;



    // Use this for initialization
    void Start () {
        spriteR = transform.GetComponent<SpriteRenderer>();

        spriteColor = spriteR.color;
        spriteColor.a = 0;
        spriteR.color = spriteColor;
    }

    private void Update()
    {

        ChangeLightAlpha();
    }

        void ChangeLightAlpha()
    {
        spriteColor.a = (transform.position.y - 31) * (0.03125f);
        spriteR.color = spriteColor;
    }
}
