using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour 
{
	public GameObject[] obstacles;
	public Movement movement;
	public TakeDamage takeDamage;
	public GameObject player;
	public Bounds bounds;

	// Use this for initialization
	void Start () 
	{
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
		takeDamage = player.GetComponent<TakeDamage>();
		bounds = new Bounds(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z)); 
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
				    && (movement.playerHeight < (transform.position.y + 1.5f))
				    && (movement.playerHeight >= (transform.position.y - 1.5f))
				    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < (bounds.size.x/2f + 0.5f)))
				{
					takeDamage.points -= (25.0000f/((float)(obstacles.Length)));
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
