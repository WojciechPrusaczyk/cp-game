using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Stale
    private float maxHealth = 2500.0f;
    public float healthPoints = 2500.0f;
    public int goldPoints = 250;
    private int goldStart = 100;
    
    
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
        if (null != healthBar)
        {
            healthBar.value = healthPoints;
        }

        if (null != healthPrecentage)
        {
            
            healthPrecentage.text = healthPoints.ToString("0.00") + "%";
        }

        if (null != goldAmount)
        {
            goldAmount.text = goldPoints.ToString();
        }
    }

    // Health
    public float GetHealthPoints()
    {
        return healthPoints;
    }
    public void MinusHealthPoints(float hp)
    {
        healthPoints -= hp;
        healthBar.value = healthPoints;
    }
    public void AddHealthPoints(float hp)
    {
        healthPoints += hp;
        healthBar.value = healthPoints;
    }

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