using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraStay : MonoBehaviour
{
	private GameObject player;
	private float playerZUpdate;
//	private float playerXUpdate;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{		
		playerZUpdate = player.transform.position.z - 8.5f;
//		playerXUpdate = player.transform.position.x;

		// position of the camera relative to the player
		// follow on the x-axis
//		transform.position = new Vector3(playerXUpdate, 8.0f, playerZUpdate);
		// fixed x-axis
		transform.position = new Vector3(0.0f, 8.0f, playerZUpdate);
	}
}