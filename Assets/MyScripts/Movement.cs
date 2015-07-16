using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public Text feedback;

	// Joints
	public Vector3 bottomSpine;
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
	public Vector3 hipUp;
	public float leftLegAngle;
	public float rightLegAngle;
		
	public KinectManager manager;

	public DetectCollision detectCollision;
	public Run run;

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

	// Use this for initialization
	void Start () 
	{
		feedback = GameObject.Find ("Feedback").GetComponent<Text>();

		head = GameObject.Find("Player/Head");

		balls = GameObject.FindGameObjectsWithTag("Ball");

		rightBoundry = 0.3f;
		leftBoundry = -0.3f;
//		moveSideways = 5.0f;
		moveSideways = 7.5f;

		moveForward = 0.0f;
		moveSpeed = 0.25f;
//		moveSpeed = 0.5f;
		combinedSpeed = 0.0f;
		hipUp = new Vector3(0.0f, 1.0f, 0.0f);

		playerHeight = 0.5f;
		bottomDif = 0.0f;
		leftHandDif = 0.0f;
		rightHandDif = 0.0f;
		maxJumpHeight = 2.0f;
		jumpSpeed = 0.25f;
		fallSpeed = 0.15f;
		floorHeight = 0.5f;

		isCrouching = false;
		isJumping = false;
		reachedJumpTop = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// setup kinect
		manager = KinectManager.Instance;
		long userID = manager ? manager.GetUserIdByIndex (0) : 0;
		if (userID == 0) return;
		bottomSpine = manager.GetJointPosition (userID, 0);
		leftHand = manager.GetJointPosition (userID, 6);
		rightHand = manager.GetJointPosition (userID, 10);
		leftShoulder = manager.GetJointPosition (userID, 4);
		rightShoulder = manager.GetJointPosition (userID, 8);
		leftHip = manager.GetJointPosition (userID, 12);
		leftKnee = manager.GetJointPosition (userID, 13);
		rightHip = manager.GetJointPosition (userID, 16);
		rightKnee = manager.GetJointPosition (userID, 17);

//		Debug.Log ("jumping: " + isJumping);
//		Debug.Log ("top reached: " + reachedJumpTop);
//		Debug.Log (string.Format ("height: {0}", playerHeight));
//
		feedback.text = string.Format(" movespeed: {0} \n knee in air?: {1} \n left angle: {2} \n right angle: {3} \n player height {4} \n is moving? {5} \n points: {6}",
		                                moveSpeed,        combinedSpeed,           run.leftLegAngle,      run.rightLegAngle,  playerHeight, run.isMoving, detectCollision.points);

		xBottom = bottomSpine.x;

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

		LegAngles();

		lastBottom = bottomSpine.y;
		lastLeftHand = leftHand.z;
		lastRightHand = rightHand.z;
	}

	void LegAngles()
	{
		leftLeg = leftKnee - leftHip;
		rightLeg = rightKnee - leftHip;
		leftLegAngle = Vector3.Angle(hipUp, leftLeg);
		rightLegAngle = Vector3.Angle(hipUp, rightLeg);
	}
	
	void VerticalMovement()
	{
		// if player is going up fast: jump
		if (bottomDif >= 0.1f)
		{
			isJumping = true;
		}

		if ((bottomSpine.y < (yBottom - 0.2f)))
		{
			isCrouching = true;
		}
		else 
		{
			isCrouching = false;
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
		combinedSpeed = moveSpeed + run.runSpeed;
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
		if ((bottomSpine.y < (yBottom - 0.25f)))
		{
			isCrouching = true;
			head.SetActive(false);
		}
		else if(!detectCollision.isBelowObstacle)
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

			if ((Mathf.Abs(ball.transform.position.z - moveForward) <= 1.0f) && !hit.ballPunch)
			{
				moveSpeed = 0.0f;

				if (((leftHand.z < (leftShoulder.z - 0.3f)) && (leftHandDif < -0.1f)) || ((rightHand.z < (rightShoulder.z - 0.3f)) && (rightHandDif < -0.1f)))
				{
					moveSpeed = 0.25f;
//					moveSpeed = 0.5f;
					hit.ballPunch = true;
					continue;
				}
			}

		}
	}
}
