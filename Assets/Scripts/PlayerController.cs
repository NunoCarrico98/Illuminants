using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Use this for initialization
    void Start()
    {
        sprite = transform.Find("Face").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        TurnToCamera();
        ChangeLayer();

    }

    private void FixedUpdate()
    {
    }

    private void TurnToCamera()
    {
        transform.rotation = Quaternion.AngleAxis(Camera.main.transform.rotation.y, Vector3.up);
    }

    private void ChangeLayer()
    {
        if (transform.position.y == 46)
        {
            sprite.sortingOrder = 3;
        }
        else
        {
            sprite.sortingOrder = 5;
        }
    }
}
