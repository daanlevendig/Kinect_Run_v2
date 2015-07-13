using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public Text feedback;

	public Vector3 bottomSpine;
	public Vector3 leftHand;
	public Vector3 rightHand;

	public KinectManager manager;

	public DetectCollision detectCollision;

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

	// ball stuff
	public float ballSpeed;

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
		//moveSpeed = 0.5f;

		playerHeight = 0.5f;
		bottomDif = 0.0f;
		leftHandDif = 0.0f;
		rightHandDif = 0.0f;
		maxJumpHeight = 2.0f;
		jumpSpeed = 0.25f;
		fallSpeed = 0.10f;
		floorHeight = 0.5f;

//		isCrouching = false;
		isJumping = false;
		reachedJumpTop = false;

		ballSpeed = 8.0f;
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

//		Debug.Log ("jumping: " + isJumping);
//		Debug.Log ("top reached: " + reachedJumpTop);
//		Debug.Log (string.Format ("height: {0}", playerHeight));

		if (Input.GetKeyDown(KeyCode.Space))
			isJumping = true;

		if (Input.GetKeyDown (KeyCode.LeftAlt))
			isCrouching = true;

		feedback.text = string.Format(" bottomSpine.z: {0} \n leftHand.z: {1} \n rightHand.z: {2}",
		                              bottomSpine.z, leftHand.z, rightHand.z);

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
//		Run();
		Crouch ();
		
		// Forward movement
		MoveForward();

		lastBottom = bottomSpine.y;
	}

	void VerticalMovement()
	{
		// if player is going up fast: jump
		if (bottomDif >= 0.05f)
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
		moveForward += moveSpeed;
	}

	// makes player go up and down
	void Jump()
	{
		if (!reachedJumpTop)
			playerHeight += jumpSpeed;
		
		if (reachedJumpTop)
			playerHeight -= fallSpeed;
	}

//	void Run()
//	{
//		if ()
//		{
//			0;
//		}
//	}

	void Crouch()
	{
		if ((bottomSpine.y < (yBottom - 0.2f)))
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
			if ((ball.transform.position.z - transform.position.z) > 1.5f)
			{
				break;
			}
			else if ((ball.transform.position.z - transform.position.z) <= 1.5f)
			{
				moveSpeed = 0.0f;
				if ((leftHand.z > (bottomSpine.z - 0.2f)) && (leftHandDif > 0.05f))
				{
					ball.transform.Translate((Vector3.up * ballSpeed * Time.deltaTime) + (Vector3.right * ballSpeed * Time.deltaTime));
					moveSpeed = 0.25f;
				}
				
				if ((rightHand.z > (bottomSpine.z - 0.2f)) && (rightHandDif > 0.05f))
				{
					ball.transform.Translate((Vector3.up * ballSpeed * Time.deltaTime) + (-Vector3.right * ballSpeed * Time.deltaTime));
					moveSpeed = 0.25f;
				}
			}
		}
	}
}
