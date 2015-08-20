using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscores : MonoBehaviour 
{
	public GameObject finish, topTen, scoreObj;

	public Renderer scoreRend;

	public Text HStext;

	public Finish fScript;

	public int[] highScore;

	public int tempScore;

	public bool levelLoadDone;

	// Use this for initialization
	void Start () 
	{
//		DontDestroyOnLoad(gameObject);

		topTen = GameObject.FindGameObjectWithTag("HighScore");
		HStext = topTen.GetComponent<Text>();
		finish = GameObject.FindGameObjectWithTag("Finish");
		fScript = finish.GetComponent<Finish>();
		scoreObj = GameObject.FindGameObjectWithTag("TopTen");
		scoreRend = scoreObj.GetComponent<Renderer>();
		
//		scoreObj.SetActive(false);

		levelLoadDone = false;

		tempScore = 0;

		highScore = new int[10];
		highScore[0] = 0;
		highScore[1] = 0;
		highScore[2] = 0;
		highScore[3] = 0;
		highScore[4] = 0;
		highScore[5] = 0;
		highScore[6] = 0;
		highScore[7] = 0;
		highScore[8] = 0;
		highScore[9] = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
//		Debug.Log (string.Format ("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7, {8}, {9}", 
//		                          highScore[0], highScore[1], highScore[2], highScore[3], highScore[4], highScore[5], highScore[6], highScore[7], highScore[8], highScore[9]));
		DontDestroyOnLoad(gameObject);
//
//		if (Application.loadedLevel != 8)
//			scoreRend.enabled = false;

		if (levelLoadDone)
		{
			scoreRend.enabled = true;
			Debug.Log ("hoi");
			HStext.text = string.Format ("{0}\n\n{1}\n\n{2}\n\n{3}\n\n{4}\n\n{5}\n\n{6}\n\n{7}\n\n{8}\n\n{9}", 
				highScore[0], highScore[1], highScore[2], highScore[3], highScore[4], highScore[5], highScore[6], highScore[7], highScore[8], highScore[9]);
		}
		else
			scoreRend.enabled = false;

		if (fScript.calcDone)
		{
			Debug.Log ("doei");
			tempScore = (int)fScript.points;
			AddScoreToList();
		}
	}

	void AddScoreToList()
	{
		for (int i = 9; i > 0; i--)
		{
			if (tempScore >= highScore[i])
				highScore[i] = highScore[i-1];
			else
			{
				highScore[i] = tempScore;
				StartCoroutine (Wait());
			}
		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2);
		levelLoadDone = true;
		Application.LoadLevel(8);
	}
}
