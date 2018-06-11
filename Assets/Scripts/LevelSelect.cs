using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public Transform[] cubes;
    public float camMoveSpeed = 200f;
    public float camRotationSpeed = 200f;
    public float minCamMoveSpeed = 30f;
    public float minCamRotationSpeed = 2f;
    public float buttonsRiseSpeed = 100f;
    public float cubesReplacementSpeed = 300f;
    public float fadeInTime = 300f;
    public float fadeOutTime = 300f;
    public float timerToStartLevel = 2f;

    //Testing variables
    public int unlockedLevels = 1;
    public bool unlockLevelsForTesting = false;

    public Sprite newSpritesForTop;
    public Sprite newSpritesForLight;

    private Transform newToppings;
    private Transform lockedLevels;
    //private SpriteRenderer[,] sprites;
    //private Color[,] spritesColor;
    private SpriteRenderer[] sprites;
    private Color[] spritesColor;
    private bool firstTrigger = false;
    private bool allow = true;
    private bool play = false;
    private bool changeSprites = false;
    private bool alphaUp = false;
    private int buttonNumber = 0;
    private float angle = 90f;
    private float acceleration = 100;


    private Transform cam;


    // Use this for initialization
    void Start()
    {
        buttonNumber = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main.transform;
        //sprites = new SpriteRenderer[cubes.Length, 6];
        //spritesColor = new Color[cubes.Length, 6];
        sprites = new SpriteRenderer[81];
        spritesColor = new Color[81];
        newToppings = GameObject.Find("NewToppings").transform;
        lockedLevels = GameObject.Find("LockedLevels").transform;


        if (unlockLevelsForTesting) NextLevel.unlockedLevels = unlockedLevels;

        for (int i = 0; i < NextLevel.unlockedLevels; i++)
        {
            //Destroy(lockedLevels.GetChild(i));
            lockedLevels.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RiseCube();

        if (!play)
        {
            CheckInput();
        }
    }

    private void FixedUpdate()
    {
        if (play)
        {
            CameraRotate();
            OverlaySprites();
            /*if(!changeSprites) AlphaDown();
            if (changeSprites) ChangeSprites();
            if (alphaUp) AlphaUp();*/

        }
    }

    private void OverlaySprites()
    {
        for (int i = 0; i < 81; i++)
        {
            sprites[i] = newToppings.GetChild(i).GetComponent<SpriteRenderer>();
            spritesColor[i] = sprites[i].color;
            spritesColor[i].a += fadeInTime / 1000;
            sprites[i].color = spritesColor[i];
        }

    }

    /*private void AlphaDown()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (cubes[i].GetChild(j) != null)
                    sprites[i, j] = cubes[i].GetChild(j).GetComponent<SpriteRenderer>();

                spritesColor[i, j] = sprites[i, j].color;
                spritesColor[i, j].a -= fadeOutTime / 1000;
                sprites[i, j].color = spritesColor[i, j];
                if (spritesColor[i, j].a <= 0)
                {
                    changeSprites = true;
                }
            }
        }
    }

    private void ChangeSprites()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = newSpritesForTop;
            cubes[i].GetChild(5).GetComponent<SpriteRenderer>().sprite = newSpritesForLight;
            if (i == cubes.Length - 1) alphaUp = true;
        }
    }

    private void AlphaUp()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (cubes[i].GetChild(j) != null)
                    sprites[i, j] = cubes[i].GetChild(j).GetComponent<SpriteRenderer>();

                spritesColor[i, j] = sprites[i, j].color;
                spritesColor[i, j].a += fadeInTime / 1000;
                sprites[i, j].color = spritesColor[i, j];
            }
        }
    }*/

    private void RiseCube()
    {

        if (!play)
        {
            cubes[buttonNumber - 1].GetComponent<MenuMouseScript>().goUp = true;
        }
        else
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                cubes[i].GetComponent<MenuMouseScript>().goUp = false;
            }
        }
    }

    private void CameraRotate()
    {
        //Move the camera to the in-game playing position
        cam.position = Vector3.MoveTowards(cam.position, new Vector3(0, 677, -508), camMoveSpeed * Time.deltaTime);
        //cam.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * camRotationSpeed);

        //Rotate the camera to the in-game playing angle
        if (angle > 60)
        {
            angle -= camRotationSpeed / 100;
            if (angle < 60) angle = 60;
            cam.eulerAngles = new Vector3(angle, cam.eulerAngles.y, cam.eulerAngles.z);
        }

        //Create acceleration effect by changing the speeds
        if (camMoveSpeed > minCamMoveSpeed)
        {
            camMoveSpeed -= camMoveSpeed / acceleration;
        }

        if (camRotationSpeed > minCamRotationSpeed)
        {
            camRotationSpeed -= camRotationSpeed / acceleration;
        }
        if (acceleration > 50) acceleration -= 1f;
        if (acceleration > 20 && acceleration <= 50) acceleration -= 0.5f;

        if ((cam.position.x == 0 && cam.position.y == 677 && cam.position.z == -508) && angle == 60)
        {
            timerToStartLevel -= Time.deltaTime;
            if (timerToStartLevel <= 0) SceneManager.LoadScene("Level" + buttonNumber);
        }
    }

    private void CheckInput()
    {

        if (Input.anyKey)
        {
            Debug.Log(buttonNumber);
        }

        if (Input.GetKey(KeyCode.Return))
        {
            play = true;
        }


        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {

            if (buttonNumber < NextLevel.unlockedLevels + 1)
            {
                buttonNumber = buttonNumber + 9;
            }
            if (buttonNumber > NextLevel.unlockedLevels)
            {
                if (NextLevel.unlockedLevels == 81)
                {
                    buttonNumber = 0 + (buttonNumber - NextLevel.unlockedLevels);
                }
                else
                {
                    buttonNumber = buttonNumber - 9;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (buttonNumber > 0)
            {
                buttonNumber = buttonNumber - 9;

            }
            if (buttonNumber < 1)
            {
                if (NextLevel.unlockedLevels == 81)
                {
                    buttonNumber = NextLevel.unlockedLevels - (0 - buttonNumber);
                }
                else
                {
                    buttonNumber = buttonNumber + 9;
                }
            }

        }

        //Left and Right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

            if (buttonNumber < NextLevel.unlockedLevels + 1)
            {
                buttonNumber++;
            }

            if (buttonNumber == NextLevel.unlockedLevels + 1)
            {
                if(NextLevel.unlockedLevels == 81)
                {
                    buttonNumber = 1;
                }
                else
                {
                    buttonNumber--;
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {

            if (buttonNumber > 0)
            {
                buttonNumber--;

            }
            if (buttonNumber == 0)
            {
                if (NextLevel.unlockedLevels == 81)
                {
                    buttonNumber = NextLevel.unlockedLevels;
                }
                else
                {
                    buttonNumber++;
                }
            }

        }





        /*if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {

            if (buttonNumber < 82)
            {
                buttonNumber = buttonNumber + 9;

                if (buttonNumber < 82)
                {
                    if (cubes[buttonNumber - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "LevelSelector_324")
                        buttonNumber = buttonNumber - 9;
                }
            }

            if (buttonNumber > 81)
            {
                buttonNumber = 0 + (buttonNumber - 81);

                if (cubes[buttonNumber - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "LevelSelector_324")
                    buttonNumber = buttonNumber + 81 - 9;
            }


        }


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (buttonNumber > 0)
            {
                buttonNumber = buttonNumber - 9;

                if (buttonNumber > 0)
                {
                    if (cubes[buttonNumber - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "LevelSelector_324")
                        buttonNumber = buttonNumber + 9;
                }
            }
            if (buttonNumber < 1)
            {
                buttonNumber = 81 - (0 - buttonNumber);

                if (cubes[buttonNumber - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "LevelSelector_324")
                    buttonNumber = (81 - 9 - buttonNumber) * -1;
            }
        }

        //Left and Right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

            if (buttonNumber < 82)
            {
                buttonNumber++;

                if (buttonNumber < 82)
                {
                    if (cubes[buttonNumber - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "LevelSelector_324")
                        buttonNumber--;
                }
            }

            if (buttonNumber == 82)
            {
                buttonNumber = 1;
            }


        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {

            if (buttonNumber > 0)
            {
                buttonNumber--;

                if (buttonNumber > 0)
                {
                    if (cubes[buttonNumber - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "LevelSelector_324")
                        buttonNumber++;
                }
            }
            if (buttonNumber == 0)
            {
                buttonNumber = 81;

                if (cubes[buttonNumber - 1].GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "LevelSelector_324")
                    buttonNumber = 1;
            }

        }*/
    }
}
