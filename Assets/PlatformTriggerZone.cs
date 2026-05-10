using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerZone : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Quando il player entra nella zona, diventa figlio della piattaforma
            other.transform.SetParent(transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Quando il player esce dalla zona, rimuovi il parent per farlo muovere indipendentemente
            other.transform.SetParent(null);
        }
    }
}
