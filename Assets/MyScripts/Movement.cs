using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour 
{
//	public Text feedback;

//	public GameObject waitingForPlayer;

	// Joints
	public Vector3 bottomSpine;
	public Vector3 bottomHead;
	public Vector3 leftHand;
	public Vector3 rightHand;
	public Vector3 leftShoulder;
	public Vector3 rightShoulder;
	public Vector3 leftHip;
	public Vector3 leftKnee;
	public Vector3 rightHip;
	public Vector3 rightKnee;
	public Vector3 leftLeg;
	public Vector3 rightLeg;
	public Vector3 body;
	public Vector3 hipUp;
	public float leftLegAngle;
	public float rightLegAngle;
	public float bodyAngle;
		
	public KinectManager manager;

	public Run run;
	public TakeDamage takeDamage;
	public HeadScript headScript;

	public GameObject[] balls;

	public GameObject head;

	// Left & Right
	public float leftBoundry;
	public float rightBoundry;
	public float moveSideways;
	public float xMove;

	// Forward
	public float moveForward;
	public float moveSpeed;
	public float combinedSpeed;

	// Up & Down
	public float playerHeight;
	public float fallSpeed;
	public float bottomDif;
	public float leftHandDif;
	public float rightHandDif;
	public float lastBottom;
	public float lastLeftHand;
	public float lastRightHand;
	public float xBottom;
	public float maxJumpHeight;
	public float jumpSpeed;
	public float floorHeight;
	public float yBottom;

	public bool reachedJumpTop;
	public bool isJumping;
	public bool isCrouching;
	public bool begin;

	public bool debugBool;

	// Use this for initialization
	void Start () 
	{
//		feedback = GameObject.Find ("Feedback").GetComponent<Text>();

//		waitingForPlayer = GameObject.FindGameObjectWithTag("OutOfSight");

		head = GameObject.Find("Player/Head");

		balls = GameObject.FindGameObjectsWithTag("Ball");

		takeDamage = gameObject.GetComponent<TakeDamage>();
		headScript = head.GetComponent<HeadScript>();
		run = GetComponent<Run>();

		rightBoundry = 0.6f;
		leftBoundry = -0.6f;
		moveSideways = 5.0f;

		moveForward = 0.0f;
		moveSpeed = 0.2f;
		combinedSpeed = 0.0f;

		// vertical normal vector for hip angle
		hipUp = new Vector3(0.0f, 1.0f, 0.0f);

		playerHeight = 0.5f;
		bottomDif = 0.0f;
		leftHandDif = 0.0f;
		rightHandDif = 0.0f;
		maxJumpHeight = 2.0f;
		jumpSpeed = 0.2f;
		fallSpeed = 0.1f;
		floorHeight = 0.5f;

		isCrouching = false;
		isJumping = false;
		reachedJumpTop = false;

		// set to true for feedback
//		debugBool = true;

//		waitingForPlayer.SetActive(false);
		begin = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// setup kinect
		manager = KinectManager.Instance;
		long userID = manager ? manager.GetUserIdByIndex (0) : 0;
		if (userID == 0)
		{
//			waitingForPlayer.SetActive(true);
			begin = false;
			return;
		}
//		waitingForPlayer.SetActive(false);
		begin = true;

		bottomSpine = manager.GetJointPosition (userID, 0);
		bottomHead = manager.GetJointPosition (userID, 2);
		leftHand = manager.GetJointPosition (userID, 6);
		rightHand = manager.GetJointPosition (userID, 10);
		leftShoulder = manager.GetJointPosition (userID, 4);
		rightShoulder = manager.GetJointPosition (userID, 8);
		leftHip = manager.GetJointPosition (userID, 12);
		leftKnee = manager.GetJointPosition (userID, 13);
		rightHip = manager.GetJointPosition (userID, 16);
		rightKnee = manager.GetJointPosition (userID, 17);

//		if (debugBool)
//		{
//			// Feeedback text in-game
////			feedback.text = string.Format(" movespeed: {0} \n all speed combined: {1} \n left dif: {2} \n right dif: {3} \n runspeed: {4} \n Body angle: {5}",
////			                              moveSpeed,        combinedSpeed,             run.leftKneeDif,   run.rightKneeDif, run.runSpeed,   bodyAngle);
//
//			// Console Debug
////			Debug.Log (string.Format ("L: {0}, R: {1}", run.leftKneeDif, run.rightKneeDif));
//
//			// Skeleton
//			manager.computeUserMap = true;
//		}
//		else
//			manager.computeUserMap = false;

		xBottom = bottomHead.x;

		// Distance temps for delta calculation
		if (transform.position.z > 0.5f)
		{
			bottomDif = bottomSpine.y - lastBottom;
			leftHandDif = leftHand.z - lastLeftHand;
			rightHandDif = rightHand.z - lastRightHand;
		}
		else if (transform.position.z <= 1.0f)
		{
			yBottom = bottomSpine.y;
		}

		if (isJumping && !isCrouching)
		{
			Jump();
		}

		Punch ();

		// Vertical Movement
		VerticalMovement();

		// Horizontal movement
		HorizontalMovement();

		if (!isJumping)
			Crouch ();
		
		// Forward movement
		MoveForward();

		// Calculate leg angles
		LegAngles();

		lastBottom = bottomSpine.y;
		lastLeftHand = leftHand.z;
		lastRightHand = rightHand.z;
	}

	void LegAngles()
	{
		leftLeg = leftKnee - leftHip;
		rightLeg = rightKnee - leftHip;
		body = bottomHead - bottomSpine;
		leftLegAngle = Vector3.Angle(hipUp, leftLeg);
		rightLegAngle = Vector3.Angle(hipUp, rightLeg);
		bodyAngle = Vector3.Angle(hipUp, body);
	}
	
	void VerticalMovement()
	{
		// if player is going up fast: jump
		if (bottomDif >= 0.05f)
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

	// function for movement in all axis' actually
	void HorizontalMovement()
	{
		if (xBottom <= rightBoundry && xBottom >= leftBoundry)
		{
			xMove = (xBottom * moveSideways);
			transform.position = new Vector3(xMove, playerHeight, moveForward);
		}
		else if (xBottom > rightBoundry)
		{
			transform.position = new Vector3((rightBoundry * moveSideways), playerHeight, moveForward);
		}
		else if (xBottom < leftBoundry)
		{
			transform.position = new Vector3((leftBoundry * moveSideways), playerHeight, moveForward);
		}
	}

	// constant movement in the z-axis
	void MoveForward()
	{
		if (!takeDamage.finished)
			combinedSpeed = moveSpeed + run.runSpeed;
		else
		{
			if (combinedSpeed > 0.0f)
				combinedSpeed -= 0.01f;
			else
				combinedSpeed = 0.0f;
		}

		moveForward += combinedSpeed;
	}

	// makes player go up and down
	void Jump()
	{
		if (!reachedJumpTop)
			playerHeight += jumpSpeed;
		
		if (reachedJumpTop)
			playerHeight -= fallSpeed;
	}

	void Crouch()
	{
		if ((bottomSpine.y < (yBottom - 0.25f)) && (bodyAngle <= 20.0f))
		{
			isCrouching = true;
			head.SetActive(false);
		}
		else if ((bottomSpine.y >= (yBottom - 0.25f)) || (bodyAngle > 20.0f))
		{
			isCrouching = false;
			head.SetActive(true);
		}
	}

	void Punch()
	{
		foreach (GameObject ball in balls)
		{
			HitBalls hit = ball.GetComponent<HitBalls>();

			if ((Mathf.Abs(ball.transform.position.z - moveForward) <= 3.5f) && !hit.ballPunch)
			{
				if (((leftHand.z < (leftShoulder.z - 0.3f)) && (leftHandDif < -0.05f)) 
				|| ((rightHand.z < (rightShoulder.z - 0.3f)) && (rightHandDif < -0.05f)))
				{
					hit.ballPunch = true;
				}

				if ((Mathf.Abs(ball.transform.position.z - moveForward) <= 1.0f) && !hit.ballPunch)
				{
					takeDamage.points -= (10.0000f/((float)(balls.Length)));
					hit.VisualHit();
					takeDamage.VisualHit();
					if (!isCrouching)
						headScript.VisualHit();
				}
			}
		}
	}
}
