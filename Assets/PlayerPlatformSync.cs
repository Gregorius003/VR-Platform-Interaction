using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformSync : MonoBehaviour
{
    // Riferimento al CharacterController del player
    public CharacterController characterController;

    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("CharacterController non trovato su questo GameObject o sui suoi figli. Assegnalo manualmente.");
                enabled = false; // Disabilita lo script se non c'è CharacterController
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Controlla se il player sta toccando una piattaforma specifica (es. con un tag "MovingPlatform")
        // Puoi anche controllare il nome, il layer, o avere un riferimento diretto.
        if (hit.gameObject.CompareTag("MovingPlatform"))
        {
            // Verifica se la piattaforma è sotto il player (per evitare parenting con muri laterali)
            // Puoi affinare questa logica se necessario
            float angle = Vector3.Angle(Vector3.up, hit.normal);
            if (angle < 45f) // Se l'angolo della superficie colpita è vicino al "tetto" (cioè, è un pavimento)
            {
                // Parenta il player alla piattaforma.
                // Questo farà sì che il player si muova con la piattaforma.
                transform.SetParent(hit.transform);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Quando il player esce dalla piattaforma (o quando non è più a contatto con essa)
        // Deparenta il player in modo che possa muoversi indipendentemente.
        // Controlla il tag o il layer della piattaforma come in OnControllerColliderHit
        if (other.CompareTag("MovingPlatform"))
        {
            // Assicurati che non ci sia nessun altro contatto con la piattaforma in movimento
            // (opzionale, per evitare di deparentare troppo presto)
            // Se il CharacterController ha smesso di toccare, de-parenta.
            // ATTENZIONE: Questa logica può essere complessa con CharacterController e Trigger.
            // Un'alternativa migliore è deparentare quando il player tenta di muoversi autonomamente.

            // Metodo più robusto: deparenta quando il player non è più a contatto col ground O quando il player usa il thumbstick per muoversi.
            // Per ora, un semplice deparenting può essere problematico se il player rimbalza.
            // Si consiglia di deparentare solo quando l'input di movimento del player viene rilevato.
        }
    }

    // Metodo più robusto per il deparenting:
    // Se il player inizia a muoversi con il thumbstick/controller, deparenta.
    // Questo richiede accesso all'input del player.
    // Non posso scriverlo direttamente qui senza conoscere le tue azioni di input.
    // Esempio concettuale (da adattare):
    // public ActionBasedContinuousMoveProvider moveProvider; // Assegna questo nell'Inspector
    // void Update()
    // {
    //     if (moveProvider.leftHandMoveAction.action.ReadValue<Vector2>().magnitude > 0.1f ||
    //         moveProvider.rightHandMoveAction.action.ReadValue<Vector2>().magnitude > 0.1f)
    //     {
    //         if (transform.parent != null && transform.parent.CompareTag("MovingPlatform"))
    //         {
    //             transform.SetParent(null); // Deparenta
    //         }
    //     }
    // }
}
