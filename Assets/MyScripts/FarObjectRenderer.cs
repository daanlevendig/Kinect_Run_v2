using UnityEngine;
using System.Collections;

public class FarObjectRenderer : MonoBehaviour 
{
	public GameObject player;
	public Renderer rend;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.z >= (player.transform.position.z - 60f))
			rend.enabled = true;
		else
			rend.enabled = false;
	}
}