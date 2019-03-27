using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyMovement : MonoBehaviour {

    public AudioSource audios;
    public AudioClip clip;

    public GameObject bat;
    public GameObject hitForce; // collider that adds extra force after swing
    public FixedJoint handBatJoint; // fixed joint from right hand to bat
    public Rigidbody batRigidbody;

    private Animator animator; // player animator controller

    // Baseball bat orientation variables
    private Vector3 direction; // used for player rotation
    private Vector3 batIdleTrans = new Vector3(0.104f, -0.062f, -0.148f); // local position of bat while idle
    private Vector3 batIdleRot = new Vector3(-78.928f, 211.185f, -31.269f); // local rotation of bat while idle
    private Vector3 batSwingTrans = new Vector3(0.0888f, -0.0301f, -0.1503f); // local position of bat while swinging
    private Vector3 batSwingRot = new Vector3(-58.624f, 351.56f, -173.957f); // local rotation of bat while swinging

    // Trigger cooldowns for actions
    private float lastSwingTime = 0.0f;
    private float swingCooldown = 1f;
    private float cameraRotation = 0f;

    // Animator Controller Floats
    private float vertical_speed; // sent to animator controller - forward motion
    private float horizontal_speed; // sent to animator controller - turning motion
    private float rotateDegreesPerSecond = 120.0f;


    void Start () {
        animator = GetComponent<Animator>();
        hitForce.GetComponent<CapsuleCollider>().enabled = false;
        direction = Vector3.zero;
        //Initialize the AudioSource
        audios = GetComponent<AudioSource>();
    }


	void Update () {
        if (GameState.GamePaused() || GameState.GameWon() || GameState.GameLost())
        {
            animator.SetFloat("v_speed", 0);
            animator.SetFloat("h_speed", 0);
            animator.SetBool("turningInPlace", false);
        } else
        {
            animator.SetBool("isJumping", animator.GetCurrentAnimatorStateInfo(0).IsName("JumpForward"));
            vertical_speed = Input.GetAxisRaw(GameState.verticalAxis);
            if (vertical_speed <= 0.05 && vertical_speed >= -0.05) {
                vertical_speed = Input.GetAxisRaw(GameState.verticalKey);
            }
            horizontal_speed = Input.GetAxisRaw(GameState.horizontalAxis); // corresponds to right joystick X-axis on controller
            if (horizontal_speed <= 0.05 && horizontal_speed >= -0.05)
            {
                horizontal_speed = Input.GetAxisRaw(GameState.horizontalKey);
            }

            animator.SetFloat("v_speed", vertical_speed); // send forward movement to AC
            animator.SetFloat("h_speed", horizontal_speed); // send turning movement to AC

            if ((Input.GetKeyDown(GameState.buttonB) || Input.GetKeyDown(GameState.swingKey)) && ((Time.time - lastSwingTime) > swingCooldown)) { // if A button pressed, perform a swing
                animator.SetTrigger("swing");
                lastSwingTime = Time.time;
                // The bat needs a different pos & rot relative to the parent hand when swinging, so below
                // we disconnet the fixed joint in order to change these, and then reattach the bat to the hand.
                handBatJoint.connectedBody = null;
                bat.transform.localPosition = batSwingTrans;
                Quaternion rotation = Quaternion.Euler(batSwingRot);
                bat.transform.localRotation = Quaternion.RotateTowards(bat.transform.localRotation, rotation,
                    1000 * Time.deltaTime); // This particular operation accounts for the animation transition
                handBatJoint.connectedBody = batRigidbody;
            } else if ((Input.GetKeyDown(GameState.buttonA) || Input.GetKeyDown(GameState.jumpKey)) && !animator.GetCurrentAnimatorStateInfo(1).IsName("Jump"))
            {
                animator.SetTrigger("jump");
            }

            if ((horizontal_speed >= 0.1 || horizontal_speed <= -0.1) && (vertical_speed >= 0.1 || vertical_speed <= -0.1))
            {
                // Turns the character based on right stick. Doesn't affect animations,
                // but does affect the camera since it follows the player.
                animator.SetBool("turningInPlace", false);
            }
            else if ((horizontal_speed >= 0.1 || horizontal_speed <= -0.1) && (vertical_speed <= 0.1 && vertical_speed >= -0.1))
            {
                // Turns the character in place if there is no forward movement.
                animator.SetBool("turningInPlace", true);
                animator.SetFloat("h_speed", horizontal_speed);
            }
            else
            {
                animator.SetBool("turningInPlace", false);
            }

            // Rotate based on camera rotation
            cameraRotation = Input.GetAxisRaw(GameState.cameraHAxis);
            if (cameraRotation <= 0.03 && cameraRotation >= -0.03)
            {
                cameraRotation = Input.GetAxisRaw(GameState.cameraHKey) / 3.0f;
            }
            if ((cameraRotation >= 0.05 || cameraRotation <= -0.05) && (vertical_speed >= 0.1 || vertical_speed <= -0.1))
            {
                direction = new Vector3(cameraRotation, 0, 0);
                direction = transform.TransformDirection(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction),
                    rotateDegreesPerSecond * Time.deltaTime);
            } else if ((cameraRotation >= 0.01 || cameraRotation <= -0.01) && (vertical_speed <= 0.1 && vertical_speed >= -0.1))
            {
                animator.SetBool("turningInPlace", true);
                animator.SetFloat("h_speed", cameraRotation * 10);
            }

            // Swing bat
            if (animator.GetCurrentAnimatorStateInfo(1).IsName("Swing"))
            {
                // The bat needs a different pos & rot relative to the parent hand when swinging, so below
                // we disconnet the fixed joint in order to change these, and then reattach the bat to the hand.
                handBatJoint.connectedBody = null;
                bat.transform.localPosition = batSwingTrans;
                Quaternion rotation = Quaternion.Euler(batSwingRot);
                bat.transform.localRotation = rotation;
                handBatJoint.connectedBody = batRigidbody;
                hitForce.GetComponent<CapsuleCollider>().enabled = true;
            }
            else
            {
                // The bat needs a different pos & rot relative to the parent hand when swinging, so below
                // we disconnet the fixed joint in order to change these, and then reattach the bat to the hand.
                handBatJoint.connectedBody = null;
                bat.transform.localPosition = batIdleTrans;
                Quaternion rotation = Quaternion.Euler(batIdleRot);
                bat.transform.localRotation = rotation;
                handBatJoint.connectedBody = batRigidbody;
                hitForce.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }
}

