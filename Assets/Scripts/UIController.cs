using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public Slider explvlSlider;
    public TMP_Text expLvLText;

    public LevelUpSelectionButton[] levelUpButtons;

    public GameObject levelUpPanel;

    public TMP_Text coinText;
    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponsUpgradeDisplay;
    public TMP_Text timeText;
    public GameObject LevelEndScreen;
    public TMP_Text endTimeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateExperience(int currentExp, int levelExp, int currentLvL)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        expLvLText.text = "Level: " + currentLvL;
    }
    public void SkiplevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void UpgradeCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }
    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkiplevelUp();
    }
    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkiplevelUp();
    }
    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkiplevelUp();
    }
    public void PurchaseMaxWeapons()
    {
        PlayerStatController.instance.PurchaseMaxWeapons();
        SkiplevelUp();
    }
    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt( time / 60f);
        float seconds = Mathf.FloorToInt(time % 60);

        timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }
    public void GoToMainMenu()
    {

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
