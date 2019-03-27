using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject buttonSelector;
    public GameObject LoadingMainMenu;
    public GameObject LoadingLevel;
    private float selectorMoveDistance = 31.25f;
    private float selectorBottom = -40.25f;
    private float selectorStart = -9.0f;
    private bool wasUp = false;
    
    // Use this for initialization
	void Start () {
        pauseMenu.gameObject.SetActive(false);
        LoadingLevel.SetActive(false);
        LoadingMainMenu.SetActive(false);
        selectorStart = buttonSelector.transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(GameState.buttonPause) || Input.GetKeyDown(GameState.pauseKey))
        {
            if (GameState.GamePaused())
            {
                pauseMenu.gameObject.SetActive(false);
                GameState.UnpauseGame();
            } else
            {
                GameState.PauseGame();
                pauseMenu = GetComponent<Transform>().GetChild(0).gameObject;
                pauseMenu.gameObject.SetActive(true);
            }
        }
        if (GameState.GamePaused() && !GameState.GameWon())
        {
            if ((Input.GetAxisRaw(GameState.verticalAxis) >= 0.8) || (Input.GetAxisRaw(GameState.verticalAxis) <= -0.8)
            || (Input.GetAxisRaw(GameState.verticalKey) >= 0.8) || (Input.GetAxisRaw(GameState.verticalKey) <= -0.8)
            || (Input.GetAxisRaw(GameState.cameraVAxis) * 10 >= 0.8) || (Input.GetAxisRaw(GameState.cameraVAxis) * 10 <= -0.8)
            || (Input.GetAxisRaw(GameState.cameraVKey) * 10 >= 0.8) || (Input.GetAxisRaw(GameState.cameraVKey) * 10 <= -0.8))
            {
                if (buttonSelector.transform.localPosition.y > selectorBottom + 2 && !wasUp)
                {
                    buttonSelector.transform.localPosition = new Vector3(buttonSelector.transform.localPosition.x,
                        buttonSelector.transform.localPosition.y - selectorMoveDistance, buttonSelector.transform.localPosition.z);
                }
                else if (buttonSelector.transform.localPosition.y < selectorStart - 2 && !wasUp)
                {
                    buttonSelector.transform.localPosition = new Vector3(buttonSelector.transform.localPosition.x,
                        buttonSelector.transform.localPosition.y + selectorMoveDistance, buttonSelector.transform.localPosition.z);
                }
                wasUp = true;
            }
            else
            {
                wasUp = false;
            }

            if ((Input.GetKeyDown(GameState.buttonA) || Input.GetKeyDown(GameState.returnKey)))
            {
                // Check height of selector
                if (buttonSelector.transform.localPosition.y == selectorStart)
                {
                    RetryGame();
                }
                else
                {
                    MainMenu();
                }

            }
        }

        

	}

    void RetryGame()
    {
        LoadingLevel = GetComponent<Transform>().GetChild(6).gameObject;
        LoadingLevel.SetActive(true);
        GameState.ResetGameState();
        SceneManager.LoadScene(GameState.GetCurrentLevel());
    }

    void MainMenu()
    {
        LoadingMainMenu = GetComponent<Transform>().GetChild(5).gameObject;
        LoadingMainMenu.SetActive(true);
        GameState.ResetGameState();
        SceneManager.LoadScene(GameState.mainMenu);
    }

}
