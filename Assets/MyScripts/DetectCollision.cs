using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DetectCollision : MonoBehaviour 
{
	public GameObject[] obstacles;
	//public GameObject obstacle;
	public Movement movement;

	public bool leftObstacle;
	public bool rightObstacle;

	// Use this for initialization
	void Start () 
	{
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

		leftObstacle = false;
		rightObstacle = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckInFront();
		CheckBelow();
		CheckLeftSide ();
		CheckRightSide ();

//		Debug.Log (string.Format("floorheight{0}", movement.floorHeight);
//		Debug.Log ("left " + leftObstacle);
//		Debug.Log ("right " + rightObstacle);
	}

//  foreach loop style
	void CheckInFront()
	{
		// obstacle collision detection
		foreach (GameObject obstacle in obstacles)
		{
			if ((Mathf.Abs (obstacle.transform.position.z - movement.moveForward) <= 1.2f) 
			&& (movement.playerHeight < (obstacle.transform.position.y + 1.5f)) 
			&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) < 5.5f))
			{
				movement.moveSpeed = 0.0f;
				break;
			}
			else 
				movement.moveSpeed = 0.2f;
		}
	}


	void CheckBelow()
	{
		foreach (GameObject obstacle in obstacles)
		{
			if (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) >= 1.2f)
			{
				movement.fallSpeed = 0.15f;
			}
			else 
			{
				if ((Mathf.Abs (obstacle.transform.position.y - movement.playerHeight) <= 1.6f) 
				&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.2f) 
				&& (Mathf.Abs (obstacle.transform.position.x - transform.position.x) < 5.5f))
				{
					//Debug.Log("yes");
					movement.fallSpeed = 0.0f;
					break;
				}
				else if ((Mathf.Abs (obstacle.transform.position.y - movement.playerHeight) > 1.6f)
				&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.2f))
				{
					movement.fallSpeed = 0.15f;
				}
				else if ((Mathf.Abs (obstacle.transform.position.y - movement.playerHeight) > 1.6f)
				&& (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) >= 1.2f))
				{
					movement.reachedJumpTop = true;
					movement.fallSpeed = 0.15f;
				}
			}
		}
	}

	void CheckLeftSide()
	{		foreach (GameObject obstacle in obstacles)
		{
			// if obstacle is on the left
			if ((obstacle.transform.position.x < 0.0f) && (Mathf.Abs (obstacle.transform.position.x - transform.position.x) >= 5.5f)
			    && (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.0f))
			{
				//Debug.Log ("left <-");
				movement.leftBoundry = (1f/15f);
				break;
			}
			else 
			{
				//Debug.Log (" not left");
				movement.leftBoundry = -0.3f;
			}
		}
	}

	void CheckRightSide()
	{
		foreach (GameObject obstacle in obstacles)
		{
			// if obstacle is on the right
			if ((obstacle.transform.position.x > 0.0f) && (Mathf.Abs (obstacle.transform.position.x - transform.position.x) >= 5.5f) 
			    && (Mathf.Abs (obstacle.transform.position.z - movement.moveForward) < 1.0f))
			{
				//Debug.Log("right ->");
				movement.rightBoundry = (-1f/15f);
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
