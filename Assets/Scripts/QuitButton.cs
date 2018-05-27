using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public float cubesRiseSpeed = 200f;

    private float initPos;
    private float newPos;
    private bool goUp = false;
    private bool play = false;
    private bool noMorePlayingAround = false;
    private Transform playButt;

    private void Start()
    {
        initPos = transform.position.y;
        newPos = initPos + 32;
        playButt = GameObject.Find("Line (5)").transform.Find("MyCube (5)");
        play = playButt.GetComponent<PlayButton>().play;
        noMorePlayingAround = playButt.GetComponent<PlayButton>().noMorePlayingAround;
    }
    private void OnMouseOver()
    {
        if(play == false) goUp = true;
    }

    private void OnMouseDown()
    {
        if(play == false) Application.Quit();
    }

    private void Update()
    {
        play = playButt.GetComponent<PlayButton>().play;
        noMorePlayingAround = playButt.GetComponent<PlayButton>().noMorePlayingAround;

        if (goUp == true && noMorePlayingAround == false)
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
}
