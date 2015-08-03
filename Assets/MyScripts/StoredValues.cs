using UnityEngine;
using System.Collections;

public class StoredValues : MonoBehaviour
{
	public float yBottom;
	// Use this for initialization
	void Start () 
	{
		yBottom = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		DontDestroyOnLoad(gameObject);
	}
}
