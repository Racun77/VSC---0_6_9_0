using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

/* Die Start()-Funktion ruft SetStats() auf, um die Waffenstatistiken zu initialisieren.
 * 
 * Die Update()-Funktion aktualisiert die Rotation des Halters, verringert den Spawn-Z�hler 
 * und instanziiert Feuerb�lle basierend auf dem Zeitintervall. 
 * Wenn statsUpdated true ist, werden die Waffenstatistiken erneut mit SetStats() aktualisiert.
 * 
 * Die SetStats()-Funktion setzt verschiedene Eigenschaften der Waffe basierend auf dem aktuellen Waffenlevel. 
 * Sie �ndert die Schadensmenge, Skalierung, Zeitintervall zwischen den Angriffen und die Lebensdauer des Damagers. 
 * Der Spawn-Z�hler wird auf 0 zur�ckgesetzt.
 * 
 * Die Bedingung if (statsUpdated == true) �berpr�ft, ob die Variable statsUpdated den Wert true hat.
 * Wenn dies der Fall ist, wird der darin enthaltene Codeblock ausgef�hrt. In diesem Fall wird statsUpdated auf 
 * false gesetzt.Dieses Muster wird oft verwendet, um eine Aktion auszuf�hren, wenn ein bestimmtes Ereignis eintritt
 * und dann den Zustand oder die Variable zur�ckzusetzen, um anzuzeigen, dass das Ereignis behandelt wurde.
 * Im Code wird statsUpdated auf false gesetzt, nachdem der Codeblock ausgef�hrt wurde, um anzuzeigen, 
 * dass die Waffenstatistiken aktualisiert wurden. 
 * Dadurch wird verhindert, dass der Codeblock bei den n�chsten Aktualisierungen erneut ausgef�hrt wird, 
 * es sei denn, statsUpdated wird erneut auf true gesetzt */

public class SpinWeapon : Weapon
{
    public float rotateSpeed; // Geschwindigkeit, mit der die Waffe rotiert
    public Transform holder, fireballToSpawn; // Referenzen auf den Halter und das Feuerball-Prefab
    public float timeBetweenSpawn; // Zeitintervall zwischen dem Erscheinen der Feuerb�lle
    private float spawnCounter; // Z�hler f�r das Zeitintervall
    public EnemyDamager damager;
    void Start()
    {
        SetStats(); //Setze die Waffenstatistiken zu Beginn

       // UIController.instance.levelUpButtons[0].UpgradeButtonDisplay(this);
    }

    void Update()
    {
        // Drehe den Halter entsprechend der angegebenen Geschwindigkeit
        // holder.transform.rotation = Quaternion.Euler(0f, 0f, holder.transform.rotation.eulerAngles.z - (rotateSpeed * Time.deltaTime));

        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));

        // Verringere den Z�hler basierend auf der vergangenen Zeit
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn; // Setze den Z�hler auf das Zeitintervall zur�ck

            // Instanziere ein neues Feuerball-Objekt an der Position und Rotation des Feuerball-Prefabs,
            // wobei der Halter als Elternobjekt angegeben wird
            //Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);

            for(int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;

                Instantiate(fireballToSpawn, fireballToSpawn.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
        }
        if (statsUpdated == true)
        {
            statsUpdated = false;

            SetStats(); // Setze die Waffenstatistiken neu
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage; // Setze die Schadensmenge des Damagers auf den Wert entsprechend des aktuellen Waffenlevels

        transform.localScale = Vector3.one * stats[weaponLevel].range; //Setze die Skalierung des Gameobjekts auf den Wert entsprechend des aktuellen Waffenlevels

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks; //Setze das Zeitintervall zwischen den Angriffen auf den Wert entsprechend des aktuellen Waffenlevels

        damager.lifeTime = stats[weaponLevel].duration; //Setze die Lebensdauer des Damagers auf den Wert entsprechend des aktuellen Waffenlevels

        spawnCounter = 0f; // Initialisiere den Z�hler f�r das Zeitintervall mit 0
    }
}
