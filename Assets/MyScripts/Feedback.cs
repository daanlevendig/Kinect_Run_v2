using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Feedback : MonoBehaviour 
{
	public Text feedback;

	public GameObject player;

	public Movement movement;
	public Run run;
	public Jump jump;

	// Use this for initialization
	void Start () 
	{
		feedback = GameObject.Find("Feedback").GetComponent<Text>();

		player = GameObject.FindGameObjectWithTag("Player");

		movement = player.GetComponent<Movement>();
		run = player.GetComponent<Run>();
		jump = player.GetComponent<Jump>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Feedback text in-game
		feedback.text = string.Format(" left dif: {0} \n right dif: {1} \n runspeed: {2}",
		                              run.leftKneeDif, run.rightKneeDif, run.runSpeed);

		// Console Debug
//		Debug.Log (string.Format ("L: {0}, R: {1}", run.leftKneeDif, run.rightKneeDif));
	}


}
