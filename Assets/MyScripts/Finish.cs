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

//			StartCoroutine(HighScores());
		}
	}
	
//	IEnumerator HighScores()
//	{
//		yield return new WaitForSeconds(3);
//		Application.LoadLevel (3);
//		Destroy (GameObject.FindGameObjectWithTag ("MainCamera"));
//		Destroy (GameObject.FindGameObjectWithTag ("DeathPlane"));
//	}
}
