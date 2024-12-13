using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int money = 0;

    public int CheckMoney()
    {
        return money;
    }

    public void AddMoney(int value)
    {
        money += value;
    }
    
    public void SubtractMoney(int value)
    {
        money -= value;
    }
    
    public void SetMoney(int value)
    {
        money = value;
    }

}
