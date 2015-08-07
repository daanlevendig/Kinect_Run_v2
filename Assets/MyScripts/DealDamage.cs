using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour 
{
	public GameObject player;
	public Movement movement;
	public FlashPlayer flash;
	public Squat squat;
	public Jump jump;
	public HUD hud;
	public Bounds bounds;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
		flash = player.GetComponent<FlashPlayer>();
		squat = player.GetComponent<Squat>();
		jump = player.GetComponent<Jump>();
		hud = player.GetComponent<HUD>();
		bounds = new Bounds(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z)); 
	}
	 	
	// Update is called once per frame
	void FixedUpdate () 
	{
		CollideWithPlayer();
	}

	void CollideWithPlayer()
	{		
		bool isGate;

		if (transform.position.y > 1.5f)
			isGate = true;
		else 
			isGate = false;

		if (!isGate || !squat.isSquatting)
		{
			if ((Mathf.Abs (transform.position.z - movement.moveForward) < (bounds.size.z/2 + 0.5f))
		    && (jump.playerHeight <= (transform.position.y + (bounds.size.y/2 + 1.0f)))
		    && (jump.playerHeight >= (transform.position.y - (bounds.size.y/2 + 1.0f)))
		    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < (bounds.size.x/2f + 0.5f)))
			{
				hud.points -= 25.0000f;
				if (!squat.isSquatting)
				{
					flash.VisualHit();
					Destroy (this);
				}
				else
				{
					squat.isSquatting = false;
					flash.VisualHit();
					Destroy (this);
				}
			}
		}
	}
}
