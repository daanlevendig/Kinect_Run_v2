using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Finish : MonoBehaviour 
{
	public GameObject player;
	public HUD hud;

//	public GameObject 
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		hud = player.GetComponent<HUD>();
	}

	void Update()
	{
		Finished();
	}

	void Finished()
	{
		if (player.transform.position.z > transform.position.z)
		{
			hud.finished = true;
		}
	}
}
