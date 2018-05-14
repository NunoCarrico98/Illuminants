using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    public float cubesRiseSpeed = 200f;
    public float camMoveSpeed = 200f;
    public float camRotationSpeed = 200f;

    public float buttonsRiseSpeed = 100f;
    public float cubesReplacementSpeed = 300f;

    public float timerToStartLevel = 2f;

    public Transform playButt;
    public Transform optionsButt;
    public Transform quitButt;

    public Transform[] cubesForReplacement = new Transform[12];

    public Sprite newSpritesForTop;
    public Sprite newSpritesForSides;
    public Sprite newSpritesForLight;

    private float initPos;
    private float newPos;
    private float angle = 90f;
    private bool goUp = false;
    private bool playWasPressed = false;
    public bool play = false;
    public bool noMorePlayingAround = false;

    private bool buttonsUp = false;
    private bool spritesChanged = false;
    private bool cubesInPlace = false;
    private Transform cam;
    private Vector3 lookPos;
    private Quaternion rotation;
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
    }
    public void OnMouseOver()
    {
        if(play == false) goUp = true;
    }
    private void OnMouseDown()
    {
        playWasPressed = true;
    }

    private void Update()
    {
        if (playWasPressed == true /*&& cubesInPlace == false*/)
        {
            play = true;
            noMorePlayingAround = true;
        } else
        {
            play = false;
        }

        if (playWasPressed == true)
        {
            Cursor.visible = false;
            CameraRotate();
            ChangeButtonsForCubes();
        }

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

    private void CameraRotate()
    {
        cam.position = Vector3.MoveTowards(cam.position, new Vector3(0, 677, -508), camMoveSpeed * Time.deltaTime);
        //cam.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * camRotationSpeed);
        if (angle >= 60)
        {
            angle -= camRotationSpeed / 100;
            if (angle < 60) angle = 60;
            cam.eulerAngles = new Vector3(angle, cam.eulerAngles.y, cam.eulerAngles.z);
        }
        if (camMoveSpeed > 2)
        {
            camMoveSpeed -= camMoveSpeed / acceleration;
            camRotationSpeed -= camRotationSpeed / acceleration;
            if (acceleration > 5) acceleration -= 2 / 10;
        }

        if ((cam.position.x == 0 && cam.position.y == 677 && cam.position.z == -508) && angle == 60)
        {
            if (cubesInPlace == true)
            {
                timerToStartLevel -= Time.deltaTime;
                if (timerToStartLevel <= 0.5)
                {
                    noMorePlayingAround = true;
                }
                    if (timerToStartLevel <= 0) SceneManager.LoadScene("Level1");
            }
        }
    }

    private void ChangeButtonsForCubes()
    {
        ButtonsRise();
        if (buttonsUp == true) ChangeSprites();
        if (spritesChanged == true) CubesDown();
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

        if (buttonsUp == false)
        {
            //Rise the buttons
            playButt.position = Vector3.MoveTowards(playButt.position,
                new Vector3(playButt.position.x, 600, playButt.position.z), buttonsRiseSpeed * Time.deltaTime);
            optionsButt.position = Vector3.MoveTowards(optionsButt.position,
                new Vector3(optionsButt.position.x, 600, optionsButt.position.z), buttonsRiseSpeed * Time.deltaTime);
            quitButt.position = Vector3.MoveTowards(quitButt.position,
                new Vector3(quitButt.position.x, 600, quitButt.position.z), buttonsRiseSpeed * Time.deltaTime);

            //Rise the invisible cubes
            for (int i = 0; i < cubesForReplacement.Length; i++)
                cubesForReplacement[i].position = Vector3.MoveTowards(cubesForReplacement[i].position,
                    new Vector3(cubesForReplacement[i].position.x, 600, cubesForReplacement[i].position.z),
                    buttonsRiseSpeed * Time.deltaTime);

            if (playButt.position.y == 600 && cubesForReplacement[11].position.y == 600) buttonsUp = true;
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
                new Vector3(playButt.position.x, 0, playButt.position.z), cubesReplacementSpeed/10 * Time.deltaTime);
            optionsButt.position = Vector3.MoveTowards(optionsButt.position,
                new Vector3(optionsButt.position.x, 0, optionsButt.position.z), cubesReplacementSpeed/10 * Time.deltaTime);
            quitButt.position = Vector3.MoveTowards(quitButt.position,
                new Vector3(quitButt.position.x, 0, quitButt.position.z), cubesReplacementSpeed/10 * Time.deltaTime);

            //Send the invisible cubes down
            for (int i = 0; i < cubesForReplacement.Length; i++)
                cubesForReplacement[i].position = Vector3.MoveTowards(cubesForReplacement[i].position,
                    new Vector3(cubesForReplacement[i].position.x, 0, cubesForReplacement[i].position.z),
                    cubesReplacementSpeed/10 * Time.deltaTime);

            if (cubesReplacementSpeed > 0.5)
            {
                cubesReplacementSpeed -= cubesReplacementSpeed / acceleration3;
                if (acceleration3 > 2) acceleration3 -= 2;
            }

        }

        if (playButt.position.y <= 0) cubesInPlace = true;
    }
}
