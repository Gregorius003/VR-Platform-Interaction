using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularBoatMovement : MonoBehaviour
{
    public Vector3 centerPoint = Vector3.zero; // Il centro del cerchio attorno a cui la barca ruoterà
    public float radius = 5.0f;               // Il raggio del cerchio
    public float speed = 1.0f;                // La velocità di movimento angolare (gradi al secondo)
    public float startAngleOffset = 0.0f;     // Offset dell'angolo iniziale per variare il punto di partenza (in gradi)

    private float currentAngle;               // L'angolo corrente della barca sul cerchio

    void Start()
    {
        // Inizializza l'angolo corrente con l'offset per dare un punto di partenza diverso
        currentAngle = startAngleOffset;
    }

    void Update()
    {
        // Aggiorna l'angolo in base alla velocità e al tempo
        // Moltiplichiamo per Time.deltaTime per rendere il movimento indipendente dal framerate
        // e convertiamo la velocità da gradi/secondo a radianti/secondo per le funzioni trigonometriche
        currentAngle += speed * Time.deltaTime;

        // Calcola la posizione X e Z (o Y se il tuo cerchio è verticale) usando seno e coseno
        // Mathf.Deg2Rad converte i gradi in radianti
        float x = centerPoint.x + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float z = centerPoint.z + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        // Mantieni la stessa altezza Y della barca, o usa centerPoint.y se preferisci
        // Puoi anche aggiungere un piccolo offset per simulare il galleggiamento
        transform.position = new Vector3(x, transform.position.y, z);

        // Opzionale: Fai in modo che la barca guardi nella direzione di movimento
        // Questo fa sì che la prua della barca sia sempre rivolta verso il centro della curva
        // oppure puoi usare transform.LookAt(centerPoint) per farla guardare sempre il centro
        // oppure calcolare la direzione tangente per farla guardare avanti lungo il cerchio.
        // Per guardare avanti lungo il cerchio, la direzione è la tangente alla circonferenza.
        // Questo è un po' più complesso, ma realistico.
        // float targetAngle = currentAngle + 90.0f; // Aggiungi 90 gradi per puntare avanti lungo la circonferenza
        // Vector3 lookDirection = new Vector3(Mathf.Cos(targetAngle * Mathf.Deg2Rad), 0, Mathf.Sin(targetAngle * Mathf.Deg2Rad));
        // transform.rotation = Quaternion.LookRotation(lookDirection);

        // Un modo più semplice per farla guardare "avanti" lungo il cerchio
        // La direzione è la differenza tra la posizione attuale e la posizione precedente (o leggermente futura)
        // Oppure, semplicemente, fai in modo che la barca guardi il punto successivo del cerchio
        // Ma per un movimento circolare continuo, un semplice LookAt va bene.
        transform.LookAt(new Vector3(centerPoint.x, transform.position.y, centerPoint.z)); // La barca guarda verso il centro
    }
}
