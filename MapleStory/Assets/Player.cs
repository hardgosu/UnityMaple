using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public Camera cam;

    NavMeshAgent agent;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir =
            (cam.transform.up * Input.GetAxis("Vertical") + cam.transform.right * Input.GetAxis("Horizontal")).normalized;
        print(dir);
        agent.Move(new Vector3(dir.x, 0, dir.z) * Time.deltaTime * speed);

        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
    }
}
