using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParameters : MonoBehaviour
{
    private float lastPos;
    Animator animator;
    private bool up;
    private bool down;
    private float vertical;
    private bool isMoving;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        lastPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        up = (Mathf.Abs(transform.position.z) - Mathf.Abs(lastPos)) < 0; //(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow));
        down = (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow));//(Mathf.Abs(transform.position.z) - Mathf.Abs(lastPos)) > 0;
        vertical = Input.GetAxis("Vertical");
        isMoving = Input.anyKeyDown;

        animator.SetBool("Up", up);
        animator.SetBool("Down", down);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("Moving", isMoving);

        lastPos = transform.position.z;
    }
}
