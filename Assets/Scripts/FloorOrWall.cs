using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorOrWall : MonoBehaviour
{

    private Transform myCube;
    private float height;
    private float initHeight;
    private int firstCheck = 0;
    public float speed = 20f;
    public float timer = 2f;
    private float resetTimer;

    // Use this for initialization
    void Start()
    {
        myCube = gameObject.transform;
        height = myCube.position.y;
        initHeight = height;
        resetTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        height = myCube.position.y;
        if (initHeight == 32 && firstCheck == 0)
        {
            firstCheck = 1;
            myCube.position = (new Vector3(myCube.position.x, 0, myCube.position.z));
        }
        if (height >= 0 && height <= 32 && firstCheck == 1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                myCube.Translate(Vector3.up * Time.deltaTime * speed);
            }
        }

    }
}
