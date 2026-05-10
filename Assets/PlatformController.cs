using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Lever Setup")]
    public Transform lever;
    public Vector3 leverAxis = Vector3.right;
    public float neutralAngle = 0f;
    public float maxAngleDelta = 30f;

    [Header("Platform Setup")]
    public Rigidbody platformRigidbody;  // assegna Rigidbody in Inspector!
    public float speed = 5f;              // aumentato per maggior velocità
    public float minHeight = 0f;
    public float maxHeight = 5f;

    void FixedUpdate()
    {
        float angle = GetLeverAngle();
        float delta = angle - neutralAngle;
        float t = Mathf.Clamp(delta / maxAngleDelta, -1f, 1f);

        // Rimuovi soglia per massima reattività
        Vector3 currentPosition = platformRigidbody.position;
        currentPosition.y += t * speed * Time.fixedDeltaTime;
        currentPosition.y = Mathf.Clamp(currentPosition.y, minHeight, maxHeight);

        platformRigidbody.MovePosition(currentPosition);
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
