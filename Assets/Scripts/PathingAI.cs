using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathingAI : MonoBehaviour {

    public GameObject[] waypoints;
    public int currWaypoint;
    public bool foundAllWaypoints = false;
    private NavMeshAgent agent;
    private float waitAroundTime = 4.0f;
    private float waitStartTime = 0.0f;
    private float fieldOfView = 140.0f;

    private Animator animator;
    public GameObject player;
    private float catchDistance = 0.5f;

    public bool canSeePlayer = false;

    public enum AIState
    {
        WaypointPath,
        LookAround,
        ChasePlayer
    };

    public AIState aiState;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;
        aiState = AIState.WaypointPath;
        currWaypoint = -1;
        SetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.GamePaused() || GameState.GameWon() || GameState.GameLost())
        {
            animator.SetBool("move", false);
            agent.destination = transform.position;
        }
        else if (!GameState.GameLost())
        {
            if ((transform.position - player.transform.position).magnitude <= catchDistance)
            {
                animator.SetBool("move", false);
                agent.destination = transform.position;
                GameState.LoseGame();
            }

            RaycastHit hit;
            int layerMask = 1 | (1 << 8);
            bool insideView = Mathf.Abs(Vector3.SignedAngle(transform.forward, player.transform.position - transform.position, new Vector3(0, 1))) < fieldOfView / 2.0f;
            bool gotHit = Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, (player.transform.position - transform.position).magnitude + 2, layerMask);
            if (gotHit)
            {
                if (hit.collider.CompareTag("Player") && insideView)
                {
                    canSeePlayer = true;
                } else
                {
                    canSeePlayer = false;
                }
            }

            // SWITCH ON STATE MACHINE
            switch (aiState)
            {
                case AIState.WaypointPath:
                    if (canSeePlayer)
                    {
                        aiState = AIState.ChasePlayer;
                        break;
                    }
                    if (foundAllWaypoints)
                    {
                        foundAllWaypoints = false;
                        currWaypoint = -1;
                        SetNextWaypoint();
                        break;
                    }

                    if (agent.remainingDistance <= catchDistance)
                    {
                        aiState = AIState.LookAround;
                        waitStartTime = Time.time;
                    }

                    animator.SetBool("move", true);
                    agent.destination = waypoints[currWaypoint].transform.position;
                    animator.SetFloat("v_speed", agent.desiredVelocity.magnitude);
                    break;
                case AIState.LookAround:
                    if (canSeePlayer)
                    {
                        aiState = AIState.ChasePlayer;
                        break;
                    }
                    animator.SetBool("move", false);
                    if ((Time.time - waitAroundTime) >= waitStartTime)
                    {
                        SetNextWaypoint();
                        aiState = AIState.WaypointPath;
                    }
                    break;
                case AIState.ChasePlayer:
                    if (!canSeePlayer)
                    {
                        aiState = AIState.WaypointPath;
                        break;
                    }

                    animator.SetBool("move", true);
                    agent.destination = player.transform.position;
                    animator.SetFloat("v_speed", agent.desiredVelocity.magnitude);

                    break;
            }


        }
    }

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }

    private void UpdateMovement()
    {
        
        if ((transform.position - agent.destination).magnitude <= catchDistance)
        {
            animator.SetBool("move", false);
            agent.destination = transform.position;
            // TODO testing
            Debug.Log("GOT HERE");
            GameState.LoseGame();
        }
        else if (animator.GetBool("move"))
        {
            agent.destination = player.transform.position;
            animator.SetFloat("v_speed", agent.desiredVelocity.magnitude);
        }
    }

    private void SetNextWaypoint()
    {
        if (currWaypoint == waypoints.Length - 1)
        {
            foundAllWaypoints = true;
            currWaypoint = -1;
            return;
        }
        if (waypoints.Length > 0)
        {
            agent.enabled = true;
        }
        currWaypoint += 1;
        if (currWaypoint >= waypoints.Length)
        {
            currWaypoint = waypoints.Length - 1;
        }
        else if (currWaypoint <= 0)
        {
            currWaypoint = 0;
        }
        if (waypoints.Length == 0)
        {
            Debug.Log("No waypoints available");
            agent.enabled = false;
        }
        else
        {
            agent.SetDestination(waypoints[currWaypoint].transform.position);
        }
    }
}


