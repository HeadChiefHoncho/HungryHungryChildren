using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cookies_Count : MonoBehaviour
{
    public static int Count;
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text = GetComponent<Text>();
        text.text = "Cookies Left:  " + GameState.CollectiblesLeft();
    }
}
