using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour
{
	public Movement movement;
	public HUD hud;
	public FlashPlayer flash;

	public GameObject[] balls;
	
	public float leftHandDif;
	public float rightHandDif;
	public float lastLeftHand;
	public float lastRightHand;

	// Use this for initialization
	void Start () 
	{
		balls = GameObject.FindGameObjectsWithTag("Ball");

		hud = GetComponent<HUD>();
		movement = GetComponent<Movement>();

		leftHandDif = 0.0f;
		rightHandDif = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Distance temps for delta calculation
		if (transform.position.z > 0.5f)
		{
			leftHandDif = movement.leftHand.z - lastLeftHand;
			rightHandDif = movement.rightHand.z - lastRightHand;
		}

		Dopunch();
		
		lastLeftHand = movement.leftHand.z;
		lastRightHand = movement.rightHand.z;
	}

	void Dopunch()
	{
		foreach (GameObject ball in balls)
		{
			HitBalls hit = ball.GetComponent<HitBalls>();
			
			if ((Mathf.Abs(ball.transform.position.z - movement.moveForward) <= 3.5f) && !hit.ballPunch)
			{
				if (((movement.leftHand.z < (movement.leftShoulder.z - 0.2f)) && (leftHandDif < -0.05f)) 
				|| ((movement.rightHand.z < (movement.rightShoulder.z - 0.2f)) && (rightHandDif < -0.05f)))
				{
					hit.ballPunch = true;
				}
				
				if ((Mathf.Abs(ball.transform.position.z - movement.moveForward) <= 1.0f) && !hit.ballPunch)
				{
					hud.points -= (10.0000f/((float)(balls.Length)));
					hit.VisualHit();
					flash.VisualHit();
				}
			}
		}
	}
}
