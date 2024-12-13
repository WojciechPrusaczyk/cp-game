using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPassiveIncome : MonoBehaviour
{
    public Wallet playerWallet;
    public Wallet enemyWallet;
    public int value;
    public int valueIncrease;
    public float Cooldown;
    public float currTimerCD;
    public float incomeInterval;
    public float currTimerIncome;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currTimerCD -= Time.deltaTime;
        currTimerIncome -= Time.deltaTime;
        if(currTimerCD < 0)
        {
            playerWallet.AddMoney(value);
            enemyWallet.AddMoney(value);
            currTimerCD = Cooldown;
        }

        if (currTimerIncome < 0)
        {
            value += valueIncrease;
            currTimerIncome = incomeInterval;
        }
    }
    
}
