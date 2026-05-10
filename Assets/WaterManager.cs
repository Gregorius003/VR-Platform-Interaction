using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class WaterManager : MonoBehaviour
{
    private MeshFilter meshFilter;
    private bool waveManagerReady = false;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] originalVertices;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        // Verifica se WaveManager è pronto all'avvio
        if (WaveManager.instance != null)
        {
            waveManagerReady = true;
        }
        
        // Inizializza il mesh e salva i vertici originali
        if (meshFilter != null && meshFilter.mesh != null)
        {
            mesh = meshFilter.mesh;
            originalVertices = mesh.vertices;
            if (originalVertices != null && originalVertices.Length > 0)
            {
                vertices = new Vector3[originalVertices.Length];
                // Copia i vertici originali
                for (int i = 0; i < originalVertices.Length; i++)
                {
                    vertices[i] = originalVertices[i];
                }
            }
        }
    }

    private void Update()
    {
        // Se WaveManager non era pronto, riprova a trovarlo
        if (!waveManagerReady && WaveManager.instance != null)
        {
            waveManagerReady = true;
        }

        // Procedi solo se WaveManager è pronto
        if (!waveManagerReady)
            return;

        // Controllo se tutto è inizializzato correttamente
        if (mesh == null || vertices == null || originalVertices == null)
            return;

        // Verifica che gli array abbiano la stessa lunghezza
        if (vertices.Length != originalVertices.Length)
            return;

        // Riusa l'array esistente invece di crearne uno nuovo
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = originalVertices[i];
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x);
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private void OnDestroy()
    {
        // Pulisci la memoria quando l'oggetto viene distrutto
        if (mesh != null && originalVertices != null)
        {
            mesh.vertices = originalVertices;
        }
    }
}
