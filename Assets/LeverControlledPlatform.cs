using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControlledPlatform : MonoBehaviour
{
    [Header("Lever Setup")]
    public Transform lever;
    public Vector3 leverAxis = Vector3.right;
    public float neutralAngle = 0f;
    public float maxAngleDelta = 30f;

    [Header("Platform Setup")]
    public Transform platform;
    public float speed = 2f;
    public float minHeight = 0f;
    public float maxHeight = 5f;

    [HideInInspector]
    public bool isMovingBackward = false;

    [HideInInspector]
    public bool isPlatformInBackPosition = false; 

    private Rigidbody platformRb;

    void Start()
    {
        platformRb = platform.GetComponent<Rigidbody>();
        platformRb.isKinematic = true; 
    }

    void FixedUpdate()
    {
        if (isMovingBackward || isPlatformInBackPosition) return;

        float angle = GetLeverAngle();
        float delta = angle - neutralAngle;

        Vector3 targetPos = platform.position; 

        if (Mathf.Abs(delta - maxAngleDelta) < 1f)
        {
            targetPos.y += speed * Time.fixedDeltaTime;
        }
        else if (Mathf.Abs(delta + maxAngleDelta) < 1f)
        {
            // mantiene la velocità costante anche se il framerate varia.
            targetPos.y -= speed * Time.fixedDeltaTime;
        }

        targetPos.y = Mathf.Clamp(targetPos.y, minHeight, maxHeight);
        
        
        platformRb.MovePosition(targetPos);
    }

    private float GetLeverAngle()
    {
        Vector3 localEuler = lever.localEulerAngles;
        float angle = 0f;

        if (leverAxis == Vector3.right) angle = localEuler.x;
        else if (leverAxis == Vector3.up) angle = localEuler.y;
        else if (leverAxis == Vector3.forward) angle = localEuler.z;

        if (angle > 180f) angle -= 360f;
        return angle;
    }
}