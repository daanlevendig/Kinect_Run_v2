using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// This is the main class for playermovement
// In this class the kinect manager and joints are set up
// Inherit from Movement to get joints
public class Movement : MonoBehaviour 
{	
	// Joints
	public Vector3 bottomSpine;
	public Vector3 bottomHead;
	public Vector3 leftHand;
	public Vector3 rightHand;
	public Vector3 leftShoulder;
	public Vector3 rightShoulder;
	public Vector3 leftHip;
	public Vector3 leftKnee;
	public Vector3 leftFoot;
	public Vector3 rightHip;
	public Vector3 rightKnee;
	public Vector3 rightFoot;
	public Vector3 body;
	public Vector3 hipUp;
	public float bodyAngle;
	public float lowestFoot;
	
	public KinectManager manager;
	public GameObject values;
	public StoredValues stored;

	public Squat squat;
	public Jump jump;
	public Run run;
	public HUD hud;
	public GameObject screen;
	public ScreenOverlay overlay;
	
	// Left & Right
	public float leftBoundry;
	public float rightBoundry;
	public float moveSideways;
	public float xMove;
	public float xChest;
	
	// Forward
	public float moveForward;
	public float moveSpeed;
	public float combinedSpeed;

	public bool begin;
	
	public bool isPaused;
	
	// Use this for initialization
	void Start () 
	{
		values = GameObject.FindGameObjectWithTag("Values");
		stored = values.GetComponent<StoredValues>();
		hud = gameObject.GetComponent<HUD>();
		run = GetComponent<Run>();
		squat = GetComponent<Squat>();
		jump = GetComponent<Jump>();
		screen = GameObject.FindGameObjectWithTag("ScreenOverlay");
		overlay = screen.GetComponent<ScreenOverlay>();
		
		rightBoundry = 0.3f;
		leftBoundry = -0.3f;
		moveSideways = 10.0f;
		
		moveForward = 0.0f;
		moveSpeed = 0.25f;
//		moveSpeed = 6.0f;
		combinedSpeed = 0.0f;
		
		// vertical normal vector for hip angle
		hipUp = new Vector3(0.0f, 1.0f, 0.0f);

		begin = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// show pause screen and return if isPaused
		if (isPaused)
		{
			overlay.waitingForPlayer.SetActive(false);
			overlay.pauseScreen.SetActive(true);
			begin = false;
			return;
		}
		overlay.pauseScreen.SetActive(false);
		
		// setup kinect & return if no user found
		manager = KinectManager.Instance;
		long userID = manager ? manager.GetUserIdByIndex (0) : 0;
		if (userID == 0)
		{
			overlay.waitingForPlayer.SetActive(true);
			begin = false;
			return;
		}
		begin = true;
		overlay.waitingForPlayer.SetActive(false);

		bottomSpine = manager.GetJointPosition (userID, 0);
		bottomHead = manager.GetJointPosition (userID, 2);
		leftShoulder = manager.GetJointPosition (userID, 4);
		leftHand = manager.GetJointPosition (userID, 6);
		rightShoulder = manager.GetJointPosition (userID, 8);
		rightHand = manager.GetJointPosition (userID, 10);
		leftHip = manager.GetJointPosition (userID, 12);
		leftKnee = manager.GetJointPosition (userID, 13);
		leftFoot = manager.GetJointPosition (userID, 15);
		rightHip = manager.GetJointPosition (userID, 16);
		rightKnee = manager.GetJointPosition (userID, 17);
		rightFoot = manager.GetJointPosition (userID, 19);
		
		xChest = bottomHead.x;


//		Debug.Log (string.Format ("Low Foot: {0}, Butt: {1}", lowestFoot, bottomSpine.y));


		// Horizontal movement
		HorizontalMovement();
		
		// Forward movement
		MoveForward();
		
		// Calculate leg angles
		CalcAngles();

		// Calculate lowest foot
		LowestFoot();
	}

	void LowestFoot()
	{
		if (leftFoot.y < rightFoot.y)
			lowestFoot = leftFoot.y;
		else 
			lowestFoot = rightFoot.y;
	}

	void CalcAngles()
	{
		body = bottomHead - bottomSpine;
		bodyAngle = Vector3.Angle(hipUp, body);
	}
	
	// function for movement in all axis' actually
	void HorizontalMovement()
	{
		if (xChest <= rightBoundry && xChest >= leftBoundry)
		{
			xMove = (xChest * moveSideways);
			transform.position = new Vector3(xMove, jump.playerHeight, moveForward);
		}
		else if (xChest > rightBoundry)
		{
			transform.position = new Vector3((rightBoundry * moveSideways), jump.playerHeight, moveForward);
		}
		else if (xChest < leftBoundry)
		{
			transform.position = new Vector3((leftBoundry * moveSideways), jump.playerHeight, moveForward);
		}
	}
	
	// constant movement in the z-axis
	void MoveForward()
	{
		if (!hud.finished)
		{
//			combinedSpeed = ((moveSpeed + run.runSpeed) * Time.fixedDeltaTime);
			if (!squat.isSquatting && !jump.isJumping)
				combinedSpeed = (moveSpeed + run.runSpeed);
			else
				combinedSpeed = moveSpeed /* Time.fixedDeltaTime*/;
		}
		else
		{
			if (combinedSpeed > 0.0f)
				combinedSpeed -= 0.01f;
			else
				combinedSpeed = 0.0f;
		}
		moveForward += combinedSpeed;
//		moveForward += combinedSpeed * Time.deltaTime;
	}
}
