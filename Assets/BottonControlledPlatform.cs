
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControlledPlatform : MonoBehaviour
{
    public Transform platform;       // Platform to move
    public Transform button;         // Button to animate

    public float moveDistance = -5f;   // Z offset for backward movement (should be negative)
    public float moveSpeed = 1f;       // Movement speed
    public float pressDepth = 0.1f;    // Visual press depth of the button

    private Vector3 platformStartPos;
    private Vector3 platformBackPos;
    private Vector3 buttonStartPos;

    private bool isPressed = false;
    private bool isMoving = false;
    private bool goingBackward = true;
    private bool queuedInput = false;
    private bool isCurrentlyInBackPosition = false;

    private Rigidbody rb;

    [Header("Optional Lever Block")]
    public LeverControlledPlatform leverControlledPlatform;

    void Start()
    {
        platformStartPos = platform.position;
        platformBackPos = platformStartPos + new Vector3(0f, 0f, moveDistance);  // moveDistance should be NEGATIVE
        buttonStartPos = button.localPosition;

        rb = platform.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing from the platform!");
        }
    }

    void Update()
    {
        // Handle input
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Allow backward movement only if at start OR at minimum height
            if (!isMoving && goingBackward)
            {
                if (IsAtStartPosition() || IsAtMinHeight())
                {
                    queuedInput = true;
                    isPressed = true;
                }
            }

            // Allow forward movement freely when not moving
            else if (!isMoving && !goingBackward)
            {
                queuedInput = true;
                isPressed = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            isPressed = false;
        }

        // Animate button press
        if (isPressed)
        {
            button.localPosition = Vector3.Lerp(button.localPosition, buttonStartPos - new Vector3(0f, 0f, pressDepth), 10f * Time.deltaTime);
        }
        else
        {
            button.localPosition = Vector3.Lerp(button.localPosition, buttonStartPos, 10f * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (queuedInput && !isMoving)
        {
            isMoving = true;
            queuedInput = false;

            if (goingBackward && leverControlledPlatform != null)
            {
                leverControlledPlatform.isMovingBackward = true;
                leverControlledPlatform.isPlatformInBackPosition = true; // Block vertical movement
            }
        }

        if (isMoving)
        {
            Vector3 target = goingBackward ? platformBackPos : platformStartPos;
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector3.Distance(rb.position, target) < 0.01f)
            {
                rb.MovePosition(target);
                isMoving = false;
                goingBackward = !goingBackward;
                isCurrentlyInBackPosition = target == platformBackPos;

                if (leverControlledPlatform != null)
                {
                    leverControlledPlatform.isMovingBackward = false;
                    leverControlledPlatform.isPlatformInBackPosition = isCurrentlyInBackPosition; 
                }
            }
        }
    }

    private bool IsAtStartPosition()
    {
        /* Restituisce true se la piattaforma è molto vicina alla sua posizione iniziale. 
        Viene usato per permettere il movimento all'indietro solo da questa posizione. */
        return Vector3.Distance(platform.position, platformStartPos) < 0.01f;
    }

    private bool IsAtMinHeight()
    {
        return leverControlledPlatform != null && platform.position.y <= leverControlledPlatform.minHeight + 0.01f;
    }
}
