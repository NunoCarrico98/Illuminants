using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSortingScript : MonoBehaviour
{

    public int sortingOrder_Down = 0;
    public int sortingOrder_Up = 4;
    private SpriteRenderer sprite;
    private SpriteRenderer sprite_verify;
    private GameObject cube;
    private Transform block_verify;
    private Transform block;
    private float yPos;

    // Use this for initialization
    void Start()
    {
        cube = GetComponent<GameObject>();

        for (int i = 0; i < 6; i++)
        {
            block_verify = transform.Find("New Sprite (1)");
            yPos = block_verify.transform.position.y;
            sprite_verify = block_verify.GetComponent<SpriteRenderer>();
            if (yPos == 0)
            {
                block = transform.Find("New Sprite (" + i + ")");
                sprite = block.GetComponent<SpriteRenderer>();
                sprite_verify.sortingOrder = sortingOrder_Down;
                sprite.sortingOrder = sortingOrder_Down;

            }
        }
        for (int i = 0; i < 6; i++)
        {
            block_verify = transform.Find("New Sprite (1)");
            yPos = block_verify.transform.position.y;
            sprite_verify = block_verify.GetComponent<SpriteRenderer>();
            if (yPos == 32)
            {
                block = transform.Find("New Sprite (" + i + ")");
                sprite = block.GetComponent<SpriteRenderer>();
                sprite_verify.sortingOrder = sortingOrder_Up;
                sprite.sortingOrder = sortingOrder_Up;
            }
        }
    }
}
