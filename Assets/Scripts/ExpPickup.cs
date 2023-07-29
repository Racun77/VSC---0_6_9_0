using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ExpPickup : MonoBehaviour
{
    public int expValue; // Die Erfahrungspunkte, die der Spieler beim Einsammeln erhält.
    private bool movingToPlayer; // Gibt an, ob das Objekt sich zum Spieler bewegt.
    public float moveSpeed; // Die Geschwindigkeit, mit der das Objekt zum Spieler bewegt wird.


    public float timeBetweenChecks = .2f; // Zeitintervall zwischen den Überprüfungen, ob das Objekt sich zum Spieler bewegen sollte.
    private float checkCounter; // Zähler für die Zeitspanne zwischen den Überprüfungen.
    private PlayerController player; // Eine Referenz auf den PlayerController des Spielers.

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerController>(); // Den PlayerController des Spielers abrufen.
    }

    // Update is called once per frame
    void Update()
    {
        if(movingToPlayer == true) // Wenn das Objekt sich zum Spieler bewegt...
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime); // ...bewege das Objekt in Richtung der Position des Spielers mit der angegebenen Geschwindigkeit.
        }
        else // Wenn das Objekt sich nicht zum Spieler bewegt...
        {
            checkCounter -= Time.deltaTime; // ...verringere den checkCounter basierend auf der vergangenen Zeit.
            if (checkCounter <= 0) // Wenn die Zeit zwischen den Überprüfungen abgelaufen ist...
            {
                checkCounter = timeBetweenChecks; //...setze den Counter auf das Ausgangsintervall zurück.
                // Überprüfe, ob der Abstand zwischen dem Objekt und dem Spieler kleiner ist als die Pickup-Range des Spielers.
                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true; // Das Objekt beginnt sich zum Spieler zu bewegen.
                    moveSpeed += player.moveSpeed; // Die Bewegungsgeschwindigkeit des Objekts erhöht sich um die Bewegungsgeschwindigkeit des Spielers.
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // Wird aufgerufen, wenn das Objekt einen anderen Trigger-Collider berührt.
    {
        if(collision.tag == "Player") // Wenn der kollidierte Collider den Tag "Player" hat...
        {
            ExperienceLevelController.instance.GetExp(expValue); // ...Fordere die Erfahrungspunkte für den Spieler an.
            Destroy(gameObject); // ...Zerstöre das Objekt nach der Berührung mit dem Spieler.
        }
    }
}
