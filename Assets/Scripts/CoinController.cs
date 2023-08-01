using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance; // Beispiel, wie man Münzen hinzufügt, indem man auf das Singleton-Objekt zugreift
    // Start is called before the first frame update
    void Start()
    {
        instance = this; // Singleton - Instanz erstellen, um auf diese Klasse von anderen Skripten aus zugreifen zu können
    }
    public int currentCoins; // Die Anzahl der aktuellen Münzen
    public CoinPickup coin;

    public void AddCoins(int coinsToAdd)     // Diese Methode wird aufgerufen, um Münzen hinzuzufügen

    {
        currentCoins += coinsToAdd; //Hier wird der Wert der Variable currentCoins um den Wert der Variablen coinsToAdd erhöht.

        UIController.instance.UpgradeCoins();
    }
    public void DropCoin(Vector3 position, int value)
    {
        CoinPickup newCoin = Instantiate(coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);
        newCoin.coinAmount = value;
        newCoin.gameObject.SetActive(true);
    }
    public void SpendCoins(int coinsToSpend)
    {
        currentCoins -= coinsToSpend;

        UIController.instance.UpgradeCoins();
    }
}