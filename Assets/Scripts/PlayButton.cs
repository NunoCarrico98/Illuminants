using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Transform playButt;
    public Transform levelButt;
    public Transform optionsButt;
    public Transform quitButt;

    public Transform[] cubesForReplacement = new Transform[16];

    public Sprite newSpritesForTop;
    public Sprite newSpritesForSides;
    public Sprite newSpritesForLight;

    private SpriteRenderer[] playButtSprites = new SpriteRenderer[6];
    private SpriteRenderer[] levelButtSprites = new SpriteRenderer[6];
    private SpriteRenderer[] optionsButtSprites = new SpriteRenderer[6];
    private SpriteRenderer[] quitButtSprites = new SpriteRenderer[6];
    private SpriteRenderer[,] cubesSprites = new SpriteRenderer[16, 6];

    public float cubesRiseSpeed = 200f;
    public float camMoveSpeed = 200f;
    public float camRotationSpeed = 200f;
    public float minCamMoveSpeed = 30f;
    public float minCamRotationSpeed = 2f;
    public float buttonsRiseSpeed = 100f;
    public float cubesReplacementSpeed = 300f;
    public float fadeInTime = 300f;
    public float fadeOutTime = 300f;
    public float timerToStartLevel = 2f;
    public bool goUp = false;

    private SpriteRenderer[] sprites;
    private Color[] spritesColor;
    private Transform newCubes;
    private Transform cam;
    private Vector3 lookPos;
    private Quaternion rotation;

    private float initPos;
    private float newPos;
    private float angle = 90f;

    public bool playWasPressed = false;
    public bool play = false;
    public bool noMorePlayingAround = false;
    private bool buttonGone = false;
    private bool spritesChanged = false;
    private bool cubesInPlace = false;

    private float acceleration = 100;
    private float acceleration2 = 100;
    private float acceleration3 = 100;
    private float initButtonsRiseSpeed;




    private void Start()
    {
        initPos = transform.position.y;
        newPos = initPos + 32;
        cam = Camera.main.transform;

        lookPos = cam.position - cam.position;
        lookPos.y = 0;
        rotation = Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(60, 0, 0); // this adds a -30 degrees X rotation

        initButtonsRiseSpeed = buttonsRiseSpeed;
        buttonsRiseSpeed = 0.5f;
        newCubes = GameObject.Find("NewCubes").transform;
        sprites = new SpriteRenderer[newCubes.childCount];
        spritesColor = new Color[newCubes.childCount];
    }

    /*public void OnMouseOver()
    {
        if (play == false) goUp = true;
    }
    private void OnMouseDown()
    {
        playWasPressed = true;
    }*/

    private void FixedUpdate()
    {
        if (playWasPressed == true /*&& cubesInPlace == false*/)
        {
            play = true;
            noMorePlayingAround = true;
        }
        else
        {
            play = false;
        }

        if (playWasPressed == true)
        {
            Cursor.visible = false;
            CameraRotate();
            EnableNewCubes();
            //ChangeButtonsForCubes();
        }
    }

    private void Update()
    {
        if (goUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, newPos, transform.position.z), cubesRiseSpeed * Time.deltaTime);
            if (transform.position.y == newPos) goUp = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, initPos, transform.position.z), cubesRiseSpeed * Time.deltaTime);
        }
    }

    private void EnableNewCubes()
    {
        for (int i = 0; i < newCubes.childCount; i++)
        {
            sprites[i] = newCubes.GetChild(i).GetComponent<SpriteRenderer>();
            spritesColor[i] = sprites[i].color;
            spritesColor[i].a += fadeInTime / 1000;
            sprites[i].color = spritesColor[i];
            if (i == newCubes.childCount - 1) cubesInPlace = true;
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
            if (cubesInPlace == true)
            {
                timerToStartLevel -= Time.deltaTime;
                if (timerToStartLevel <= 0.5)
                {
                    noMorePlayingAround = true;
                }
                if (timerToStartLevel <= 0) SceneManager.LoadScene("Level" + NextLevel.unlockedLevels);
            }
        }
    }

    private void ChangeButtonsForCubes()
    {
        //ButtonsRise();
        //if (buttonsUp == true) ChangeSprites();
        //if (spritesChanged == true) CubesDown();
        if (buttonGone == false)
        {
            ButtonsDisappear();
        }
        if (buttonGone == true && spritesChanged == false)
        {
            ChangeSprites();
        }
        if (spritesChanged == true) CubesAlphaUp();
    }

    private void ButtonsDisappear()
    {
        GetSprites();
        if (buttonGone == false)
        {
            for (int i = 0; i < playButtSprites.Length - 1; i++)
            {
                spritesColor[i] = playButtSprites[i].color;

                /*if (spritesColor[5].a <= 0.05)
                {
                    spritesColor[i].a = 0;
                }*/
                if (spritesColor[i].a > 0)
                {
                    spritesColor[i].a -= fadeOutTime / 100;
                }
                playButtSprites[i].color = spritesColor[i];
                levelButtSprites[i].color = spritesColor[i];
                optionsButtSprites[i].color = spritesColor[i];
                quitButtSprites[i].color = spritesColor[i];
                for (int j = 0; j < cubesForReplacement.Length; j++)
                {
                    cubesSprites[j, i].color = spritesColor[i];
                }
            }
        }
        if (quitButtSprites[4].color.a <= 0) buttonGone = true;
    }

    private void GetSprites()
    {
        for (int i = 1; i < 6; i++)
        {

            playButtSprites[i - 1] = playButt.Find("New Sprite (" + i + ")").GetComponent<SpriteRenderer>();
            levelButtSprites[i - 1] = levelButt.Find("New Sprite (" + i + ")").GetComponent<SpriteRenderer>();
            optionsButtSprites[i - 1] = optionsButt.Find("New Sprite (" + i + ")").GetComponent<SpriteRenderer>();
            quitButtSprites[i - 1] = quitButt.Find("New Sprite (" + i + ")").GetComponent<SpriteRenderer>();
            for (int j = 0; j < cubesForReplacement.Length; j++)
            {

                cubesSprites[j, i - 1] = cubesForReplacement[j].Find("New Sprite (" + i + ")").GetComponent<SpriteRenderer>();
            }
        }
        playButtSprites[5] = playButt.Find("light").GetComponent<SpriteRenderer>();
        levelButtSprites[5] = levelButt.Find("light").GetComponent<SpriteRenderer>();
        optionsButtSprites[5] = optionsButt.Find("light").GetComponent<SpriteRenderer>();
        quitButtSprites[5] = quitButt.Find("light").GetComponent<SpriteRenderer>();
        for (int i = 0; i < cubesForReplacement.Length; i++)
        {

            cubesSprites[i, 5] = cubesForReplacement[i].Find("light").GetComponent<SpriteRenderer>();
        }
    }

    private void CubesAlphaUp()
    {
        for (int i = 0; i < cubesSprites.GetLength(1); i++)
        {
            spritesColor[i] = cubesSprites[0, 0].color;
            spritesColor[i].a += fadeInTime / 100;

            if (i < cubesSprites.GetLength(1) - 1)
            {
                playButtSprites[i].color = spritesColor[i];
                levelButtSprites[i].color = spritesColor[i];
                optionsButtSprites[i].color = spritesColor[i];
                quitButtSprites[i].color = spritesColor[i];
            }

            for (int j = 0; j < cubesForReplacement.Length; j++)
            {
                if (i < cubesSprites.GetLength(1) - 1)
                {
                    cubesSprites[j, i].color = spritesColor[i];
                }
            }
        }
        if (cubesSprites[15, 4].color.a >= 1) cubesInPlace = true;
    }

    private void ButtonsRise()
    {
        if (buttonsRiseSpeed <= initButtonsRiseSpeed)
        {
            buttonsRiseSpeed += buttonsRiseSpeed / acceleration;
            if (acceleration > 5) acceleration2 -= 5 / 10;
        }

        if (buttonsRiseSpeed <= initButtonsRiseSpeed)
        {
            buttonsRiseSpeed = buttonsRiseSpeed * acceleration2;
            acceleration2 += 0.1f;
            if (buttonsRiseSpeed > initButtonsRiseSpeed) buttonsRiseSpeed = initButtonsRiseSpeed;
        }

        if (buttonGone == false)
        {
            //Rise the buttons
            playButt.position = Vector3.MoveTowards(playButt.position,
                new Vector3(playButt.position.x, 600, playButt.position.z), buttonsRiseSpeed * Time.deltaTime);
            levelButt.position = Vector3.MoveTowards(levelButt.position,
                new Vector3(levelButt.position.x, 600, levelButt.position.z), buttonsRiseSpeed * Time.deltaTime);
            optionsButt.position = Vector3.MoveTowards(optionsButt.position,
                new Vector3(optionsButt.position.x, 600, optionsButt.position.z), buttonsRiseSpeed * Time.deltaTime);
            quitButt.position = Vector3.MoveTowards(quitButt.position,
                new Vector3(quitButt.position.x, 600, quitButt.position.z), buttonsRiseSpeed * Time.deltaTime);

            //Rise the invisible cubes
            for (int i = 0; i < cubesForReplacement.Length; i++)
                cubesForReplacement[i].position = Vector3.MoveTowards(cubesForReplacement[i].position,
                    new Vector3(cubesForReplacement[i].position.x, 600, cubesForReplacement[i].position.z),
                    buttonsRiseSpeed * Time.deltaTime);

            if (playButt.position.y == 600 && cubesForReplacement[15].position.y == 600) buttonGone = true;
        }
    }

    private void ChangeSprites()
    {
        if (spritesChanged == false)
        {
            //Transform buttons into cubes again
            playButt.Find("New Sprite (1)").GetComponent<SpriteRenderer>().sprite = newSpritesForTop;
            playButt.Find("New Sprite (2)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            playButt.Find("New Sprite (3)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            playButt.Find("light").GetComponent<SpriteRenderer>().sprite = newSpritesForLight;
            playButt.GetComponent<BoxCollider>().size = new Vector3(32, 32, 32);

            //Transform buttons into cubes again
            levelButt.Find("New Sprite (1)").GetComponent<SpriteRenderer>().sprite = newSpritesForTop;
            levelButt.Find("New Sprite (2)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            levelButt.Find("New Sprite (3)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            levelButt.Find("light").GetComponent<SpriteRenderer>().sprite = newSpritesForLight;
            levelButt.GetComponent<BoxCollider>().size = new Vector3(32, 32, 32);

            optionsButt.Find("New Sprite (1)").GetComponent<SpriteRenderer>().sprite = newSpritesForTop;
            optionsButt.Find("New Sprite (2)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            optionsButt.Find("New Sprite (3)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            optionsButt.Find("light").GetComponent<SpriteRenderer>().sprite = newSpritesForLight;
            optionsButt.GetComponent<BoxCollider>().size = new Vector3(32, 32, 32);

            quitButt.Find("New Sprite (1)").GetComponent<SpriteRenderer>().sprite = newSpritesForTop;
            quitButt.Find("New Sprite (2)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            quitButt.Find("New Sprite (3)").GetComponent<SpriteRenderer>().sprite = newSpritesForSides;
            quitButt.Find("light").GetComponent<SpriteRenderer>().sprite = newSpritesForLight;
            quitButt.GetComponent<BoxCollider>().size = new Vector3(32, 32, 32);

            //Make other cubes visible again
            for (int i = 0; i < cubesForReplacement.Length; i++)
            {
                cubesForReplacement[i].Find("New Sprite (1)").GetComponent<SpriteRenderer>().enabled = true;
                cubesForReplacement[i].Find("New Sprite (2)").GetComponent<SpriteRenderer>().enabled = true;
                cubesForReplacement[i].Find("New Sprite (3)").GetComponent<SpriteRenderer>().enabled = true;
                cubesForReplacement[i].Find("New Sprite (4)").GetComponent<SpriteRenderer>().enabled = true;
                cubesForReplacement[i].Find("New Sprite (5)").GetComponent<SpriteRenderer>().enabled = true;
                cubesForReplacement[i].Find("light").GetComponent<SpriteRenderer>().enabled = true;
                cubesForReplacement[i].GetComponent<Collider>().enabled = true;
                if (i == cubesForReplacement.Length - 1) spritesChanged = true;
            }
        }
    }

    private void CubesDown()
    {
        if (cubesInPlace == false)
        {
            //Rise the buttons
            playButt.position = Vector3.MoveTowards(playButt.position,
                new Vector3(playButt.position.x, 0, playButt.position.z), cubesReplacementSpeed / 10 * Time.deltaTime);
            levelButt.position = Vector3.MoveTowards(levelButt.position,
                new Vector3(levelButt.position.x, 0, levelButt.position.z), cubesReplacementSpeed / 10 * Time.deltaTime);
            optionsButt.position = Vector3.MoveTowards(optionsButt.position,
                new Vector3(optionsButt.position.x, 0, optionsButt.position.z), cubesReplacementSpeed / 10 * Time.deltaTime);
            quitButt.position = Vector3.MoveTowards(quitButt.position,
                new Vector3(quitButt.position.x, 0, quitButt.position.z), cubesReplacementSpeed / 10 * Time.deltaTime);

            //Send the invisible cubes down
            for (int i = 0; i < cubesForReplacement.Length; i++)
                cubesForReplacement[i].position = Vector3.MoveTowards(cubesForReplacement[i].position,
                    new Vector3(cubesForReplacement[i].position.x, 0, cubesForReplacement[i].position.z),
                    cubesReplacementSpeed / 10 * Time.deltaTime);

            if (cubesReplacementSpeed > 0.5)
            {
                cubesReplacementSpeed -= cubesReplacementSpeed / acceleration3;
                if (acceleration3 > 2) acceleration3 -= 2;
            }

        }

        if (playButt.position.y <= 0) cubesInPlace = true;
    }
}
