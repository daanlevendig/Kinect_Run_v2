using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour 
{
	public GameObject[] obstacles;
	public Movement movement;
	public TakeDamage takeDamage;
	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
		takeDamage = player.GetComponent<TakeDamage>();
	}

	// Update is called once per frame
	void Update () 
	{
		CollideWithPlayer();
	}

	void CollideWithPlayer()
	{		
		foreach (GameObject obstacle in obstacles)
		{
			bool isGate;

			if (transform.position.y > 1.5f)
				isGate = true;
			else 
				isGate = false;

			if (!isGate || !movement.isCrouching)
			{
				if ((Mathf.Abs (transform.position.z - movement.moveForward) < 1.0f)
				    && (movement.playerHeight < (transform.position.y + 1.0f))
				    && (movement.playerHeight >= (transform.position.y - 1.5f))
				    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < 5.5f))
				{
					takeDamage.points -= 10;
					if (!movement.isCrouching)
						Destroy (this);
					else
					{
						movement.isCrouching = false;
						Destroy (this);
					}
				}
			}
		}
	}
}
