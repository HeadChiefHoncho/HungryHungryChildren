using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTimingController : MonoBehaviour {

    Rigidbody  rigidBody;
    bool       activated;
    bool       wasActivatedAtSomePoint = false;
    int        timeSinceActivation     = 0;
    public int enabledTime             = 250;
    int        additionalRandomEnabledTime;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        additionalRandomEnabledTime = (int) (Random.value * 50);
	}

	// Update is called once per frame
	void Update () {
        if (activated) {
            timeSinceActivation++;
        }
        if (timeSinceActivation >= enabledTime + additionalRandomEnabledTime) {
            DisableCube();
        }
	}

    // my functions
    // will happen when a cube collides with the player
    void EnableCube() {
        rigidBody.isKinematic      = false;
        rigidBody.detectCollisions = true;
        activated                  = true;
        wasActivatedAtSomePoint    = true;
        rigidBody.gameObject.layer = 2;
        //So we add the statement that is used to count how many blocks destroyed here.
        GameState.blocksDestroyed = GameState.blocksDestroyed + 1;
    }

    // will happen after some set amount of time from when the cube collides with the player
    void DisableCube() {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider otherObjectCollider) {
        if (otherObjectCollider.gameObject.tag == "PlayerBat" && !wasActivatedAtSomePoint) {
            EnableCube();
        }
    }
}
