using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisualFloat : MonoBehaviour
{
    public float amplitude = 0.4f;   // Altezza dell'oscillazione (aumentata)
    public float frequency = 1.5f;   // Frequenza dell'oscillazione (più veloce)
    public float noiseFactor = 0.1f; // Movimento casuale extra (opzionale)

    private float startY;
    private float randomOffset;

    void Start()
    {
        startY = transform.position.y;
        randomOffset = Random.Range(0f, 2f * Mathf.PI); // Ogni oggetto fluttua con fase diversa
    }

    void Update()
    {
        float sineWave = Mathf.Sin(Time.time * frequency + randomOffset) * amplitude;
        float noise = Mathf.PerlinNoise(Time.time * 0.5f, transform.position.x) * noiseFactor;
        Vector3 pos = transform.position;
        pos.y = startY + sineWave + noise;
        transform.position = pos;
    }
}
