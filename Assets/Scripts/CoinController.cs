using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance; // Beispiel, wie man M�nzen hinzuf�gt, indem man auf das Singleton-Objekt zugreift
    // Start is called before the first frame update
    void Start()
    {
        instance = this; // Singleton - Instanz erstellen, um auf diese Klasse von anderen Skripten aus zugreifen zu k�nnen
    }
    public int currentCoins; // Die Anzahl der aktuellen M�nzen
    public CoinPickup coin;

    public void AddCoins(int coinsToAdd)     // Diese Methode wird aufgerufen, um M�nzen hinzuzuf�gen

    {
        currentCoins += coinsToAdd; //Hier wird der Wert der Variable currentCoins um den Wert der Variablen coinsToAdd erh�ht.

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