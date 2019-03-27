using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]

public class HumanoidFollow : MonoBehaviour {

    private Animator animator;
    private NavMeshAgent agent;
    public GameObject player;
    private float catchDistance     = 0.5f;
    private bool  waitingAtStart    = true;
    private float   timeRemaining = 5.0f;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;
	}

	// Update is called once per frame
	void Update () {
        if (GameState.GamePaused() || GameState.GameWon() || GameState.GameLost())
        {
            animator.SetBool("move", false);
            agent.destination = transform.position;
        }
        else if (!GameState.GameLost())
        {  
            if (waitingAtStart)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0.0f)
                {
                    animator.SetBool("move", true);
                    agent.destination = player.transform.position;
                    waitingAtStart = false;
                }
            } else
            {
                animator.SetBool("move", true);
            }

            if ((transform.position - player.transform.position).magnitude <= catchDistance)
            {
                animator.SetBool("move", false);
                agent.destination = transform.position;
                GameState.LoseGame();
            }
            else if (animator.GetBool("move"))
            {
                agent.destination = player.transform.position;
                animator.SetFloat("v_speed", agent.desiredVelocity.magnitude);
            }
        }   
	}

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }
}
