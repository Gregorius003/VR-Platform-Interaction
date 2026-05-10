using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMotionFollower : MonoBehaviour
{
    public Transform platform; // piattaforma mobile
    private Vector3 lastPlatformPos;

    private Transform playerTransform = null;
    private bool playerOnPlatform = false;

    private void Start()
    {
        if (platform == null)
            platform = transform.parent; // fallback
        lastPlatformPos = platform.position;
    }

    private void LateUpdate()
    {
        if (playerOnPlatform && playerTransform != null)
        {
            Vector3 platformDelta = platform.position - lastPlatformPos;
            playerTransform.position += platformDelta;
        }

        lastPlatformPos = platform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerOnPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            playerTransform = null;
        }
    }
}
