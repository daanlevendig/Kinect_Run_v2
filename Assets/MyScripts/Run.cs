using UnityEngine;
using System.Collections;

public class Run : MonoBehaviour 
{
	public enum Moving {Stopped, Running};
	public int isMoving;

	public Movement movement;

	public float leftLegAngle;
	public float rightLegAngle;
	public float leftKneeY, lastLeftKneeY;
	public float rightKneeY, lastRightKneeY;
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
		leftLegAngle = movement.leftLegAngle;
		rightLegAngle = movement.rightLegAngle;
		leftKneeY = movement.leftKnee.y;
		rightKneeY = movement.rightKnee.y;
		bottomSpineY = movement.bottomSpine.y;

		Running();

		lastLeftKneeY = movement.leftKnee.y;
		lastRightKneeY = movement.rightKnee.y;
		lastBottom = movement.bottomSpine.y;
	}

	void Running()
	{
//		if ((Mathf.Abs (movement.bottomSpine.y - lastBottom)) >= 0.001) 
		if (((Mathf.Abs (leftKneeY - lastLeftKneeY)) >= 0.05f) || ((Mathf.Abs(rightKneeY - lastRightKneeY)) > 0.05f))
		{
			isMoving = (int)Moving.Running;
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
				runSpeed -= 0.01f;
			else
				runSpeed = 0.0f;
			break;
		case 1:
			if (runSpeed < 0.15)
				runSpeed += 0.01f;
			else
				runSpeed = 0.15f;
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

	double getTimestamp() {
		var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		var timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

		return timestamp;
	}
}



