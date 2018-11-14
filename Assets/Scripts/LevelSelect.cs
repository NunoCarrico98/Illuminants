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
    private bool hasPressed = false;
    private bool hasPressed2 = false;
    private bool hasPressed3 = false;
    private bool hasPressed4 = false;
    private float counter = 0;
    private float counter2 = 0;
    private float counter3 = 0;
    private float counter4 = 0;


    private Transform cam;


    // Use this for initialization
    void Start()
    {
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
        buttonNumber = NextLevel.unlockedLevels;

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

        if (Input.GetKey(KeyCode.Return) || Input.GetButton("AButton"))
        {
            play = true;
        }

        if (Input.GetKey(KeyCode.Escape) || Input.GetButton("BButton"))
        {
            SceneManager.LoadScene("Menu");
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

        if ((Input.GetAxis("LeftJoystickVertical") <= -0.8
            || Input.GetAxis("ArrowsVertical") <= -0.8) && (!hasPressed || counter > 12f))
        {
            hasPressed = true;
            counter = 0;

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

        if (Input.GetAxis("LeftJoystickVertical") > -0.8
                && Input.GetAxis("ArrowsVertical") > -0.8)
        {
            hasPressed = false;
        }

        if (hasPressed)
        {
            counter++;
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

        if ((Input.GetAxis("LeftJoystickVertical") >= 0.8
            || Input.GetAxis("ArrowsVertical") >= 0.8) && (!hasPressed2 || counter2 > 12f))
        {
            hasPressed2 = true;
            counter2 = 0;

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

        if (Input.GetAxis("LeftJoystickVertical") < 0.8
                && Input.GetAxis("ArrowsVertical") < 0.8)
        {
            hasPressed2 = false;
        }

        if (hasPressed2)
        {
            counter2++;
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
                if (NextLevel.unlockedLevels == 81)
                {
                    buttonNumber = 1;
                }
                else
                {
                    buttonNumber--;
                }
            }
        }

        if ((Input.GetAxis("LeftJoystickHorizontal") >= 0.8
            || Input.GetAxis("ArrowsHorizontal") >= 0.8) && (!hasPressed3 || counter3 > 12f))
        {
            hasPressed3 = true;
            counter3 = 0;

            if (buttonNumber < NextLevel.unlockedLevels + 1)
            {
                buttonNumber++;
            }

            if (buttonNumber == NextLevel.unlockedLevels + 1)
            {
                if (NextLevel.unlockedLevels == 81)
                {
                    buttonNumber = 1;
                }
                else
                {
                    buttonNumber--;
                }
            }
        }

        if (Input.GetAxis("LeftJoystickHorizontal") < 0.8
                && Input.GetAxis("ArrowsHorizontal") < 0.8)
        {
            hasPressed3 = false;
        }

        if (hasPressed3)
        {
            counter3++;
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

        if ((Input.GetAxis("LeftJoystickHorizontal") <= -0.8
            || Input.GetAxis("ArrowsHorizontal") <= -0.8) && (!hasPressed4 || counter4 > 12f))
        {
            hasPressed4 = true;
            counter4 = 0;

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

        if (Input.GetAxis("LeftJoystickHorizontal") > -0.8
                && Input.GetAxis("ArrowsHorizontal") > -0.8)
        {
            hasPressed4 = false;
        }

        if (hasPressed4)
        {
            counter4++;
        }
    }
}
