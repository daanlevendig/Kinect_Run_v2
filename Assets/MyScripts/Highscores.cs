using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscores : MonoBehaviour 
{
	public GameObject finish, topTen, scoreObj, hsOverlay, screen;

	public HUD hud;

	public Button hsButton;

	public Renderer scoreRend;

	public Text hsText;

	public Finish fScript;

	public int[] highScore;

	public int tempScore;

	public bool hsDone;

	// Use this for initialization
	void Start () 
	{
		hud = GameObject.FindGameObjectWithTag("Player").GetComponent<HUD>();
		topTen = GameObject.FindGameObjectWithTag("HighScore");
		hsText = topTen.GetComponent<Text>();
		finish = GameObject.FindGameObjectWithTag("Finish");
		fScript = finish.GetComponent<Finish>();
		scoreObj = GameObject.FindGameObjectWithTag("TopTen");
		hsOverlay = GameObject.Find("HSimage");
		screen = GameObject.FindGameObjectWithTag("ScreenOverlay");
		hsButton = GameObject.Find("HSButton").GetComponent<Button>();

		tempScore = 0;

		hsDone = false;

		highScore = new int[10];
		highScore[0] = 50000;
		highScore[1] = 25000;
		highScore[2] = 10000;
		highScore[3] = 5000;
		highScore[4] = 2500;
		highScore[5] = 1250;
		highScore[6] = 1000;
		highScore[7] = 500;
		highScore[8] = 125;
		highScore[9] = 50;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Debug.Log (string.Format ("{0}\n\n{1}\n\n{2}\n\n{3}\n\n{4}\n\n{5}\n\n{6}\n\n{7}\n\n{8}\n\n{9}", 
		                          highScore[0], highScore[1], highScore[2], highScore[3], highScore[4], highScore[5], highScore[6], highScore[7], highScore[8], highScore[9]));

		if (!fScript.calcDone)
			hsButton.interactable = false;
		else
		{
			hsText.text = string.Format ("{0}\n\n{1}\n\n{2}\n\n{3}\n\n{4}\n\n{5}\n\n{6}\n\n{7}\n\n{8}\n\n{9}", 
				highScore[0], highScore[1], highScore[2], highScore[3], highScore[4], highScore[5], highScore[6], highScore[7], highScore[8], highScore[9]);
		}

//		Debug.Log (fScript.calcDone);

		if (fScript.calcDone && !hsDone)
		{
			tempScore = (int)fScript.points;
			AddScoreToList();
			hsButton.interactable = true;
		}

		if (!hud.finished)
			hsOverlay.SetActive(false);

		DontDestroyOnLoad(gameObject);
	}

	void AddScoreToList()
	{
		for (int i = 9; i > 0; i--)
		{
			if (tempScore <= highScore[i])
			{
				if (i <= 8)
				{
					highScore[i+1] = tempScore;
					hsDone = true;
					return;
				}
				else
					break;
			}
			else
			{
				highScore[i] = highScore[i-1];
			}
		}
	}

	public void ShowHS()
	{
		screen.SetActive(false);
		hsOverlay.SetActive(true);
	}
}
