using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private float timeSpan;
    private float checkTime;
    private int beHaviour;
    private Animator animator;
    private float yMove;

    bool choice = true;
    bool left = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Flip()
    {
        Vector3 _scale = transform.localScale;

        _scale.x *= -1;

        transform.localScale = _scale;
    }
    void Start()
    {
        beHaviour = Random.Range(0,3);
        timeSpan = 0.0f;
        checkTime = 2.0f;
    }

   
    void Update()
    {
        timeSpan += Time.deltaTime;
        if(timeSpan > checkTime)
        {
            beHaviour = Random.Range(0,3);
            timeSpan = 0.0f;
            choice = true;
        }

        // IDLE, MOVE L, MOVE R 순서
        if(beHaviour == 0)
        {
            animator.SetBool("Move", false);
        }
        else if(beHaviour == 1)
        {
            animator.SetBool("Move", true);
            if(!left)
            {
                Flip();
                left = true;
            }
            if(choice)
            {
                yMove = (((float)Random.Range(0,100) - 50)/1000);
                choice = false;
            }
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y + yMove, transform.position.z);
        }
        else if(beHaviour == 2)
        {
            animator.SetBool("Move", true);
             if(left)
            {
                Flip();
                left = false;
            }
            if(choice)
            {
                yMove = (((float)Random.Range(0,100) - 50)/1000);
                choice = false;
            }
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y + yMove, transform.position.z);
        }
        else if(beHaviour == 3)
        {
            //맞았을 때 할 행동 랜덤 밖의 행동임
        }
    }
}
