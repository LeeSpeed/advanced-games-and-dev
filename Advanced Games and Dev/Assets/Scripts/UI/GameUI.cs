using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    [SerializeField] GameObject gameOverUI;

    bool isGameOver;

	void Start ()
    {
        PatrolBots.OnPlayerHasBeenSpotted += DisplayGameOverUI;
	}
	
	void Update ()
    {
		if(isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
	}

    void DisplayGameOverUI()
    {
        gameOverUI.SetActive(true);
        isGameOver = true;
        PatrolBots.OnPlayerHasBeenSpotted -= DisplayGameOverUI;
    }


}
