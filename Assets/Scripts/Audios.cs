using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is the test.
[RequireComponent(typeof(AudioSource))]
public class Audios : MonoBehaviour
{
    public AudioClip Playing;
    public AudioClip Idle;
    public AudioClip Death;
    public AudioClip WinCurrentLevel;
    public AudioClip FinalVictory;
    public AudioClip Restarting;
    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.clip = Playing;
    }

    private void Update()
    {
        /*if (audioSource.clip == Playing)
        {
            audioSource.Stop();
        }
        //Game pause, change music to idle.
        if (GameState.GamePaused() == true)
        {
            if (audioSource.clip == Playing)
            {
                audioSource.clip = Idle;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip == Idle)
            {
                audioSource.clip = Playing;
                audioSource.Play();
            }
        }*/
    }

    /*public static void unPauseGame()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource.clip == Playing)
        {
            audioSource.clip = Idle;
            audioSource.Play();
        }
        if (AudioSource.clip == Idle)
        {
            AudioSource.clip = Playing;
            AudioSource.Play();
        }
    }

    public static void Restart()
    {
        if (AudioSource.clip == Playing)
        {
            AudioSource.clip = Restarting;
            AudioSource.Play();
        }
    }

    public static void WinLevel()
    {
        if (AudioSource.clip == Playing)
        {
            AudioSource.clip = WinCurrentLevel;
            AudioSource.Play();
        }
    }

    public static void Victory()
    {
        if (AudioSource.clip == Playing)
        {
            AudioSource.clip = FinalVictory;
            AudioSource.Play();
        }
    }

    public static void GameOver()
    {
        if (AudioSource.clip == Playing)
        {
            AudioSource.clip = Death;
            AudioSource.Play();
        }
    }*/
}