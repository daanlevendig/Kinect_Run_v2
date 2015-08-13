﻿using UnityEngine;
using System.Collections;

public class ScreenOverlay : MonoBehaviour 
{
	public GameObject waitingForPlayer;
	
	public Pause pause;
	
	public GameObject pauseScreen;

	// Use this for initialization
	void Start () 
	{
		waitingForPlayer = GameObject.FindGameObjectWithTag("OutOfSight");
		pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
		pause = GameObject.Find("PauseButton").GetComponent<Pause>();
	}
}
