using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverMouseKeyboardControl : MonoBehaviour
{
   public float rotationSpeed = 50f;    // velocità di rotazione
    public Vector3 rotationAxis = Vector3.right;  // asse di rotazione della leva
    public float minAngle = -30f;
    public float maxAngle = 30f;

    void Update()
    {
        float input = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
            input = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            input = -1f;

        // Calcola nuova rotazione
        float angleChange = input * rotationSpeed * Time.deltaTime;
        Vector3 currentEuler = transform.localEulerAngles;

        // Converti angolo da 0-360 a -180 a 180 per X
        float currentAngle = currentEuler.x;
        if (currentAngle > 180f) currentAngle -= 360f;

        // Calcola nuovo angolo limitato
        float newAngle = Mathf.Clamp(currentAngle + angleChange, minAngle, maxAngle);

        // Applica rotazione limitata sull'asse scelto
        currentEuler.x = newAngle;
        transform.localEulerAngles = currentEuler;
    }
}
