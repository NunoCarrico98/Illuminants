using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    [SerializeField] internal int maxNumberOfLevels;

    internal int currentLevel;

    private bool activeFinalAnims;

    // Use this for initialization
    void Start()
    {
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(CreditsManager.firstVerification)
        {
            MenuManager.toCreditsFromMenu = false;
        }
        activeFinalAnims = GameObject.FindGameObjectWithTag("Characters").GetComponent<NextLevel>().activeFinalAnims;
        currentLevel = GameObject.Find("LevelNumber").GetComponent<GetLevelNumber>().levelNumber;

        if (activeFinalAnims)
        {
            if (GameObject.Find("GameManager").GetComponent<GameManagement>().currentLevel
                    == GameObject.Find("GameManager").GetComponent<GameManagement>().maxNumberOfLevels + 1)
                GetComponentInChildren<BlackScreenManager>().PlayAnimation();
        }
    }
}
