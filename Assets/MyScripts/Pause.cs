using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour 
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
		if (!movement.isPaused)
		{
			if (Input.GetKeyDown(KeyCode.P))
				movement.isPaused = true;
		}
	}

	public void SetPaused()
	{
		movement.isPaused = true;
	}
}
