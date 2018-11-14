using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    private bool isPaused = false;


	// Update is called once per frame
	void Update () {

        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
            {
                int counter = 0;

                if (!isPaused)
                {
                    isPaused = true;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                    Time.timeScale = 0f;
                    counter++;
                }

                if (isPaused && counter == 0)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    Time.timeScale = 1f;
                    isPaused = false;
                }
            }

            if(Input.GetButtonDown("BButton"))
            {
                if (isPaused)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    Time.timeScale = 1f;
                    isPaused = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("AButton"))
            {

                if (isPaused)
                {
                    isPaused = false;
                    Time.timeScale = 1f;
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }
}
