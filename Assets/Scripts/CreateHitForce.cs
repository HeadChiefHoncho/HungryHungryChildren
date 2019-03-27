using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHitForce : MonoBehaviour {

    public float scaleFactor = 300.0f; // Changes how strong the applied force is to hit the destructible object
    private float lastPlayedTime = 0.0f;
    private float audioCooldown = 1.0f;
    public AudioSource hitSound;
    //public AudioSource audios;
    //public AudioClip clip;

    void Start () {
        //audios = GetComponent<AudioSource>();
        
    }

    // This collider will apply a force to everything inside it when a swing happens
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer != 8 && !collider.gameObject.isStatic) // ensures the collider cannot add force to the player
        {
            if ((Time.time - audioCooldown) >= lastPlayedTime)
            {
                hitSound.Play();
                lastPlayedTime = Time.time;
            }
            Rigidbody body = collider.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = collider.gameObject.transform.position - transform.position;
            if (body)
            {
                body.AddForceAtPosition(direction * scaleFactor, transform.position);
            }
            //Play the sound effect of the hit performance.
            //audios.Play();
        }   
    }
}