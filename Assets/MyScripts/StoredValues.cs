using UnityEngine;
using System.Collections;

public class StoredValues : MonoBehaviour
{
	public float yBottom, lowestFoot;

	// Use this for initialization
	void Start () 
	{
		yBottom = 0;
		lowestFoot = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		DontDestroyOnLoad(gameObject);
	}
}
