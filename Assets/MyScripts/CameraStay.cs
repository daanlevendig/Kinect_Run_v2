using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraStay : MonoBehaviour
{
	public GameObject player;
	public Vector3 targetPos;
	
	public float smoothTime = 0.1f;
	private float zVelocity = 10.0F;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		targetPos = new Vector3(0.0f, 6.0f, player.transform.position.z - 8.5f);
		
		float newPosition = Mathf.SmoothDamp(transform.position.z, targetPos.z, ref zVelocity, smoothTime);
		
		transform.position = new Vector3(0f, 8.0f, newPosition);
	}
}