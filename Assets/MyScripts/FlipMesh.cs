using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlipMesh : MonoBehaviour
{
	public MeshRenderer[] children;
	public MeshRenderer rend, headRend;
	public GameObject player, head;
	public Movement movement;
	public Jump jump;
	public Squat squat;
	public FlashPlayer flash;
	public Bounds bounds;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
		squat = player.GetComponent<Squat>();
		jump = player.GetComponent<Jump>();
		flash = player.GetComponent<FlashPlayer>();
		head = GameObject.FindGameObjectWithTag("Head");
		headRend = head.GetComponent<MeshRenderer>();
		rend = gameObject.GetComponent<MeshRenderer>();
		children = gameObject.GetComponentsInChildren<MeshRenderer>();
		bounds = new Bounds(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z)); 
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		CollideWithPlayer();
	}
	
	void CollideWithPlayer()
	{
		bool isGate;
		
		if (transform.position.y > 1.5f)
			isGate = true;
		else 
			isGate = false;
		
		if (!isGate || !squat.isSquatting)
		{
			if ((Mathf.Abs (transform.position.z - movement.moveForward) < (bounds.size.z/2 + 0.5f))
		    && (jump.playerHeight < (transform.position.y + (bounds.size.y/2 + 1.0f)))
		    && (jump.playerHeight >= (transform.position.y - (bounds.size.y/2 + 1.0f)))
		    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < (bounds.size.x/2 + 0.5f)))
			{
				flash.VisualHit();
				if (!squat.isSquatting && !movement.isPaused)
					StartCoroutine(Flash());
			}
		}
	}

	IEnumerator Flash()
	{
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
	}
}
