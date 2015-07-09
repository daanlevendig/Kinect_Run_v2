using UnityEngine;
using System.Collections;

public class LeftWallCollision : MonoBehaviour 
{
	private GameObject player;
	private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerMovement = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log(string.Format ("difje xL: {0}", Mathf.Abs(player.transform.position.x - transform.position.x)));
		Debug.Log(string.Format ("difje zL: {0}", Mathf.Abs(player.transform.position.z - transform.position.z)));

		DepthCollision();
	}

	void DepthCollision()
	{
		if ((Mathf.Abs(this.transform.position.z - player.transform.position.z) <= 1.41f) && (player.transform.position.y <= 2.5f))
			playerMovement.moveSpeed = 0.0f;
		else if  ((Mathf.Abs(this.transform.position.z - player.transform.position.z) <= 1.41f) && (player.transform.position.y > 1.0f))
			playerMovement.moveSpeed = 0.1f;
		
		if ((Mathf.Abs(this.transform.position.z - player.transform.position.z) >= 1.41f) && (player.transform.position.y <= 1.0f))
			playerMovement.moveSpeed = 0.1f;
//
//		if ((Mathf.Abs(this.transform.position.z - player.transform.position.z) <= 1.0f) && (Mathf.Abs(player.transform.position.x - transform.position.x) < 3.0f))
//			playerMovement.moveSpeed = 0.0f;
//		
//		if ((Mathf.Abs(transform.position.z - player.transform.position.z) <= 1.0f) && (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > 3.0f))
//			playerMovement.moveSpeed = 0.1f;
	}
}
