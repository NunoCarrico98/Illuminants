using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour {

    internal static bool firstVerification = false;

	// Use this for initialization
	void Start () {

        firstVerification = false;

        if (!MenuManager.toCreditsFromMenu)
        {
            GameObject.Find("RawImage").GetComponent<RawImage>().enabled = true;
        }

        firstVerification = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("AButton")
            || Input.GetButtonDown("BButton"))
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
