using UnityEngine;
using System.Collections;

public class CollisionScript : MonoBehaviour 
{
	private GameObject player;
	private PlayerMovement playerMovement;

	public bool isAboveObstacle;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerMovement = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		VerticalCollision();
		HorizontalCollision();
	}

	void HorizontalCollision()
	{
		if ((Mathf.Abs(transform.position.z - player.transform.position.z) <= 1.1f) && (player.transform.position.y <= 1.0f))
		{
			playerMovement.moveSpeed = 0.0f;
			//Debug.Log ("Hier gaat t goed");
		}
		else if  ((Mathf.Abs(transform.position.z - player.transform.position.z) <= 1.1f) && (player.transform.position.y > 1.0f))
			playerMovement.moveSpeed = 0.1f;

		if ((Mathf.Abs(transform.position.z - player.transform.position.z) >= 1.1f) && (player.transform.position.y <= 1.0f))
			playerMovement.moveSpeed = 0.1f;
	}

	void VerticalCollision()
	{
		if ((Mathf.Abs(this.transform.position.z - player.transform.position.z) <= 1.1f) && (player.transform.position.y >= 1.0f))
			isAboveObstacle = true;
		else if ((Mathf.Abs(this.transform.position.z - player.transform.position.z) > 1.1f) && (player.transform.position.y >= 1.0f))
			isAboveObstacle = false;
	}
}
