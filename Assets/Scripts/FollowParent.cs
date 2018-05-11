using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{

    public GameObject parent;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //sprite = transform.Find("Face").GetComponent<SpriteRenderer>();
        //if (sprite.enabled == false)
        //{
            transform.position = parent.transform.position;
        //}
    }
}
