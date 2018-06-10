using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{

    public GameObject parent;
    private GameObject newParent;
    private SpriteRenderer sprite;
    private BlendCharacters blend;
    private RewindTime rewind;

    // Use this for initialization
    void Start()
    {
        blend = FindObjectOfType<BlendCharacters>();
        rewind = FindObjectOfType<RewindTime>();
        newParent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        //sprite = transform.Find("Face").GetComponent<SpriteRenderer>();
        //if (sprite.enabled == false)
        //{
        if (blend.isWhite == false || rewind.isRewinding)
        {
            newParent = parent;
        }
        else
        {
            newParent = GameObject.Find("Whitey_Player");
        }


        transform.position = newParent.transform.position;
        if(!rewind.isRewinding)
        parent.transform.position = newParent.transform.position;
        //}
    }
}
