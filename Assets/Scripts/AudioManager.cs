using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    private static bool firstTime = true;

    private void Awake()
    {
        if (firstTime)
        {
            GetComponent<AudioSource>().Play(0);
            DontDestroyOnLoad(transform.gameObject);
            firstTime = false;
        }
    }
}
