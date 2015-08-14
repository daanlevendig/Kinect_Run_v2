using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour 
{
	public Movement movement;
	public Squat squat;
	public GameObject values;
	public StoredValues stored;

	public float playerHeight;
	public float bottomDif;
	public float floorHeight;
	public float maxJumpHeight;
	public float jumpSpeed;
	public float fallSpeed;
	public float yBottom;
	public float squatThreshold;
	public float jumpThreshold;
	public float lastBottom;
	public float lowestFoot;

	public bool isJumping;
	public bool reachedJumpTop;

	// Use this for initialization
	void Start () 
	{	
		movement = gameObject.GetComponent<Movement>();
		squat = gameObject.GetComponent<Squat>();
		values = GameObject.FindGameObjectWithTag("Values");
		stored = values.GetComponent<StoredValues>();

		playerHeight = 1.0f;
		bottomDif = 0.0f;
		floorHeight = 1.0f;
		maxJumpHeight = 3.5f;
		jumpSpeed = 0.25f;
		fallSpeed = 0.175f;
		jumpThreshold = 2.0f;
		lowestFoot = stored.lowestFoot;

		isJumping = false;
		reachedJumpTop = false;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		fallSpeed = (movement.combinedSpeed / 2.0f);
		// Distance temps for delta calculation
		if (transform.position.z > 1.0f)
		{
			bottomDif = movement.bottomSpine.y - lastBottom;
		}
		else if (transform.position.z <= 1.0f)
		{
			yBottom = movement.stored.yBottom;
			squatThreshold = (yBottom - movement.lowestFoot) * 0.85f;
			jumpThreshold = yBottom * 1.05f;
		}

		VerticalMovement();

		if (isJumping)
		{
			Dojump();
		}

		lastBottom = movement.bottomSpine.y;
	}

	void VerticalMovement()
	{
		// if player is going up fast: jump
		if ((bottomDif >= 0.05f) && (movement.bottomSpine.y > (jumpThreshold)) && ((movement.rightFoot.y - movement.leftFoot.y) < 0.1f))
		{
			isJumping = true;
		}
		
		// check if the player can go down again
		if (playerHeight >= maxJumpHeight)
		{
			reachedJumpTop = true;
		}
		
		// make sure player doesn't fall through the ground
		// and resets jump booleans
		if (playerHeight < floorHeight)
		{
			playerHeight = floorHeight;
			isJumping = false;
			reachedJumpTop = false;
		}
	}

	// makes player go up and down
	void Dojump()
	{
		if (!reachedJumpTop)
			playerHeight += jumpSpeed;
		
		if (reachedJumpTop)
			playerHeight -= fallSpeed;
	}
}
