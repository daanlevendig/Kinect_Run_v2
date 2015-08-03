﻿using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour 
{	
	public GameObject player;

	public Vector3 randomDirection;

	public float spinSpeed;

	public Movement movement;
	public HUD hud;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");

		movement = player.GetComponent<Movement>();
		hud = player.GetComponent<HUD>();

		randomDirection = new Vector3(0,0, Random.Range(-180f, 180f));
		transform.Rotate(randomDirection);

		spinSpeed = 100.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Spin ();
		CollideWithPlayer();
	}

	void Spin()
	{
		transform.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
	}

	void CollideWithPlayer()
	{
		if ((Mathf.Abs(player.transform.position.x - transform.position.x) < 0.9f)
	    && (Mathf.Abs(player.transform.position.y - transform.position.y) < 0.9f)
	    && (Mathf.Abs(player.transform.position.z - transform.position.z) < 0.9f))
		{
			hud.points += 10.0000f;
			gameObject.SetActive(false);
		}
	}
}
