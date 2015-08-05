using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraStay : MonoBehaviour
{
	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{		
		// position of the camera relative to the player
		// follow on the x-axis
//		transform.position = new Vector3(player.transform.position.x, 8.0f, player.transform.position.z - 8.5f);
		// fixed x-axis
		transform.position = new Vector3(0.0f, 8.0f, player.transform.position.z - 8.5f);
	}
}