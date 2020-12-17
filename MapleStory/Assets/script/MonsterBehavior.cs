using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterBehavior : MonoBehaviour
{

    private float checkTime;
    private int beHaviour;
    private Animator animator;
    int moveDirection = 1;

    bool coroutineReady = true;



    Coroutine currentCoroutine = null;

    Coroutine currentCoroutine2 = null;

    bool alive = true;

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
        GetComponent<Status>().mySubscribers += YouAreDead;

        beHaviour = Random.Range(0, 3);

        checkTime = (Random.Range(0, 300) / 100);
    }

    private void YouAreDead(object sender, System.EventArgs e)
    {
        //throw new System.NotImplementedException();

        if(currentCoroutine !=null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;

            currentCoroutine2 = StartCoroutine(Die());
            alive = false;
        }


    }

    IEnumerator Idle()
    {


        coroutineReady = false;

        GetComponent<Animator>().PlayInFixedTime("idle", 0, 0);
        yield return new WaitForSeconds(0.167f * 6);
        currentCoroutine = null;
        coroutineReady = true;
    }
    IEnumerator Move()
    {

        coroutineReady = false;
        int randomValue = Random.Range(0, 2);
        if (randomValue == 1)
        {
            moveDirection = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            moveDirection = -1;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        checkTime = 1.25f;
        GetComponent<Animator>().PlayInFixedTime("move", 0, 0);


        float timer = 0;
        float timerLimit = (0.25f * 4);
        while (true)
        {
            timer += Time.deltaTime;
            transform.position = new Vector3(transform.position.x + Time.deltaTime * 1.0f * moveDirection, transform.position.y, transform.position.z);
            yield return null;


            if (timerLimit < timer)
            {
                break;
            }


        }
        currentCoroutine = null;
        coroutineReady = true;
    }
    IEnumerator Damaged()
    {

        coroutineReady = false;

        StartCoroutine(DamagedSpriteColorAnimation());

        GetComponent<Animator>().PlayInFixedTime("hit", 0, 0);
        float timer = 0;
        float timerLimit = (0.25f * 2);
        while (true)
        {
            timer += Time.deltaTime;
            transform.position = new Vector3(transform.position.x + Time.deltaTime * 1.0f * moveDirection, transform.position.y, transform.position.z);
            yield return null;


            if (timerLimit < timer)
            {
                break;
            }


        }
        currentCoroutine = null;
        coroutineReady = true;
    }
    IEnumerator DamagedSpriteColorAnimation()
    {

        float timer = 0;

        while(true)
        {
            timer += Time.deltaTime;
            yield return null;
            if(timer <= 0.1f)
            {
                Vector4 color = Vector4.zero;

                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = timer / 0.1f;

                GetComponent<SpriteRenderer>().color = color;
            }
            else if(timer <= 0.2f)
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = 1.0f - (timer / 0.2f);

                GetComponent<SpriteRenderer>().color = color;
            }
            else if (timer <= 0.3f)
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = timer / 0.3f;

                GetComponent<SpriteRenderer>().color = color;
            }
            else if (timer <= 0.4f)
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = 1.0f - (timer / 0.4f);

                GetComponent<SpriteRenderer>().color = color;
            }
            else
            {
                Vector4 color = Vector4.zero;
                color.x = GetComponent<SpriteRenderer>().color.r;
                color.y = GetComponent<SpriteRenderer>().color.g;
                color.z = GetComponent<SpriteRenderer>().color.b;
                color.w = 1.0f;
                GetComponent<SpriteRenderer>().color = color;
                break;
            }
        }

    }

    IEnumerator Die()
    {


        coroutineReady = false;


        animator.PlayInFixedTime("die", 0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length/ animator.GetCurrentAnimatorStateInfo(0).speed);
        currentCoroutine = null;
        //coroutineReady = true;
        GameObject.Find("Player").GetComponent<Status>().AddEXP(GetComponent<Status>().eXPToGive);

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!alive)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Attack"))
        {
            beHaviour = 2;
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }

        }

    }
    void Update()
    {
        if(!alive)
        {
            return;
        }

        if (coroutineReady)
        {

            beHaviour = Random.Range(0, 2);

            checkTime = (Random.Range(0, 300) / 100);
        }

        // IDLE, MOVE L, MOVE R 순서
        if (beHaviour == 0)
        {

            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(Move());
            }

        }
        else if (beHaviour == 1)
        {
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(Move());
            }

        }
        else if (beHaviour == 2)
        {
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(Damaged());
            }
        }

    }
}
