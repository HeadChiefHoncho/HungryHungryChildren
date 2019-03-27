using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTriggerZoneController : MonoBehaviour {
    void OnTriggerEnter(Collider otherObjectCollider) {
        if (otherObjectCollider.gameObject.tag == "Player") {
            GameState.LoseGame();
        }
    }
}
