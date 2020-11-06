﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAniControl : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("Move", false);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("Move", true);
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("Hit", true);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Hit", false);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Die", true);
        }
    }
}
