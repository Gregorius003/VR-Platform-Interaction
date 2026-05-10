using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdvancedFloatObject : MonoBehaviour
{
    public Transform waterTransform;     // Reference al livello dell'acqua
    public float floatStrength = 10f;    // Buoyancy force
    public float waterDrag = 1f;         // Drag resistance in water
    public float floatDamping = 0.5f;    // Angular drag damping
    public float floatThreshold = 0.5f;  // How far above water the object can float before stopping the force

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Debug.Log("FixedUpdate called");
        float waterHeight = waterTransform.position.y;
        float objectY = transform.position.y;
        float depth = waterHeight - objectY;

        // Se è parzialmente immerso (o appena sopra), continua ad applicare forza
        if (depth > -floatThreshold)
        {
            float clampedDepth = Mathf.Clamp01(depth + floatThreshold); // Valori da 0 a 1
            Vector3 buoyancy = Vector3.up * floatStrength * clampedDepth;
            rb.AddForce(buoyancy, ForceMode.Force);

            // Applica drag solo se stiamo "interagendo" con l'acqua
            rb.velocity *= (1f - Time.fixedDeltaTime * waterDrag);
            rb.angularVelocity *= (1f - Time.fixedDeltaTime * (waterDrag + floatDamping));
        }
    }
}
