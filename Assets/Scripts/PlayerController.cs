using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Stale
    private float maxHealth = 2500.0f;
    // public float healthPoints = 2500.0f;
    public int goldPoints = 250;
    private int goldStart = 100;

    [Header("Useless fucking attachments")]
    [SerializeField]
    private Wallet playerMoney;

    [SerializeField] private Castle playerCastle;


    //Bialek
    //zmienne z visuala
    private VisualElement InterfaceRoot;
    public ProgressBar healthBar;
    public Label healthPrecentage;
    private Label goldAmount;
    
    
    private void OnEnable()
    {
        var uiDocument = GameObject.Find("MainUserInterface").GetComponent<UIDocument>();
        InterfaceRoot = uiDocument.rootVisualElement;
        healthBar = InterfaceRoot.Q<ProgressBar>("PlayerProgressBar");
        healthPrecentage = InterfaceRoot.Q<Label>("PlayerHealthText");
        goldAmount = InterfaceRoot.Q<Label>("GoldAmountText");
    }

    private void Update()
    {
        if (null == healthBar)
            return;
        if (null == healthPrecentage)
            return;
        if (null == goldAmount)
            return;

        healthBar.value = playerCastle.health;
        healthPrecentage.text = playerCastle.health.ToString("0");
        goldAmount.text = playerMoney.money.ToString();
    }

    // Health
    // public float GetHealthPoints()
    // {
    //     return healthPoints;
    // }
    // public void MinusHealthPoints(float hp)
    // {
    //     healthPoints -= hp;
    //     healthBar.value = healthPoints;
    // }
    // public void AddHealthPoints(float hp)
    // {
    //     healthPoints += hp;
    //     healthBar.value = healthPoints;
    // }

    // Gold
    public void AddGoldPoints(int gold)
    {
        goldPoints += gold;
        goldAmount.text = gold.ToString();
    }
    public void MinusGoldPoints(int gold)
    {
        goldPoints -= gold;
        goldAmount.text = gold.ToString();
    }
}