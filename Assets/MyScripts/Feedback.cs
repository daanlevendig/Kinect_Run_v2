using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Feedback : MonoBehaviour 
{
	public Text feedback;

	public GameObject player;

	public Movement movement;
	public Run run;

	public bool debugOnOff;

	// Use this for initialization
	void Start () 
	{
		feedback = GameObject.Find("Feedback").GetComponent<Text>();

		player = GameObject.FindGameObjectWithTag("Player");

		movement = player.GetComponent<Movement>();
		run = player.GetComponent<Run>();

		debugOnOff = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (debugOnOff)
		{
			// Feedback text in-game
			feedback.text = string.Format(" movespeed: {0} \n all speed combined: {1} \n left dif: {2} \n right dif: {3} \n runspeed: {4} \n Body angle: {5}",
			                              movement.moveSpeed,        movement.combinedSpeed,             run.leftKneeDif,   run.rightKneeDif, run.runSpeed,   movement.bodyAngle);

			// Console Debug
//			Debug.Log (string.Format ("L: {0}, R: {1}", run.leftKneeDif, run.rightKneeDif));
		}
		else 
		{
			feedback.text = null;
		}
	}
}
