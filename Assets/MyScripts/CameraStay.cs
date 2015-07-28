using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraStay : MonoBehaviour
{
	private GameObject player;
	private float playerPosUpdate;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{		
		playerPosUpdate = player.transform.position.z - 8.5f;

		// position of the camera relative to the player
		transform.position = new Vector3(0.0f, 8.0f, playerPosUpdate);
	}
}