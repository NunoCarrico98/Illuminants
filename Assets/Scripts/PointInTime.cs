using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime {

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;
        public AnimationClip animation;

    public PointInTime(Transform t, Vector3 v, Vector3 aV, AnimationClip anim)
    {
        position = t.position;
        rotation = t.rotation;
        velocity = v;
        angularVelocity = aV;
        animation = anim;
    }

}
