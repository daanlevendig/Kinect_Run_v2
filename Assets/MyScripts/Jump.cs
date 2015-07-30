using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour 
{
	public Movement movement;
	public Squat squat;

	public float playerHeight;
	public float bottomDif;
	public float floorHeight;
	public float maxJumpHeight;
	public float jumpSpeed;
	public float fallSpeed;
	public float yBottom;
	public float lastBottom;

	public bool isJumping;
	public bool reachedJumpTop;

	// Use this for initialization
	void Start () 
	{	
		movement = gameObject.GetComponent<Movement>();
		squat = gameObject.GetComponent<Squat>();

		playerHeight = 0.5f;
		bottomDif = 0.0f;
		floorHeight = 0.5f;
		maxJumpHeight = 2.5f;
		jumpSpeed = 0.2f;
		fallSpeed = 0.1f;

		isJumping = false;
		reachedJumpTop = false;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		// Distance temps for delta calculation
		if (transform.position.z > 1.0f)
		{
			bottomDif = movement.bottomSpine.y - lastBottom;
		}
		else if (transform.position.z <= 1.0f)
		{
			yBottom = movement.bottomSpine.y;
		}

		VerticalMovement();

		if (isJumping && !squat.isSquatting)
		{
			Dojump();
		}

		lastBottom = movement.bottomSpine.y;
	}

	void VerticalMovement()
	{
		// if player is going up fast: jump
		if ((bottomDif >= 0.05f) && (movement.bottomSpine.y > (yBottom - 0.1f)))
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
