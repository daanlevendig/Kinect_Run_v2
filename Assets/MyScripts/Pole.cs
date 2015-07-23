using UnityEngine;
using System.Collections;

public class Pole : MonoBehaviour 
{
	public GameObject[] poles;
	public Movement movement;
	public TakeDamage takeDamage;
	public GameObject player;
	
	// Use this for initialization
	void Start () 
	{
		poles = GameObject.FindGameObjectsWithTag("Pole");
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
		foreach (GameObject pole in poles)
		{
			if ((Mathf.Abs (transform.position.z - movement.moveForward) < 1.0f)
			    && (movement.playerHeight < (transform.position.y + 1.0f))
			    && (movement.playerHeight >= (transform.position.y - 1.5f))
			    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < 1.0f))
			{
				takeDamage.points -= 5;
				Destroy (this);
			}
		}
	}
}