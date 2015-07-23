using UnityEngine;
using System.Collections;

public class Run : MonoBehaviour 
{
	public enum Moving {Stopped, Jogging, Sprinting};
	public int isMoving;

	public Movement movement;

	public float leftLegAngle, lastLeftAngle, leftAngleDif;
	public float rightLegAngle, lastRightAngle, rightAngleDif;
	public float leftKneeY, lastLeftKneeY, leftKneeDif;
	public float rightKneeY, lastRightKneeY, rightKneeDif;
	public float bottomSpineY;
	public float lastBottom;
	public float runSpeed;

	public bool isRunning;

	private double timestampLastMoved;

	// Use this for initialization
	void Start () 
	{
		runSpeed = 0.0f;
		isMoving = (int)Moving.Stopped;

		movement = GetComponent<Movement>();

		leftLegAngle = 180f;
		rightLegAngle = 180f;

		isRunning = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Using Knee Height
		leftKneeY = movement.leftKnee.y;
		rightKneeY = movement.rightKnee.y;

		Running();

		lastLeftKneeY = movement.leftKnee.y;
		lastRightKneeY = movement.rightKnee.y;

		// Using Knee Angles
//		leftLegAngle = movement.leftLegAngle;
//		rightLegAngle = movement.rightLegAngle;
//
//		Running2();
//
//		lastLeftAngle = movement.leftLegAngle;
//		lastRightAngle = movement.rightLegAngle;
	}

	void Running()
	{
		leftKneeDif = (Mathf.Abs (leftKneeY - lastLeftKneeY));
		rightKneeDif = (Mathf.Abs(rightKneeY - lastRightKneeY));

		if ((leftKneeDif >= 0.2f) || (rightKneeDif >= 0.2f))
		{
			isMoving = (int)Moving.Sprinting;
			timestampLastMoved = getTimestamp();
		} 
		else if ((leftKneeDif >= 0.01f) || (rightKneeDif >= 0.01f))
		{
			isMoving = (int)Moving.Jogging;
			timestampLastMoved = getTimestamp();
		} 
		else if (getTimestamp() - timestampLastMoved > 1)
		{
			isMoving = (int)Moving.Stopped;
		}

		switch(isMoving)
		{
		case 0:
			if (runSpeed > 0.0f)
				runSpeed -= 0.02f;
			else
				runSpeed = 0.0f;
			break;
		case 1:
			if (runSpeed < 0.1f)
				runSpeed += 0.01f;
			else
				runSpeed = 0.1f;
			break;
		case 2:
			if (runSpeed < 0.2f)
				runSpeed += 0.015f;
			else
				runSpeed = 0.02f;
			break;
		default:
			Debug.Log ("error: isMoving 0x04011988-D44N");
			break;
		}
	}

	void Running2()
	{
		leftAngleDif = (Mathf.Abs (leftLegAngle - lastLeftAngle));
		rightAngleDif = (Mathf.Abs(rightLegAngle - lastRightAngle));
		
		if ((leftAngleDif >= 20.0f) || (rightAngleDif >= 20.0f))
		{
			isMoving = (int)Moving.Sprinting;
			timestampLastMoved = getTimestamp();
		} 
		else if ((leftAngleDif >= 10.0f) || (rightAngleDif >= 10.0f))
		{
			isMoving = (int)Moving.Jogging;
			timestampLastMoved = getTimestamp();
		} 
		else if (getTimestamp() - timestampLastMoved > 1)
		{
			isMoving = (int)Moving.Stopped;
		}
		
		switch(isMoving)
		{
		case 0:
			if (runSpeed > 0.0f)
				runSpeed -= 0.02f;
			else
				runSpeed = 0.0f;
			break;
		case 1:
			if (runSpeed < 0.1f)
				runSpeed += 0.01f;
			else
				runSpeed = 0.1f;
			break;
		case 2:
			if (runSpeed < 0.2f)
				runSpeed += 0.015f;
			else
				runSpeed = 0.02f;
			break;
		default:
			Debug.Log ("error: isMoving 0x04011988-D44N");
			break;
		}
	}

//	void Running()
//	{		
//		if ((leftLegAngle < 120f) || (rightLegAngle < 120f))
//		{
//			isMoving = (int)Moving.Running;
//			timestampLastMoved = getTimestamp();
//		}
//		
//		if ((leftLegAngle >= 120f) && (rightLegAngle >= 120f) && (getTimestamp() - timestampLastMoved > 1))
//		{
//			isMoving = (int)Moving.Stopped;
//		}
//		
//		switch(isMoving)
//		{
//		case 0:
//			runSpeed = 0.0f;
//			break;
//		case 1:
//			runSpeed = 0.15f;
//			break;
//		default:
//			Debug.Log ("error: isMoving 0x04011988-D44N");
//			break;
//		}
//	}

	double getTimestamp() 
	{
		var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		var timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

		return timestamp;
	}
}



