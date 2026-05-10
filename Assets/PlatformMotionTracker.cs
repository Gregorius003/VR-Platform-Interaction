using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMotionTracker : MonoBehaviour
{
     private CharacterController characterController;
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (currentPlatform != null)
        {
            // Calcola delta di movimento della piattaforma
            Vector3 platformDelta = currentPlatform.position - lastPlatformPosition;

            // Applica il movimento al character controller
            if (platformDelta.magnitude > 0.0001f)
            {
                characterController.Move(platformDelta);
            }

            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovingPlatform"))
        {
            currentPlatform = hit.collider.transform;
            lastPlatformPosition = currentPlatform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            if (other.transform == currentPlatform)
            {
                currentPlatform = null;
            }
        }
    }
}
