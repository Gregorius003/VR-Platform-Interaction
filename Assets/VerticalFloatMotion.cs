using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalFloatMotion : MonoBehaviour
{
    public float floatAmplitude = 0.1f; // Altezza del movimento (molto leggera)
    public float floatFrequency = 0.5f; // Velocità del movimento

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = initialPosition + new Vector3(0f, offsetY, 0f);
    }
}
