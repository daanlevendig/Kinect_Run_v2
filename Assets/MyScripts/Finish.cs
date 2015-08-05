using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Finish : MonoBehaviour 
{
	public Text score;

	public GameObject player, scoreScreen;
	public HUD hud;
	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		hud = player.GetComponent<HUD>();

		scoreScreen = GameObject.FindGameObjectWithTag("ScoreScreen");
		score = GameObject.FindGameObjectWithTag("EndScore").GetComponent<Text>();

	}

	void Update()
	{
		if (!hud.finished)
			scoreScreen.SetActive(false);
		else
			StartCoroutine(Screen());

		Finished();
	}

	void Finished()
	{
		if (player.transform.position.z > transform.position.z)
		{
			hud.finished = true;
		}
	}

	IEnumerator Screen()
	{
		score.text = "Score:\n" + hud.points.ToString() + "\n\nTime:\n" + hud.clock;
		yield return new WaitForSeconds(3);
		scoreScreen.SetActive(true);
	}
}
