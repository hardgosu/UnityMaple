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
    public int aTK = 10;

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
        if (currentHP <= 0)
        {
            mySubscribers(this, null);

        }

        if(eXPGained >= 500)
        {
            eXPGained = Mathf.Clamp(eXPGained, 0, eXPGained - 500);
            aTK += 5;
            currentHP += 30;
            maxHP += 30;
        }
    }
}
