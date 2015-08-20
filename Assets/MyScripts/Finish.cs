using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Finish : MonoBehaviour 
{
	public Text score;

	public GameObject player, scoreScreen;
	public HUD hud;

	public float points;
	public int seconds, minutes, secondsHS;
	public string clock, endPoints;
	public bool calcDone;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		hud = player.GetComponent<HUD>();

		scoreScreen = GameObject.FindGameObjectWithTag("ScoreScreen");
		score = GameObject.FindGameObjectWithTag("EndScore").GetComponent<Text>();

		calcDone = false;
	}

	void FixedUpdate()
	{
		if (!hud.finished)
		{
			ConvertScore();
			scoreScreen.SetActive(false);
		}
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
		score.text = "Score:\n" + endPoints + "\n\nTime:\n" + clock;
		yield return new WaitForSeconds(2);
		scoreScreen.SetActive(true);
		yield return new WaitForSeconds(2);
		AddTimeToScore();
	}

	void AddTimeToScore()
	{
		for (int i = secondsHS; i > 0; i--)
		{
			points -= 10.0f;
			if (seconds > 0)
			{
				seconds--;
			}
			else if (seconds <= 0)
			{
				if (minutes > 0)
				{
					minutes--;
					seconds = 59;
				}
				else if (minutes <= 0)
				{
					seconds = 0;
					minutes = 0;
					calcDone = true;
				}
			}
//			secondsHS--;
			StartCoroutine(ScoreScreen());
			break;
		}
	}
	
	void ConvertScore()
	{
		points = hud.points;
		seconds = hud.seconds;
		minutes = hud.minutes;
		clock = hud.clock;
		secondsHS = hud.secondsHS;
		endPoints = string.Format ("{0}", (int)points);
	}
	
	IEnumerator ScoreScreen()
	{
		yield return new WaitForFixedUpdate();
		endPoints = string.Format ("{0}",(int)points);
		yield return new WaitForFixedUpdate();
		clock = (string.Format("{0}", minutes)) + ":" + (string.Format ("{0:00}", seconds));
		yield return new WaitForFixedUpdate();
		score.text = "Score:\n" + endPoints + "\n\nTime:\n" + clock;
	}
}
