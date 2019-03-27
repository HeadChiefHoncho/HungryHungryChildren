using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject buttonSelector;
    private float selectorMoveDistance = 28.74f;
    private float selectorBottom = 31.84f;
    private float selectorStart = 60.58f;
    private bool wasUp = false;

    // Use this for initialization
    void Start()
    {
        mainMenu.gameObject.SetActive(true);
        selectorStart = buttonSelector.GetComponent<Transform>().localPosition.y;
    }

    // Update is called once per frame
    void Update () {
        
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

        if (Input.GetKeyDown(GameState.buttonA) || Input.GetKeyDown(GameState.returnKey))
        {
            if (buttonSelector.transform.localPosition.y == selectorStart)
            {
                StartGame();
            }
            else
            {
                ExitGame();
            }
        }
    }

    void StartGame()
    {
        GameState.ResetLevelIndex();
        SceneManager.LoadScene(GameState.GetFirstLevel());
    }

    void ExitGame()
    {
        Application.Quit();
    }
}