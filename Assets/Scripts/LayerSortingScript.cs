using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSortingScript : MonoBehaviour
{

    public int floorSortingOrderUp = 4;
    public int wallsSortingOrderUp = 2;
    public int floorSortingOrderDown = 0;
    public int wallsSortingOrderDown = -1;
    private SpriteRenderer sprite;      //sprites from the walls and top of the cube
    private SpriteRenderer sprite_verify;       //sprite from the bottom of the cube
    private GameObject cube;        //child
    private Transform block_verify;     //child of child
    private Transform block;        //child of child
    private float yPos;     //y coordinates of the bottom of the cube
    private GameObject characters;
    private bool activeFinalAnims;
    private bool cubesInPlace = false;

    // Use this for initialization
    void Start()
    {
        characters = GameObject.FindGameObjectWithTag("Characters");
    }

    private void Update()
    {
        ChangeLayers();

        activeFinalAnims = characters.GetComponent<NextLevel>().activeFinalAnims;
        if (activeFinalAnims == true) ResetLayers();
    }

    public void ChangeLayers()
    {
        cubesInPlace = CubeController.cubesInPlace;
        if (cubesInPlace == false)
        {
            for (int j = 1; j < 10; j++)
            {
                cube = transform.Find("MyCube (" + j + ")").gameObject;
                block_verify = cube.transform.Find("New Sprite (1)");
                yPos = block_verify.transform.position.y;
                sprite_verify = block_verify.GetComponent<SpriteRenderer>();

                for (int i = 2; i < 6; i++)
                {
                    if (yPos > 31 && yPos < 32)
                    {
                        block = cube.transform.Find("New Sprite (" + i + ")");
                        sprite = block.GetComponent<SpriteRenderer>();
                        sprite_verify.sortingOrder = floorSortingOrderDown;
                        sprite.sortingOrder = wallsSortingOrderDown;
                    }
                    if (yPos >= 32 && yPos <= 63)
                    {
                        sprite_verify.sortingOrder = floorSortingOrderUp;
                    }
                    if(yPos == 63)
                    {
                        block = cube.transform.Find("New Sprite (" + i + ")");
                        sprite = block.GetComponent<SpriteRenderer>();
                        sprite.sortingOrder = wallsSortingOrderUp;
                    }
                }
            }
        }
    }

    void ResetLayers()
    {
        for (int j = 1; j < 10; j++)
        {
            cube = transform.Find("MyCube (" + j + ")").gameObject;

            for (int i = 2; i < 6; i++)
            {
                block = cube.transform.Find("New Sprite (" + i + ")");
                sprite = block.GetComponent<SpriteRenderer>();
                sprite.sortingOrder = wallsSortingOrderDown;
                sprite_verify.sortingOrder = 0;
            }
        }
    }
}
