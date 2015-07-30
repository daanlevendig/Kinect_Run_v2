﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour 
{
	public MeshRenderer rend, headRend;

	public Movement movement;

	public Text timer, score, endTime, endScore;

	public string clock, highScore;

	public float realSeconds, points;

	public int seconds, minutes, secondsHS;

	public bool finished;

	// Use this for initialization
	void Start () 
	{
		rend = GetComponent<MeshRenderer>();
		headRend = GetComponentInChildren<MeshRenderer>();

		movement = GetComponent<Movement>();

		timer = GameObject.Find ("Timer").GetComponent<Text>();
		score = GameObject.Find ("Score").GetComponent<Text>();

		points = 0.0f;

		finished = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (points <= 0.0f)
			points = 0.0f;
		
		score.text = string.Format ("{0}",(int)points);

		if (movement.begin && !finished)
			Timer ();
	}

//	public void VisualHit()
//	{
//		if (!movement.isPaused)
//			StartCoroutine(Flash());
//	}
	
//	IEnumerator Flash()
//	{
//		rend.enabled = false;
//		headRend.enabled = false;
//		yield return new WaitForSeconds(0.1f);
//		rend.enabled = true;
//		headRend.enabled = true;
//		yield return new WaitForSeconds(0.1f);
//		rend.enabled = false;
//		headRend.enabled = false;
//		yield return new WaitForSeconds(0.1f);
//		rend.enabled = true;
//		headRend.enabled = true;
//		yield return new WaitForSeconds(0.1f);
//		rend.enabled = false;
//		headRend.enabled = false;
//		yield return new WaitForSeconds(0.1f);
//		rend.enabled = true;
//		headRend.enabled = true;
//		yield return new WaitForSeconds(0.1f);
//		rend.enabled = false;
//		headRend.enabled = false;
//		yield return new WaitForSeconds(0.1f);
//		rend.enabled = true;
//		headRend.enabled = true;
//	}
	
	// Timer
	void Timer()
	{
		// get seconds from the clock
		realSeconds += Time.deltaTime;
		
		// add a second every time a 'real' second ends
		if (realSeconds > 1.00) 
		{
			seconds++;
			realSeconds = 0;
		}
		
		// add a minute every 60 seconds
		if (seconds > 59) 
		{
			seconds = 0;
			minutes += 1;
		}
		
		// write to the text object
		if (timer != null) 
		{
			clock = (string.Format("{0}", minutes)) + ":" + (string.Format ("{0:00}", seconds));
			timer.text = clock;
		}
		
		// store seconds in int for highscore purposes
		if (realSeconds > 1.00) 
		{
			secondsHS++;
			realSeconds = 0;
		}
		
		// store highscore in a string
		if (finished)
			highScore = clock;
	}
}
