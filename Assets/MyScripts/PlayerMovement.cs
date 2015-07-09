using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public Text feedback;
	
	public float moveSpeed;
	public float playerHeight;
	public float lastPlayerY;
	public float leftBoundry;
	public float rightBoundry;
	public float floorHeight;

	public bool movementDown;
	public bool isJumping;


//-----------------------------------------------------------------------

	private float moveForward;
	private float jumpSpeed;
	private float lastBottom;
	private float maxJumpHeight;
	private float bottomDif;

	private bool reachedJumpTop;

	private KinectManager manager;
	private Vector3 bottomSpine;
	private Vector3 leftKnee;
	private Vector3 rightKnee;

	private CollisionScript obstacle;


	// Use this for initialization
	void Start ()
	{
		feedback = GameObject.Find ("Feedback").GetComponent<Text>();

		obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<CollisionScript>();

		rightBoundry = 0.3f;
		leftBoundry = -0.3f;
		floorHeight = 0.5f;

		moveSpeed = 0.1f;
		moveForward = 0.0f;
		playerHeight = 0.5f;
		jumpSpeed = 4.0f;
		maxJumpHeight = 2.0f;
		bottomDif = 0.000f;

		isJumping = false;
		reachedJumpTop = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		manager = KinectManager.Instance;

		long userID = manager ? manager.GetUserIdByIndex (0) : 0;

		if (userID == 0) return;

		bottomSpine = manager.GetJointPosition (userID, 0);
		leftKnee = manager.GetJointPosition (userID, 13);
		rightKnee = manager.GetJointPosition (userID, 17);


		// Keyboard input for testing
		if (Input.GetKeyDown(KeyCode.Space)) Jump ();


		feedback.text = string.Format("bottomSpine.x: {0}\n leftKnee.y: {1}\n rightKnee.y: {2}\n player.y: {3}\nY-Dif: {4}", 
		                              bottomSpine.x, leftKnee.y, rightKnee.y, transform.position.y, (bottomSpine.y - lastBottom));

		// Vertical movement
		VerticalMovement();

		// check if isJumping to avoid double jump
		if (isJumping)
			Jump();

		// check is the player can go down again
		if (playerHeight >= maxJumpHeight)
			reachedJumpTop = true;

		// Horizontal movement
		HorizontalMovement();

		// Forward movement
		MoveForward();

		// Distance temps for delta calculation
		if (transform.position.z > 1.0f)
			bottomDif = bottomSpine.y - lastBottom;

		if (obstacle.isAboveObstacle)
			floorHeight = 1.0f;
		else if (!obstacle.isAboveObstacle)
			floorHeight = 0.5f;


		Down ();

		lastBottom = bottomSpine.y;
		lastPlayerY = transform.position.y;
	}

	void MoveForward()
	{
		moveForward += moveSpeed;
	}

	void HorizontalMovement()
	{
		if (bottomSpine.x <= rightBoundry && bottomSpine.x >= leftBoundry)
			transform.position = new Vector3((bottomSpine.x * 7.5f), playerHeight, moveForward);
		else if (bottomSpine.x > rightBoundry)
			transform.position = new Vector3((rightBoundry * 7.5f), playerHeight, moveForward);
		else if (bottomSpine.x < leftBoundry)
			transform.position = new Vector3((leftBoundry * 7.5f), playerHeight, moveForward);
	}

	void VerticalMovement()
	{
		if (bottomDif >= 0.05f)
			isJumping = true;

		
		if (transform.position.y < floorHeight)
		{
			playerHeight = floorHeight;
			isJumping = false;
			reachedJumpTop = false;
		}
	}

	void Jump()
	{
		if (movementDown && obstacle.isAboveObstacle)
			floorHeight = 1.0f;

		if (movementDown && !obstacle.isAboveObstacle)
			floorHeight = 0.5f;

		if (isJumping && !reachedJumpTop)
			playerHeight += jumpSpeed * Time.deltaTime;

		if (reachedJumpTop && (transform.position.y > floorHeight))
			playerHeight -= jumpSpeed * Time.deltaTime;
	}

	void Down()
	{
		if (lastPlayerY - transform.position.y >= 0)
			movementDown = true;
		else if(lastPlayerY - transform.position.y < 0)
			movementDown = false;
	}
}











