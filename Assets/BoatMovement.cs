using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Vector3 startPosition; // Posizione iniziale della barca
    public Vector3 endPosition;   // Posizione finale della barca
    public float speed = 1.0f;    // Velocità di movimento della barca

    private bool movingForward = true; // Indica se la barca si sta muovendo in avanti

    void Update()
    {
        // Se la barca si sta muovendo in avanti
        if (movingForward)
        {
            // Muovi la barca verso la endPosition
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

            // Se la barca ha raggiunto la endPosition, inizia a tornare indietro
            if (transform.position == endPosition)
            {
                movingForward = false;
            }
        }
        // Se la barca si sta muovendo indietro
        else
        {
            // Muovi la barca verso la startPosition
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

            // Se la barca ha raggiunto la startPosition, inizia a muoversi in avanti
            if (transform.position == startPosition)
            {
                movingForward = true;
            }
        }
    }

}
