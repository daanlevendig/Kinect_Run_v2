using UnityEngine;
using System;
using System.Collections;

public class Run : MonoBehaviour 
{
	public double[] leftSteps;
	public double[] rightSteps;

	public Jump jump;
	public Movement movement;
	public HUD hud;

	public int steps, maxSteps, leftStepCount, rightStepCount;

	public float leftKneeY, lastLeftKneeY;
	public float rightKneeY, lastRightKneeY;
	public float bottomSpineY;
	public float runSpeed;
	public float runThreshold;

	private double timestampLastMoved;

	// Use this for initialization
	void Start () 
	{
		runSpeed = 0.0f;

		maxSteps = 30;
		leftSteps = new double[maxSteps];
		rightSteps = new double[maxSteps];

		jump = GetComponent<Jump>();
		movement = GetComponent<Movement>();
		hud = GetComponent<HUD>();

		runThreshold = movement.stored.yBottom - 0.25f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		bottomSpineY = movement.bottomSpine.y;

		// Using Knee Height
		leftKneeY = movement.leftKnee.y;
		rightKneeY = movement.rightKnee.y;

		LeftLegAdd();
		RightLegAdd();
		LeftLegRemove();
		RightLegRemove();
		Steps();
		Runspeed();

		lastLeftKneeY = leftKneeY;
		lastRightKneeY = rightKneeY;
	}

	void Runspeed()
	{
		runSpeed = (float)(steps/75.0);
	}

	void LeftLegAdd()
	{
		if ((lastLeftKneeY <= runThreshold) && (leftKneeY > runThreshold))
		{
			leftSteps[leftStepCount] = getTimestamp();
			leftStepCount++;
		}
	}

	void LeftLegRemove()
	{
		if (leftSteps[0] < (getTimestamp() - 5.0))
		{
			for(int i = 0; i < leftStepCount; i++) 
			{
				if (i == leftStepCount-1)
					leftStepCount--;
				else
					leftSteps[i] = leftSteps[i+1]; 
			}
		}
	}

	void RightLegAdd()
	{
		if ((lastRightKneeY <= runThreshold) && (rightKneeY > runThreshold))
		{
			rightSteps[rightStepCount] = getTimestamp();
			rightStepCount++;
		}
	}

	void RightLegRemove()
	{
		if (rightSteps[0] < (getTimestamp() - 5.0))
		{
			for(int i = 0; i < rightStepCount; i++) 
			{
				if (i == rightStepCount-1)
					rightStepCount--;
				else
					rightSteps[i] = rightSteps[i+1];
			}
		}
	}

	void Steps()
	{
		steps = leftStepCount + rightStepCount;
	}

	double getTimestamp() 
	{
		var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		var timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

		return timestamp;
	}
}



