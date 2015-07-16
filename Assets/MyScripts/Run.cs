using UnityEngine;
using System.Collections;

public class Run : MonoBehaviour 
{
	public enum Moving {Stopped, Running};
	public int isMoving;

	public Movement movement;
	public DetectCollision detectCollision;

	public float leftLegAngle;
	public float rightLegAngle;
	public float runSpeed;

	private double timestampLastMoved;

	// Use this for initialization
	void Start () 
	{
		runSpeed = 0.0f;
		isMoving = (int)Moving.Stopped;

		leftLegAngle = 180f;
		rightLegAngle = 180f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		leftLegAngle = movement.leftLegAngle;
		rightLegAngle = movement.rightLegAngle;

		if ((leftLegAngle < 120f) || (rightLegAngle < 120f))
		{
//			Debug.Log ("Yes");
			isMoving = (int)Moving.Running;

			timestampLastMoved = getTimestamp();
		}
		else if ((leftLegAngle >= 120f) && (rightLegAngle >= 120f) && (getTimestamp() - timestampLastMoved > 1))
		{
//			Debug.Log("No");
			//StartCoroutine(WaitForNextKnee());
			isMoving = (int)Moving.Stopped;
		}

		switch(isMoving)
		{
		case 0:
//			Debug.Log ("case 0");
			runSpeed = 0.0f;
			break;
		case 1:
//			Debug.Log ("case 1");
			runSpeed = 0.15f;
			break;
		default:
			break;
		}
	}

	double getTimestamp() {
		var epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		var timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;

		return timestamp;
	}
		
	IEnumerator WaitForNextKnee()
	{
		yield return new WaitForSeconds(1);
		if ((leftLegAngle < 120f) || (rightLegAngle < 120f))
		{
			isMoving = (int)Moving.Running;
		}
		else if ((leftLegAngle >= 120f) && (rightLegAngle >= 120f))
		{
			isMoving = (int)Moving.Stopped;
		}
	}
}



