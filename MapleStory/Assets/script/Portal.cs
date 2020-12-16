using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    bool portalUse = false;
    public GameObject player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    private void Update()
    {     
        if(portalUse && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(gameObject.tag == "Portal1")
                player.transform.position = new Vector3(20.17f,  -3.3f, transform.position.z);
            if(gameObject.tag == "Portal2")
                player.transform.position = new Vector3(47.3f,  -2.6f, transform.position.z);
            if(gameObject.tag == "Portal3")
                player.transform.position = new Vector3(79.94f,  transform.position.y, transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            portalUse = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        portalUse = false;
    }
}
