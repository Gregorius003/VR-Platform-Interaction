using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyOscillator : MonoBehaviour
{
    public float rotationAmplitude = 2f; // quanto si inclina (gradi)
    public float rotationFrequency = 0.5f; // quanto è veloce l'oscillazione

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        float rotX = Mathf.Sin(Time.time * rotationFrequency) * rotationAmplitude;
        float rotZ = Mathf.Cos(Time.time * rotationFrequency * 1.2f) * rotationAmplitude;

        Quaternion offsetRotation = Quaternion.Euler(rotX, 0f, rotZ);
        transform.localRotation = initialRotation * offsetRotation;
    }
}
