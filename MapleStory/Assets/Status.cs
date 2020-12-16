using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Status : MonoBehaviour
{
    public int maxHP = 100;
    public int maxMP = 100;
    public int currentHP = 0;
    public int currentMP = 0;

    public int eXPToGive = 100;
    public int eXPGained = 0;

    public event EventHandler mySubscribers;


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    public void AddEXP(int toAdd)
    {
        eXPGained += toAdd;

    }
    public void AddCurrentHP(int toAdd)
    {
        currentHP += toAdd;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void SubCurrentHP(int toSub)
    {
        currentHP -= toSub;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
