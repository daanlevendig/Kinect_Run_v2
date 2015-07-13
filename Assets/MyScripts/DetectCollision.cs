using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DetectCollision : MonoBehaviour 
{
	public GameObject[] obstacles;

	public Movement movement;

	public bool isBelowObstacle;

	// Use this for initialization
	void Start () 
	{
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
	}
	
	// Update is called once per frame
	void Update () 
	{
		isBelowObstacle = false;

		// Functions 
		CheckInFront();
		CheckBelow();
		CheckAbove();
		CheckLeftSide ();
		CheckRightSide ();

//		Debug.Log (string.Format("floorheight{0}", movement.floorHeight);
//		Debug.Log ("left " + leftObstacle);
//		Debug.Log ("right " + rightObstacle);
	}

	// front obstacle collision detection
	void CheckInFront()
	{
		foreach (GameObject obstacle in obstacles)
		{
			// if there's no more room between player and obstacle, stop moving forward
			if ((Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.25f) 
			&& ((movement.playerHeight < (obstacle.transform.position.y + 1.5f)) && (movement.playerHeight > (obstacle.transform.position.y - 1.5f)))
			&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) < 5.5f))
			{
				//Debug.Log (string.Format ("{0}", Mathf.Abs (obstacle.transform.position.x - transform.position.x)));
				movement.moveSpeed = 0.0f;
				break;
			}
			else 
			{
				// reset moveSpeed if there's room to move again
				movement.moveSpeed = 0.25f;
				//movement.moveSpeed = 0.5f;
			}
		}
	}

	// below obstacle collision detection
	void CheckBelow()
	{
		foreach (GameObject obstacle in obstacles)
		{
			// if not even near an obstacle: do nothing or reset fallSpeed
			if (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) >= 1.25f)
			{
				movement.fallSpeed = 0.1f;
				movement.jumpSpeed = 0.25f;
				isBelowObstacle = false;
			}
			else 
			{
				// if on top of an obstacle stop falling
				if (((obstacle.transform.position.y - movement.playerHeight) <= 1.6f) 
				&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.0f) 
				&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) < 5.5f))
				{
					//Debug.Log("yes");
					movement.fallSpeed = 0.0f;
					break;
				} 
				// if above an obstacle keep falling
				else if (((obstacle.transform.position.y - movement.playerHeight) > 1.6f)
				&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.0f))
				{
					movement.fallSpeed = 0.1f;
				}
			}
		}
	}

	// Check for objects above the player
	void CheckAbove()
	{		
		foreach (GameObject obstacle in obstacles)
		{
			if ((obstacle.transform.position.y < 2.0f) 
			|| (movement.playerHeight > movement.floorHeight))
			{
				break;
			}
			else if ((obstacle.transform.position.y == 2.0f) 
			&& (movement.playerHeight == movement.floorHeight))
			{
				if (movement.isCrouching
				&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.25f)
				&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) < 5.5f))
				{
					isBelowObstacle = true;
					movement.moveSpeed = 0.25f;
					movement.jumpSpeed = 0.0f;
				}
				else if (!movement.isCrouching 
				&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.0f)
				&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) < 5.5f))
				{
					isBelowObstacle = true;
					movement.moveSpeed = 0.0f;
					movement.jumpSpeed = 0.0f;
				}
			}
		}
	}
	

	// left side obstacle collision detection
	void CheckLeftSide()
	{		
		foreach (GameObject obstacle in obstacles)
		{				
			// if obstacle is on the left
			if ((obstacle.transform.position.x < 0.0f)
			&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) >= 5.5f)
			&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) <= 1.1f))
			{
				//Debug.Log ("left <-");
				movement.leftBoundry = ((obstacle.transform.position.x + 5.5f)/movement.moveSideways);
				break;
			}
			else 
			{
				//Debug.Log ("not left");
				movement.leftBoundry = -0.3f;
			}
		}
	}

	// right side obstacle collision detection
	void CheckRightSide()
	{
		foreach (GameObject obstacle in obstacles)
		{
			// if obstacle is on the right
			if ((obstacle.transform.position.x > 0.0f)
			&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) >= 5.5f) 
			&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) <= 1.1f))
			{
				//Debug.Log("right ->");
				movement.rightBoundry = ((obstacle.transform.position.x - 5.5f)/movement.moveSideways);
				break;
			}
			else 
			{
				//Debug.Log ("not right");
				movement.rightBoundry = 0.3f;
			}
		}
	}
}
