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
	
	public void SetPaused()
	{
		movement.isPaused = false;
	}
}
