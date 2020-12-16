using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum SimpleMonsterState
{

    Idle,
    Wander,
    Damaged,
    Die

}

public class Monster : MonoBehaviour
{

    Dictionary<SimpleMonsterState, Coroutine> coroutineDictionary = new Dictionary<SimpleMonsterState, Coroutine>();

    // Start is called before the first frame update
    void Start()
    {
        //coroutineDictionary[SimpleMonsterState.Idle] = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
