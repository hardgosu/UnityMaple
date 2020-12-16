using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public int JobNum =1;
    
    [HideInInspector]
    public Transform SetWarrior;
    public Transform SetWizard;
    
    private void Awake()
    {
        SetWarrior=transform.GetChild(0); 
        SetWizard=transform.GetChild(1);
        
    }

    public void warrior()
    { 
        SetWarrior.gameObject.SetActive(true);
        SetWizard.gameObject.SetActive(false);
        JobNum = 1;
    }
    public void wizard()
    {
        SetWarrior.gameObject.SetActive(false);
        SetWizard.gameObject.SetActive(true);
        JobNum = 2;
    }
}
