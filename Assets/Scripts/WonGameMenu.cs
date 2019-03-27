using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonGameMenu : MonoBehaviour {

    public GameObject wonGameMenu;
    public GameObject LoadingMainMenu;
    public GameObject LoadingLevel;
    public GameObject buttonSelector;
    private float selectorMoveDistance = 31.25f;
    private float selectorBottom = -40.25f;
    private float selectorStart = -9.0f;
    private bool wasUp = false;

    // Use this for initialization
    void Start()
    {
        wonGameMenu.gameObject.SetActive(false);
        LoadingLevel.SetActive(false);
        LoadingMainMenu.SetActive(false);
        selectorStart = buttonSelector.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GameWon())
        {
            wonGameMenu = GetComponent<Transform>().GetChild(1).gameObject;
            wonGameMenu.gameObject.SetActive(true);

            // disable the AI so that we don't lose after we win
            Destroy(GameObject.Find("claire"));

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
                    ContinueGame();
                }
                else
                {
                    MainMenu();
                }
            }
        }
    }

    void ContinueGame()
    {
        LoadingLevel = GetComponent<Transform>().GetChild(6).gameObject;
        LoadingLevel.SetActive(true);
        GameState.ResetGameState();
        if (GameState.GetCurrentLevel() == "Maze Level")
        {
            GameState.ResetLevelIndex();
        } else
        {
            GameState.NextLevel();
        }
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
