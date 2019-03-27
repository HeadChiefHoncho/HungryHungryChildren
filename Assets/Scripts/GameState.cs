using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is the test GameState script.
public class GameState {

    // Joystick Buttons & Axes
    public const string buttonA = "joystick 1 button 0";
    public const string buttonB = "joystick 1 button 1";
    public const string buttonX = "joystick 1 button 2";
    public const string buttonY = "joystick 1 button 3";
    public const string buttonPause = "joystick 1 button 7";

    // Axes
    // Left Joystick Vertical Axis & W/S keys
    public const string verticalAxis = "Vertical Axis";
    public const string verticalKey = "Vertical Key";
    // Left Joystick Horizontal Axis & A/D keys
    public const string horizontalAxis = "Horizontal Axis";
    public const string horizontalKey = "Horizontal Key";
    // Right Joystick Vertical Axis
    public const string cameraVAxis = "Camera Vertical Axis";
    public const string cameraVKey = "Camera Vertical Key";
    // Right Joystick Horizontal Axis
    public const string cameraHAxis = "Camera Horizontal Axis";
    public const string cameraHKey = "Camera Horizontal Key";

    // Keyboard Keys
    public const string returnKey = "return";
    public const string pauseKey = "p";
    public const string swingKey = "e";
    public const string jumpKey = "space";
    public const string smashKey = "r";

    // Level Names
    public const string speedLevel = "Speed Level";
    public const string mazeLevel = "Maze Level";
    public const string mainMenu = "Main Menu";

    private static bool pause = false;
    private static bool foundAllCollectibles = false;

    //This is the variable that is used to store the foundCollectibles variable as primarily int type.
    private static int foundCollectibles = 0;
    private static bool lostGame = false;
    public static int blocksDestroyed = 0;

    // Level Switching Logic
    private static string[] levels = { "Start Level", "Speed Level", "Maze Level" }; //Start Level, 
    private static int levelIndex = 0;
    private static string currentLevel = levels[levelIndex];

    public static void ResetLevelIndex()
    {
        levelIndex = 0;
        currentLevel = levels[levelIndex];
    }

    public static bool NextLevel()
    {
        levelIndex++;
        if (levelIndex >= levels.Length)
        {
            levelIndex = levels.Length;
            return true;
        }
        currentLevel = levels[levelIndex];
        return false;
    }

    public static string GetFirstLevel()
    {
        return levels[0];
    }

    public static string GetCurrentLevel()
    {
        return levels[levelIndex];
    }

    public static void PauseGame()
    {
        pause = true;
        //Audios.
    }

    public static void UnpauseGame()
    {
        pause = false;
    }

    public static void FoundCollectible()
    {
        int cookiesLeft = CountCollectibles();
        Debug.Log(cookiesLeft);
        if (cookiesLeft <= 0)
        {
            foundAllCollectibles = true;
        }
    }

    public static void SetWon()
    {
        foundAllCollectibles = true;
    }

    public static bool GameWon()
    {
        return foundAllCollectibles;
    }

    public static int CollectiblesLeft()
    {
        int cookiesLeft = CountCollectibles();
        return cookiesLeft;
    }

    public static int CollectiblesFound()
    {
        return foundCollectibles;
    }

    public static bool GamePaused()
    {
        return pause;
    }

    public static bool GameLost()
    {
        return lostGame;
    }

    public static void LoseGame()
    {
        lostGame = true;
    }

    public static int CountCollectibles()
    {
        return GameObject.Find("Collectibles").gameObject.GetComponent<Transform>().childCount;
    }

    //This is the method that is used to calculate the number of blocks been destroyed.
    private static int CountDestroyedBlocks()
    {
        return 0;
    }
	
    public static void ResetGameState()
    {
        pause = false;
        foundAllCollectibles = false;
        foundCollectibles = 0;
        blocksDestroyed = 0;
        lostGame = false;
    }
}
