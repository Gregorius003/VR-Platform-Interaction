using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterOscillator : MonoBehaviour
{
     public float amplitude = 0.5f;
    public float frequency = 1f;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = pos;
    }
}
