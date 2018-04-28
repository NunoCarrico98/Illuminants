using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        TurnToCamera();

    }

    private void FixedUpdate()
    {
    }

    private void TurnToCamera()
    {
        transform.rotation = Quaternion.AngleAxis(Camera.main.transform.rotation.y, Vector3.up);
    }
}
