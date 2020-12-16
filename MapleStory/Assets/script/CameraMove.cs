using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMove : MonoBehaviour 
{
	public float smoothTimeX, smoothTimeY;
	public Vector2 velocity;
	public GameObject player;
	private Vector2 minPos, maxPos;
	public int mapNum = 0;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}


	void FixedUpdate () {
		if(player.transform.position.x < 15f && player.transform.position.x > -15f)
			mapNum = 0;
		if(player.transform.position.x < 41f && player.transform.position.x > 18.8f)
			mapNum = 1;
		if(player.transform.position.x < 75f && player.transform.position.x > 45f)
			mapNum = 2;
		if(player.transform.position.x < 103f && player.transform.position.x > 77f)
			mapNum = 3;

		if(mapNum == 0)
		{
			maxPos.x = 4.05f;
			minPos.x = -4.0f;
			minPos.y = 0;
			maxPos.y = 0;
		}
		else if(mapNum == 1)
		{
			maxPos.x = 30.0f;
			minPos.x = 29.96f;
			
			minPos.y = 0;
			maxPos.y = 1.4f;
		}
		else if(mapNum == 2)
		{
			maxPos.x = 63.6f;
			minPos.x = 56.4f;
			
			minPos.y = 0;
			maxPos.y = 1.3f;
		}
		else if(mapNum == 3)
		{
			minPos.x = 88.2f;
			maxPos.x = 91.8f;
			minPos.y = 0;
			maxPos.y = 5.0f;
		}

		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX, posY, transform.position.z);

		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minPos.x, maxPos.x),
		Mathf.Clamp (transform.position.y, minPos.y, maxPos.y),
		Mathf.Clamp (transform.position.z, transform.position.z, transform.position.z));

		

	}

}