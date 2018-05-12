using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperGlowBifrost : MonoBehaviour
{

    [SerializeField, Range(1, 7)]
    public float radius;
    [SerializeField, Range(1, 7)]
    public float radiusForBifrosts;
    private float resetRadius;
    private bool bifrostsActive = false;

    // Use this for initialization
    void Start()
    {
        resetRadius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        bifrostsActive = GameObject.FindGameObjectWithTag("Objects").GetComponent<SpawnScript>().bifrostActive;

        if (bifrostsActive == true)
        {
            if (radius < radiusForBifrosts)
            {
                radius += 0.5f;
            }

        } else
        {
            if (radius > resetRadius)
            {
                radius -= 0.5f;
            }
        }
	}
}
