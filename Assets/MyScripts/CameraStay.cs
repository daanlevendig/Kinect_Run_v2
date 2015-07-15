using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraStay : MonoBehaviour
{
	private Movement movement;

	private GameObject player;

	private Vector3 targetPos;
	
	private float smoothTime = 0.5f;
	private float zVelocity = 15.0f;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// player position
		targetPos = new Vector3(0.0f, 8.0f, player.transform.position.z - 8.5f);
		
		float newPosition = Mathf.SmoothDamp(transform.position.z, targetPos.z, ref zVelocity, smoothTime);

		// position of the camera relative to the player
		transform.position = new Vector3(0.0f, 8.0f, newPosition);
	}
}