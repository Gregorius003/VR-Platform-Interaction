using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour
{ 
   public Transform waterTransform;
    public float floatStrength = 10f;
    public float waterDrag = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float waterHeight = waterTransform.position.y;

        if (transform.position.y < waterHeight)
        {
            float depth = waterHeight - transform.position.y;

            // Forza di galleggiamento proporzionale alla profondità
            Vector3 buoyancy = Vector3.up * floatStrength * depth;

            rb.AddForce(buoyancy, ForceMode.Force);

            // Aggiunge drag nell’acqua
            rb.velocity *= (1f - Time.fixedDeltaTime * waterDrag);
            rb.angularVelocity *= (1f - Time.fixedDeltaTime * waterDrag);
        }
    }
}
