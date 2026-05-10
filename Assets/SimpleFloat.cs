using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFloat : MonoBehaviour
{
     public Transform waterTransform;
    public float floatOffset = 0f; // quanto sopra il livello dell'acqua
    public float smoothSpeed = 2f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (waterTransform == null) return;

        float targetY = waterTransform.position.y + floatOffset;
        Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

        // Movimento morbido verso il livello dell’acqua
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / smoothSpeed);
    }
}
