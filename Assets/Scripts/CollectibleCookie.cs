using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCookie : MonoBehaviour {

    public AudioSource source;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
        GameObject collidingObject = collider.gameObject;
        if (collidingObject.layer == 8)
        {
            //Debug.Log("FOUND EET");
            int cookiesLeft = GameState.CountCollectibles() - 1;
            source.Play();
            if (cookiesLeft == 0)
            {
                GameState.SetWon();
            }

            Destroy(transform.parent.gameObject);
            
            //Destroy(this.gameObject);
        }
    }
}
