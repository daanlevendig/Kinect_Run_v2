using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour
{
	public Movement movement;
	public HUD hud;
	public FlashPlayer flash;

	public GameObject[] blocks;
	
	public float leftHandDif;
	public float rightHandDif;
	public float lastLeftHand;
	public float lastRightHand;

	// Use this for initialization
	void Start () 
	{
		blocks = GameObject.FindGameObjectsWithTag("Ball");

		flash = GetComponent<FlashPlayer>();
		hud = GetComponent<HUD>();
		movement = GetComponent<Movement>();

		leftHandDif = 0.0f;
		rightHandDif = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
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
		foreach (GameObject block in blocks)
		{
			HitBlocks hit = block.GetComponent<HitBlocks>();
			
			if ((Mathf.Abs(block.transform.position.z - movement.moveForward) <= 5.0f) && !hit.wallPunch)
			{
				if (((movement.leftHand.z < (movement.leftShoulder.z - 0.2f)) && (leftHandDif < -0.05f)) 
				|| ((movement.rightHand.z < (movement.rightShoulder.z - 0.2f)) && (rightHandDif < -0.05f)))
				{
					hit.wallPunch = true;
				}
				
				if ((Mathf.Abs(block.transform.position.z - movement.moveForward) <= 1.0f) && !hit.wallPunch)
				{
					hud.points -= (10.0000f/((float)(blocks.Length)));
					hit.VisualHit();
					flash.VisualHit();
				}
			}
		}
	}
}
