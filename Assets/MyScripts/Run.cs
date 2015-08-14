using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Run : MonoBehaviour 
{
	public double[] leftSteps;
	public double[] rightSteps;

	public Jump jump;
	public Movement movement;
	public HUD hud;

	public Slider slider;
	public GameObject speedSlider;

	public int steps, maxSteps, leftStepCount, rightStepCount;

	public float leftKneeY, lastLeftKneeY;
	public float rightKneeY, lastRightKneeY;
	public float bottomSpineY;
	public float runSpeed, lastRunSpeed;
	public float runThreshold;
	public float vel;

	private double timestampLastMoved;

	// Use this for initialization
	void Start () 
	{
		runSpeed = 0.0f;
		lastRunSpeed = 0.0f;
		vel = 0.1f;

		maxSteps = 25;
		leftSteps = new double[maxSteps];
		rightSteps = new double[maxSteps];

		jump = GetComponent<Jump>();
		movement = GetComponent<Movement>();
		hud = GetComponent<HUD>();

		runThreshold = ((movement.stored.yBottom - movement.stored.lowestFoot) * 0.6f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		bottomSpineY = movement.bottomSpine.y;

		speedSlider = GameObject.FindGameObjectWithTag("RunSpeed");
		slider = speedSlider.GetComponent<Slider>();

		// Using Knee Height
		leftKneeY = movement.leftKnee.y;
		rightKneeY = movement.rightKnee.y;

		LeftLegAdd();
		RightLegAdd();
		LeftLegRemove();
		RightLegRemove();
		Steps();
		Runspeed();
		AdjustSlider();

		lastLeftKneeY = leftKneeY;
		lastRightKneeY = rightKneeY;
		lastRunSpeed = runSpeed;
	}

	void AdjustSlider()
	{
		slider.value = Mathf.SmoothDamp((lastRunSpeed / 0.333f),(runSpeed / 0.333f), ref vel, 0.1f);
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



