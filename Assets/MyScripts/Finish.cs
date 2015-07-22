using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Finish : MonoBehaviour 
{
	public GameObject player;
	public TakeDamage takeDamage;
	
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		takeDamage = player.GetComponent<TakeDamage>();
	}

	void Update()
	{
		Finished();
	}

	void Finished()
	{
		if (player.transform.position.z > transform.position.z)
		{
			takeDamage.finished = true;
		}
	}
}
