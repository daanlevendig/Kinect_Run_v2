using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Play : MonoBehaviour 
{
	public Movement movement;
	public GameObject player;
	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
	}
	
	void Update()
	{
		if (movement.isPaused)
		{
			if (Input.GetKeyDown(KeyCode.P))
				movement.isPaused = false;
		}
	}

	public void SetPaused()
	{
		movement.isPaused = false;
	}
}
