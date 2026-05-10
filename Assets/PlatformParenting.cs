using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    private Transform platformTransform;

    // Riferimento al CharacterController del player quando è sulla piattaforma
    private CharacterController currentPlayerCC;


    private void Start()
    {
        // Assicura che il genitore di questo trigger sia la piattaforma con Rigidbody.
        platformTransform = transform.parent;
        if (platformTransform == null)
        {
            Debug.LogError("PlatformParenting: Il GameObject con questo script deve essere un figlio della piattaforma!", this);
            enabled = false; 
        }
        else if (platformTransform.GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning("PlatformParenting: La piattaforma genitore non ha un Rigidbody! Il parenting potrebbe non funzionare correttamente con il movimento fisico.", platformTransform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            // Ottiene il CharacterController dal player che ha attivato il trigger
            currentPlayerCC = other.GetComponent<CharacterController>();
            if (currentPlayerCC == null)
            {
                // Se il CharacterController non è direttamente sul collider, cerca sul genitore dell'XR Origin
                currentPlayerCC = other.GetComponentInParent<CharacterController>();
            }

            if (currentPlayerCC != null && platformTransform != null)
            {
                // Parenta il Transform dell'XR Origin (quello con il CharacterController) alla piattaforma.
                // 'true' per mantenere la posizione globale del player durante il parenting.
                currentPlayerCC.transform.SetParent(platformTransform, true);
                
                // Stampa un messaggio nella console per confermare che il player è stato parentato.
                //Debug.Log($"Player {currentPlayerCC.name} parentato a {platformTransform.name} (via OnTriggerEnter).");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            // Recupera il CharacterController per assicurarti di deparentare il player corretto
            CharacterController exitingPlayerCC = other.GetComponent<CharacterController>();
            if (exitingPlayerCC == null)
            {
                exitingPlayerCC = other.GetComponentInParent<CharacterController>();
            }

            if (exitingPlayerCC != null && exitingPlayerCC.transform.parent == platformTransform)
            {
                // Deparenta il player, mantenendo la sua posizione globale.
                exitingPlayerCC.transform.SetParent(null, true);
                Debug.Log($"Player {exitingPlayerCC.name} deparentato da {platformTransform.name} (via OnTriggerExit).");
            }
            // Resetta currentPlayerCC se è il player che sta uscendo.
            if (exitingPlayerCC == currentPlayerCC)
            {
                currentPlayerCC = null;
            }
        }
    }
}