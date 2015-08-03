using UnityEngine;
using System.Collections;

public class DisplayMaps : MonoBehaviour 
{
	public KinectManager manager;

	public int level;

	// Use this for initialization
	void Start () 
	{
		manager = GetComponent<KinectManager>();
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		manager.usersClrRect(500f, );
		Debug.Log (level);
		level = Application.loadedLevel;
		switch(level)
		{
		case 0:
			manager.computeColorMap = false;
			manager.displayColorMap = false;

			break;
		case 1:
			manager.computeColorMap = true;
			manager.displayColorMap = true;
			break;
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
			manager.computeColorMap = false;
			manager.displayColorMap = false;
			break;
		default:
			Debug.Log ("error");
			break;
		}
	}
}
