using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ThirdPersonMovements : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector]
    public float walkSpeed;
    [HideInInspector]
    public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    
    public Transform groundCheck;
    public  float groundDistance = 0.4f;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public Animator myAnimator;

    Vector3 moveDirection;

    Rigidbody rb;

    // WIRE CUTTING
    public float moveDistance = 0.1f; // How much to move the object each time
    public int requiredClicks = 5; // How many clicks are required before the object moves
    public float timeLimit = 5f;
    public GameObject objectToMove; // Reference to the object that will be moved
    public AudioSource audioSource; //

    private int clickCount = 0;
    private Coroutine clickCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround); 

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
            //myAnimator.SetBool("jumping", false);
        }
        else
        {
            rb.drag = 0;
            //myAnimator.SetBool("jumping", true);
        }

        // Check if the player is inside the wire area and presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerInWireArea())
        {
            myAnimator.SetBool("attacking", true);

            //ADD CHEWING AUDIO HERE
            audioSource.Play();

            clickCount++;
            // If the required number of clicks has been reached, move the object
            if (clickCount == requiredClicks)
            {
                objectToMove.transform.position += new Vector3(moveDistance, 0, 0);
                clickCount = 0;
                if (clickCoroutine != null)
                {
                    StopCoroutine(clickCoroutine);
                }
            }
            else if (clickCoroutine == null)
            {
                clickCoroutine = StartCoroutine(ResetClickCount());
            }
        }
        else
        {
            myAnimator.SetBool("attacking", false);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            //myAnimator.SetBool("jumping", true);

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //Debug.Log("VELOCITY: " + flatVel);

        if (flatVel.magnitude == 0)
        {
            myAnimator.SetBool("walking", false);
        } else
        {
            myAnimator.SetBool("walking", true);
        }

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        //myAnimator.SetBool("jumping", false);
        readyToJump = true;
    }

    // Check if the player is inside the wire area
    bool IsPlayerInWireArea()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f); // Change the radius as needed
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("wire"))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetClickCount()
    {
        yield return new WaitForSeconds(timeLimit);
        clickCount = 0;
        clickCoroutine = null;
    }
}
