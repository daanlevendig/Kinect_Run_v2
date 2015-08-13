using UnityEngine;
using System.Collections;

public class Run : MonoBehaviour 
{
	public enum Moving { Stopped, Walking, Jogging, Running };
	public int isMoving;

	public Movement movement;
	public HUD hud;

	public int steps, maxSteps;

	public float leftLegAngle, lastLeftAngle, leftAngleDif;
	public float rightLegAngle, lastRightAngle, rightAngleDif;
	public float leftKneeY, lastLeftKneeY, leftKneeDif;
	public float rightKneeY, lastRightKneeY, rightKneeDif;
	public float bottomSpineY;
	public float lastBottom;
	public float runSpeed;

	double temp1, temp2, temp3;

	public bool isRunning;

	private double timestampLastMoved;

	// Use this for initialization
	void Start () 
	{
		runSpeed = 0.0f;
		isMoving = (int)Moving.Stopped;

		movement = GetComponent<Movement>();
		hud = GetComponent<HUD>();

		leftKneeDif = 0.0f;
		rightKneeDif = 0.0f;

		isRunning = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// Using Knee Height
		leftKneeY = movement.leftKnee.y;
		rightKneeY = movement.rightKnee.y;

		Running();

		// #TODO check what the extreme knee-differences per frame are
		if(leftKneeDif > 0f || rightKneeDif > 0f)
			Debug.Log(string.Format("left: {0}, right: {1}", leftKneeDif, rightKneeDif));

		if (hud.seconds == 10)
			temp1 = System.DateTime.UtcNow.Ticks;
		else if (hud.seconds == 12)
		{
			temp2 = System.DateTime.UtcNow.Ticks;
			temp3 = temp2 - temp1;
		}
		Debug.Log (temp3);

		lastLeftKneeY = movement.leftKnee.y;
		lastRightKneeY = movement.rightKnee.y;
	}

//	void RunningTest ()
//	{
//		timestampLastMoved = getTimestamp() - 2;
//
//		if ((leftKneeY > (bottomSpineY)) || (rightKneeY > (bottomSpineY)))
//
//
//
//
//		if (timestampLastMoved < (getTimestamp() - 2))
//
//		for (steps = 0; steps < maxSteps; steps++)
//		{
//		
//
//		}
//
//	}

	void Running ()
	{
		// check the y-difference each frame
		leftKneeDif = (Mathf.Abs (leftKneeY - lastLeftKneeY));
		rightKneeDif = (Mathf.Abs(rightKneeY - lastRightKneeY));

		// conditions for running: 
		// significant difference in y value = knees going up or down 
		// knees have to be above a certain threshold
		/*if (((leftKneeDif >= 0.05f) && (leftKneeY > (bottomSpineY - 0.05f))) 
		    || ((rightKneeDif >= 0.05f) && (rightKneeY > (bottomSpineY - 0.05f))))
		{
//			isMoving = (int)Moving.Running;
			timestampLastMoved = getTimestamp();
		} 
		else if (((leftKneeDif >= 0.05f) && (leftKneeY > (bottomSpineY - 0.10f))) 
		    || ((rightKneeDif >= 0.05f) && (rightKneeY > (bottomSpineY - 0.10f))))
		{
//			isMoving = (int)Moving.Jogging;
			timestampLastMoved = getTimestamp();
		} 
		else */if (((leftKneeDif >= 0.05f) && (leftKneeY > (bottomSpineY - 0.15f))) 
		|| ((rightKneeDif >= 0.05f) && (rightKneeY > (bottomSpineY - 0.15f))))
		{
//			isMoving = (int)Moving.Walking;
			timestampLastMoved = getTimestamp();
		} 
		else if (getTimestamp() - timestampLastMoved > 0.75)
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
			if (runSpeed < 0.15f)
				runSpeed += 0.01f;
			else
				runSpeed = 0.15f;
			break;
		case 2:
			if (runSpeed < 0.2f)
				runSpeed += 0.015f;
			else
				runSpeed = 0.2f;
			break;
		case 3:
			if (runSpeed < 0.25f)
				runSpeed += 0.02f;
			else
				runSpeed = 0.25f;
			break;
		default:
			Debug.Log ("error");
			break;
		}
	}

	double getTimestamp() 
	{
		var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		var timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

		return timestamp;
	}

//	void RepetitionCount ()
//	{
//		double[] stepCount;
//		// #TODO if step then add timestamp to count, if timestamp expires (~2 sec) subtract from count
//		// length of count adds speed up to maxspeed
//		if (bla)
//		{
//			steps++;
//		}
//	}
}



