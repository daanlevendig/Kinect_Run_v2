using UnityEngine;
using System.Collections;

public class Squat : MonoBehaviour 
{
	public GameObject head;
	public MeshRenderer headRend;

	public Movement movement;
	public Jump jump;
	public FlashPlayer flash;

	public bool isSquatting;

	// Use this for initialization
	void Start () 
	{
		head = GameObject.FindGameObjectWithTag("Head");
		headRend = head.GetComponent<MeshRenderer>();
		
		movement = GetComponent<Movement>();
		jump = GetComponent<Jump>();
		flash = GetComponent<FlashPlayer>();

		isSquatting = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Dosquat();
	}

	void Dosquat()
	{
		if ((movement.bottomSpine.y <= (jump.yBottom - 0.2f))/* && (movement.bodyAngle <= 20.0f)*/)
		{
			isSquatting = true;
			headRend.enabled = false;
		}
		else if (((movement.bottomSpine.y > (jump.yBottom - 0.2f)) /*|| (movement.bodyAngle > 20.0f)*/ && !flash.isColliding))
		{
			isSquatting = false;
			headRend.enabled = true;
		}
	}
}
