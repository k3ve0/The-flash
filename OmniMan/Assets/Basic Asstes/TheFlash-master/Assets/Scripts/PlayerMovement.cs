using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Regular Character Variables")]
    public Animator anim;
    public CharacterController controller;
    public float walkSpeed = 6;
    public float turnSpeed = 50f;
    public Transform cameraTransform;

    [Header("Jump & Grounding Variables")]
    public float jumpHeight;
    public float gravity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    Vector3 velocity;

    [Header("Speed Mode Variables")]
    public float superSpeed = 15;
    public float boostSpeed = 25; // 🔥 Shift boost while in Speed Mode
    public float animSpeed = 1.5f;
    public GameObject lightningObj;
    public GameObject speedModeAura;
    public bool inSpeedMode = false;

    [Header("Audio Sources")]
    public AudioSource speedModeAudio;   // Plays when speed mode toggles
    public AudioSource moveAudio;        // Loops while moving
    public AudioSource stopMoveAudio;    // Plays when player stops

    private bool wasMoving = false;
    private float currentSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        lightningObj.SetActive(false);
        speedModeAura.SetActive(false);
        currentSpeed = walkSpeed;

        // Ensure no audio starts playing
        if (speedModeAudio != null) { speedModeAudio.playOnAwake = false; speedModeAudio.Stop(); }
        if (moveAudio != null) { moveAudio.playOnAwake = false; moveAudio.loop = true; moveAudio.Stop(); }
        if (stopMoveAudio != null) { stopMoveAudio.playOnAwake = false; stopMoveAudio.Stop(); }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * z + right * x;
        bool isMoving = moveDirection.magnitude > 0.1f;

        // 🧠 Handle Speed Values
        if (inSpeedMode)
        {
            // If holding Shift → go even faster
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = boostSpeed;
            else
                currentSpeed = superSpeed;
        }
        else
        {
            // Normal walking speed when not in Speed Mode
            currentSpeed = walkSpeed;
        }

        // Handle movement sound only in Speed Mode
        if (inSpeedMode)
        {
            if (isMoving && !wasMoving)
            {
                if (moveAudio != null && !moveAudio.isPlaying)
                    moveAudio.Play();
            }
            else if (!isMoving && wasMoving)
            {
                if (moveAudio != null && moveAudio.isPlaying)
                    moveAudio.Stop();

                if (stopMoveAudio != null)
                    stopMoveAudio.Play();
            }
        }
        wasMoving = isMoving;

        // Movement
        if (isMoving)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        // Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Visuals
        speedModeAura.SetActive(inSpeedMode);
        lightningObj.SetActive(inSpeedMode && isMoving);
        jumpHeight = inSpeedMode ? 20f : 5f;

        // Toggle Speed Mode
        if (Input.GetKeyDown(KeyCode.F))
        {
            inSpeedMode = !inSpeedMode;
            anim.speed = inSpeedMode ? animSpeed : 1f;
            currentSpeed = inSpeedMode ? superSpeed : walkSpeed;

            if (speedModeAudio != null)
                speedModeAudio.Play();

            if (!inSpeedMode)
            {
                // Turn off effects and audio
                lightningObj.SetActive(false);
                speedModeAura.SetActive(false);

                if (moveAudio != null) moveAudio.Stop();
                if (stopMoveAudio != null) stopMoveAudio.Stop();
                if (speedModeAudio != null) speedModeAudio.Stop();
            }
        }

        anim.SetFloat("Speed", moveDirection.magnitude);
    }
}
