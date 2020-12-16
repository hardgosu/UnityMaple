using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Player player;
    NavMeshAgent navMeshAgent;
    EnemyState state;
    Coroutine lifeRoutine;


    public float sightLevel = 0.4f;

    private void OnEnable()
    {
        state = EnemyState.Idle;

        lifeRoutine = StartCoroutine(LifeRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(lifeRoutine);
    }

    IEnumerator LifeRoutine()
    {
        while(state != EnemyState.Dead)
        {
            if(state == EnemyState.Idle)
            {
                state = EnemyState.Search;
            }
            else if(state == EnemyState.Search)
            {

                var dot = Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized);

                if( dot > sightLevel)
                {
                    state = EnemyState.Chase;
                }


            }
            else if(state == EnemyState.Chase)
            {
                navMeshAgent.SetDestination(player.transform.position);
            }

            yield return null;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        navMeshAgent.SetDestination(player.transform.position);
    }
}
