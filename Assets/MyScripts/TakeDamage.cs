﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TakeDamage : MonoBehaviour 
{
	public MeshRenderer rend;

	public Text score;
	public Text timer;

	public float realSeconds;
	public int seconds, minutes, secondsHS;
	public int points;

	public string clock, highScore;
	public bool finished;

	// Use this for initialization
	void Start () 
	{
		points = 0;

		rend = gameObject.GetComponent<MeshRenderer>();

		score = GameObject.Find ("Score").GetComponent<Text>();
		timer = GameObject.Find ("Timer").GetComponent<Text>();
		
		finished = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (points <= 0)
			points = 0;

		score.text = string.Format ("{0}",points);

		Timer ();
	}

	public void VisualHit()
	{
		StartCoroutine(Flash());
	}

	IEnumerator Flash()
	{
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
	}

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
