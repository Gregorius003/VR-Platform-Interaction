using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCenterOfMass : MonoBehaviour
{
     public Vector3 centerOfMassOffset = Vector3.zero;  // Spostamento relativo dal pivot

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.centerOfMass = centerOfMassOffset;
        }
    }
}
