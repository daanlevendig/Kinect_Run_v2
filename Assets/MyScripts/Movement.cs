using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Movement : MonoBehaviour 
{
	//public Text feedback;

	private Vector3 bottomSpine;
	
	public KinectManager manager;

	public DetectCollision detectCollision;

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
	public float bottomDif;
	public float lastBottom;
	public float xBottom;
	public float maxJumpHeight;
	public float jumpSpeed;
	public float fallSpeed;
	public float floorHeight;

	public bool isJumping;
	public bool reachedJumpTop;


	// Use this for initialization
	void Start () 
	{
		//feedback = GameObject.Find ("Feedback").GetComponent<Text>();

		rightBoundry = 0.3f;
		leftBoundry = -0.3f;
		moveSideways = 7.5f;

		moveForward = 0.0f;
		moveSpeed = 0.5f;

		playerHeight = 0.5f;
		bottomDif = 0.0f;
		maxJumpHeight = 2.0f;
		jumpSpeed = 0.2f;
		fallSpeed = 0.15f;
		floorHeight = 0.5f;

		isJumping = false;
		reachedJumpTop = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// setup kinect ------------
		manager = KinectManager.Instance;
		
		long userID = manager ? manager.GetUserIdByIndex (0) : 0;
		
		if (userID == 0) return;
		
		bottomSpine = manager.GetJointPosition (userID, 0);
		// -------------------------


//		Debug.Log ("jumping: " + isJumping);
//		Debug.Log ("top reached: " + isJumping);
//		Debug.Log (string.Format ("height: {0}", playerHeight));


		if (Input.GetKeyDown(KeyCode.Space))
			isJumping = true;

		//feedback.text = string.Format(" bottomSpine.x: {0} \n left boundry: {1} \n right boundry: {2} \n jumping?: {3} \n reached top? {4}",
		//                                bottomSpine.x, leftBoundry, rightBoundry, isJumping, reachedJumpTop);

		xBottom = bottomSpine.x;

		// Distance temps for delta calculation
		if (transform.position.z > 1.0f)
			bottomDif = bottomSpine.y - lastBottom;

		if (isJumping)
			Jump();

		// Vertical Movement
		VerticalMovement();

		// Horizontal movement
		HorizontalMovement();
		
		// Forward movement
		MoveForward();

		lastBottom = bottomSpine.y;
		//Debug.Log(string.Format ("leftB: {0}, rightB: {1}", leftBoundry, rightBoundry));
	}

	void VerticalMovement()
	{
		// If player is going up fast: jump
		if (bottomDif >= 0.05f)
			isJumping = true;

		// check if the player can go down again
		if (playerHeight >= maxJumpHeight)
		{
			reachedJumpTop = true;
		}

		if (playerHeight < floorHeight)
		{
			playerHeight = floorHeight;
			isJumping = false;
			reachedJumpTop = false;
		}
	}

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

	void MoveForward()
	{
		moveForward += moveSpeed;
	}

	void Jump()
	{
		if (!reachedJumpTop)
			playerHeight += jumpSpeed;
		
		if (reachedJumpTop)
			playerHeight -= fallSpeed;
	}
}
