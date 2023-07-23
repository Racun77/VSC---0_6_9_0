using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB; // Referenz auf das Rigidbody2D-Komponente des Gegners
    public float moveSpeed; // Bewegungsgeschwindigkeit des Gegners
    private Transform target; // Referenz auf das Transform des Spielers
    public float damage; // Schaden, den der Gegner verursacht
    public float hitWaitTime = 1f; // Wartezeit zwischen den Angriffen
    private float hitCounter; // Z�hler f�r die Wartezeit zwischen den Angriffen
    public float health = 5f; // Gesundheit des Gegners
    public float knockBackTime = .5f; // Zeit, die der Gegner zur�ckschreckt, nachdem er getroffen wurde
    private float knockBackCounter; // Z�hler f�r die R�cksto�zeit
    public int expToGive = 1;

    void Start()
    {
        // Finde den Spieler-Controller und setze das Ziel auf dessen Transform
        target = PlayerHealthController.instance.transform;
    }

    void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f; // �ndere die Bewegungsgeschwindigkeit in die entgegengesetzte Richtung
            }
            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f); // Setze die Bewegungsgeschwindigkeit auf die H�lfte des urspr�nglichen Werts
            }
        }

        theRB.velocity = (target.position - transform.position).normalized * moveSpeed; // Bewege den Gegner in Richtung des Spielers

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime; // Verringere den Angriffs-Z�hler basierend auf vergangener Zeit
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage); // F�ge dem Spieler Schaden hinzu

            hitCounter = hitWaitTime; // Setze den Angriffs-Z�hler auf die Wartezeit
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake; // Verringere die Gesundheit des Gegners um den Schaden

        if (health <= 0)
        {
            Destroy(gameObject); // Zerst�re den Gegner, wenn die Gesundheit auf 0 oder darunter f�llt

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position); // Erzeuge eine Schadensnummer an der Position des Gegners
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake); // Rufe die TakeDamage-Methode auf, um Schaden zu nehmen

        if (shouldKnockback == true)
        {
            knockBackCounter = knockBackTime; // Setze den R�cksto�-Z�hler auf die R�cksto�zeit
        }
    }
}
