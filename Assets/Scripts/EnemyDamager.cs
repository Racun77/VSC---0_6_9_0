using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount; // Schaden, den dieser Schadensverursacher verursacht
    public float lifeTime, growSpeed = 5f; // Lebensdauer und Wachstumsgeschwindigkeit des Schadensverursachers
    private Vector3 targetSize; // Zielgr��e f�r das Wachstum des Schadensverursachers
    public bool shouldKnockBack; // Gibt an, ob ein R�cksto� angewendet werden soll
    public bool destroyParent;
    public bool damageOverTime;
    public float timeBetweenDamage;
    private float damageCounter;
    private List<EnemyController> enemiesInRange = new List<EnemyController>();

    public bool destroyOnImpact;

    void Start()
    {
        // Destroy(gameObject, lifeTime);
        targetSize = transform.localScale; // Setze die Zielgr��e auf die aktuelle Skalierung des Schadensverursachers
        transform.localScale = Vector3.zero; // Setze die Skalierung auf Null, um den Schadensverursacher zu verstecken
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime); // Ver�ndere die Skalierung allm�hlich zur Zielgr��e hin
        lifeTime -= Time.deltaTime; // Verringere die Lebensdauer basierend auf vergangener Zeit

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero; // Setze die Zielgr��e auf Null, um den Schadensverursacher schrumpfen zu lassen

            if (transform.localScale.x == 0f) // �berpr�fe, ob der Schadensverursacher vollst�ndig geschrumpft ist
            {
                Destroy(gameObject); // Zerst�re den Schadensverursacher

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
        if(damageOverTime == true)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                damageCounter = timeBetweenDamage;

                for( int i = 0; i < enemiesInRange.Count; i++) 
                {
                    if (enemiesInRange[i] != null)
                    {
                        enemiesInRange[i].TakeDamage(damageAmount, shouldKnockBack);
                    }
                    else
                    {
                        enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageOverTime == false)
        {
            if (collision.tag == "Enemy") // �berpr�fe, ob der Kollisionsgegenstand das Tag "Enemy" hat
            {
                collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack); // Verursache Schaden am Kollisionsgegenstand (Gegner)
                if (destroyOnImpact)
                {
                    Destroy(gameObject);
                }
             }
        }
        else
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Add(collision.GetComponent<EnemyController>());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(damageOverTime == true)
        {
            if(collision.tag == "Enemy")
            {
                enemiesInRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
