using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public AnimationClip animation;
    public bool up;
    public bool down;
    public bool sideways;

    public PointInTime(Transform character, Transform canvas, Vector3 v, bool isGoingUp, bool isGoingDown, bool isGoingSideways)
    {
        position = character.position;
        rotation = canvas.rotation;
        velocity = v;
        up = isGoingUp;
        down = isGoingDown;
        sideways = isGoingSideways;
    }

}
