using UnityEngine;
using System.Collections;

public class HideHelp : MonoBehaviour 
{
	public Help help;

	// Use this for initialization
	void Start () 
	{
		help = GameObject.Find("HelpButton").GetComponent<Help>();
	}

	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Hide()
	{
		help.helpbool = false;
	}
}
