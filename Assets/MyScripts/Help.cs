using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour 
{
	public GameObject helpOverlay;

	public bool helpbool = false;

	// Use this for initialization
	void Start () 
	{
		helpOverlay = GameObject.FindGameObjectWithTag("HelpOverlay");
	}

	void Update()
	{
		if (helpbool)
			helpOverlay.SetActive(true);
		else if (!helpbool)
			helpOverlay.SetActive(false);
	}

	public void DisplayHelp()
	{
		helpbool = true;
	}

}