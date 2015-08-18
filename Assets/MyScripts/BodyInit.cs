using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Body init.
/// The experience of the gameplay depends on good initialization of the body
/// </summary>

public class BodyInit : MonoBehaviour
{
	public KinectManager manager;
	public GameObject values;
	public StoredValues stored;

	public Text initText, kneeAngles, countDown;

	public string noUser, straightKnees, inRange, groundedFeet;

	public Vector3 bottomSpine;
	public Vector3 bottomHead;
	public Vector3 leftHip;
	public Vector3 leftKnee;
	public Vector3 leftFoot;
	public Vector3 rightHip;
	public Vector3 rightKnee;
	public Vector3 rightFoot;
	public Vector3 leftUpperLeg, leftLowerLeg;
	public Vector3 rightUpperLeg, rightLowerLeg;

	public float leftLegAngle, rightLegAngle, leftUp, rightUp, lowestFoot, yBottom;

	public bool distanceBool, angleBool, groundedBool, coroutineStarted, delayBusy;

	// Use this for initialization
	void Start () 
	{
		initText = GameObject.Find ("InitText").GetComponent<Text>();
		kneeAngles = GameObject.Find ("KneeAngles").GetComponent<Text>();
		countDown = GameObject.Find ("CountDown").GetComponent<Text>();
		values = GameObject.FindGameObjectWithTag("Values");
		stored = values.GetComponent<StoredValues>();

		noUser = "";
		straightKnees = "";
		inRange = "";
		groundedFeet = "";
		countDown.text = "";

		distanceBool = false;
		angleBool = false;
		groundedBool = false;
		coroutineStarted = false;
		delayBusy = false;

		yBottom = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		initText.text = noUser + straightKnees + inRange + groundedFeet;

		manager = KinectManager.Instance;
		long userID = manager ? manager.GetUserIdByIndex (0) : 0;
		if (userID == 0) 
		{
			noUser = "No User Detected\n";
			return;
		}
		else
			noUser = "\n";

		// 
		bottomSpine = manager.GetJointPosition (userID, 0);
		bottomHead = manager.GetJointPosition (userID, 2);
		leftHip = manager.GetJointPosition (userID, 12);
		leftKnee = manager.GetJointPosition (userID, 13);
		leftFoot = manager.GetJointPosition (userID, 15);
		rightHip = manager.GetJointPosition (userID, 16);
		rightKnee = manager.GetJointPosition (userID, 17);
		rightFoot = manager.GetJointPosition (userID, 19);

		LegAngles();
		LowestFoot();

		// check distance from the sensor
		if (bottomSpine.z > 2.0f && !delayBusy)
		{
			inRange = "Too far!\n";
			distanceBool = false;
		}
		else if (bottomSpine.z < 1.25f && !delayBusy)
		{
			inRange = "Too close!\n";
			distanceBool = false;
		}
		else if (!delayBusy)
		{
			distanceBool = true;
			inRange = "\n";
		}

		// check for straight legs
		if (((leftLegAngle < 165.0f) || (rightLegAngle < 165.0f) || (leftUp < 155.0f) || (rightUp < 155.0f)) && !delayBusy)
		{
			straightKnees = "Stand up straight!\n";
			angleBool = false;
		}
		else if (!delayBusy)
		{
			angleBool = true;
			straightKnees = "\n";
		}

		// check for feet on ground
		if ((Mathf.Abs (leftFoot.y - rightFoot.y) > 0.05f) && !delayBusy)
		{
			groundedFeet = "Keep both feet grounded!\n";
			groundedBool = false;
		}
		else if (!delayBusy)
		{
			groundedBool = true;
			groundedFeet = "\n";
		}

		if (distanceBool && angleBool && groundedBool && !coroutineStarted)
		{
			coroutineStarted = true;
			StartCoroutine(InitCount());
		}
	}

	void LegAngles()
	{
		leftUpperLeg = leftKnee - leftHip;
		rightUpperLeg = rightKnee - rightHip;
		leftLowerLeg = leftKnee - leftFoot;
		rightLowerLeg = rightKnee - rightFoot;

		leftLegAngle = Vector3.Angle (leftUpperLeg, leftLowerLeg);
		rightLegAngle = Vector3.Angle (rightUpperLeg, rightLowerLeg);

		leftUp = Vector3.Angle (Vector3.up, -leftLowerLeg);
		rightUp = Vector3.Angle (Vector3.up, -rightLowerLeg);
	}

	void LowestFoot()
	{
		if (leftFoot.y < rightFoot.y)
			lowestFoot = leftFoot.y;
		else 
			lowestFoot = rightFoot.y;
	}

	IEnumerator InitCount()
	{
		countDown.text = "3";
		yield return new WaitForSeconds(1);
		countDown.text = "2";
		yield return new WaitForSeconds(1);
		countDown.text = "1";
		yield return new WaitForSeconds(1);
		countDown.text = "0";
		yield return new WaitForSeconds(1);
		countDown.text = "";
		if (distanceBool && angleBool && groundedBool)
		{
			yBottom = bottomSpine.y;
			stored.yBottom = yBottom;
			stored.lowestFoot = lowestFoot;
			Application.LoadLevel(2);
		}
		else
			coroutineStarted = false;
	}
}









