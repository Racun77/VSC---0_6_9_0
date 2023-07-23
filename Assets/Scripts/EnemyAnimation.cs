using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Transform sprite; // Referenz auf das Sprite des Gegners

    public float speed; // Geschwindigkeit der Skalierungsanimation

    public float minSize, maxSize; // Minimale und maximale Skalierungsgr��e

    private float activeSize; // Aktuelle Skalierungsgr��e

    void Start()
    {
        activeSize = maxSize; // Setze die aktuelle Skalierungsgr��e auf die maximale Gr��e

        speed = speed * Random.Range(.75f, 1.25f); // Passe die Geschwindigkeit basierend auf einem zuf�lligen Faktor an
    }

    void Update()
    {
        // Ver�ndere die Skalierung des Sprites allm�hlich zur aktuellen Gr��e hin
        sprite.transform.localScale = Vector3.MoveTowards(sprite.transform.localScale, Vector3.one * activeSize, speed * Time.deltaTime);

        // �berpr�fe, ob die Skalierung die aktuelle Gr��e erreicht hat
        if (sprite.transform.localScale.x == activeSize)
        {
            if (activeSize == maxSize) // Wenn die aktuelle Gr��e die maximale Gr��e ist
            {
                activeSize = minSize; // Setze die aktuelle Gr��e auf die minimale Gr��e
            }
            else
            {
                activeSize = maxSize; // Andernfalls setze die aktuelle Gr��e auf die maximale Gr��e
            }
        }
    }
}
