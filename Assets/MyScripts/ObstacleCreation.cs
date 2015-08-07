using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ObstacleCreation : MonoBehaviour 
{
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
	
	public GameObject[] obstacleList;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
	}
}
