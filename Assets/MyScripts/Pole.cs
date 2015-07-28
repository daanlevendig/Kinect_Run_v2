using UnityEngine;
using System.Collections;

public class Pole : MonoBehaviour 
{
	public GameObject[] poles;
	public Movement movement;
	public TakeDamage takeDamage;
	public GameObject player;
	public Bounds bounds;
	
	// Use this for initialization
	void Start () 
	{
		poles = GameObject.FindGameObjectsWithTag("Pole");
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
		foreach (GameObject pole in poles)
		{
			if ((Mathf.Abs (transform.position.z - movement.moveForward) < (bounds.size.z/2 + 0.5f))
			    && (movement.playerHeight < (transform.position.y + (bounds.size.y/2 + 0.5f)))
			    && (movement.playerHeight >= (transform.position.y - (bounds.size.y/2 + 0.5f)))
			    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < (bounds.size.x/2 + 0.5f)))
			{
				takeDamage.points -= (5.0000f/((float)(poles.Length)));
				Destroy (this);
			}
		}
	}
}