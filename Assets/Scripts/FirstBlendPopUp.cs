using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBlendPopUp : MonoBehaviour
{

    public float popUpTime = 0.4f;
    private float timer = 0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (BlendCharacters.playerBlended == 1)
        {
            PopUp();
        }
    }

    private void PopUp()
    {

        if (timer < popUpTime) timer += Time.deltaTime;
        if (timer < popUpTime)
        {
            Time.timeScale = 0f;
            transform.Find("OopsBlend").gameObject.SetActive(true);

            if (Input.anyKeyDown)
            {
                StopPopUp();
            }
        }

    }

    private void StopPopUp()
    {
        Time.timeScale = 1f;
        transform.Find("OopsBlend").gameObject.SetActive(false);
        BlendCharacters.playerBlended = 2;
    }
}
