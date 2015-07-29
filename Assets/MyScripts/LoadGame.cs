using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		StartCoroutine(StartGame());
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(3);
		Application.LoadLevel(1);
	}
}
