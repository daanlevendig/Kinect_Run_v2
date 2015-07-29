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
	public HUD hud;
	public Bounds bounds;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
		squat = player.GetComponent<Squat>();
		jump = player.GetComponent<Jump>();
		hud = player.GetComponent<HUD>();
		head = GameObject.FindGameObjectWithTag("Head");
		headRend = head.GetComponent<MeshRenderer>();
		rend = gameObject.GetComponent<MeshRenderer>();
		children = gameObject.GetComponentsInChildren<MeshRenderer>();
		bounds = new Bounds(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z)); 
	}
	
	// Update is called once per frame
	void Update () 
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
		
		if (isGate && squat.isSquatting)
		{
			return;
		}
		else
		{
			if ((Mathf.Abs (transform.position.z - movement.moveForward) < (bounds.size.z/2 + 0.5f))
		    && (jump.playerHeight < (transform.position.y + 1.5f))
		    && (jump.playerHeight >= (transform.position.y - 1.5f))
		    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < (bounds.size.x/2 + 0.5f)))
			{
				hud.VisualHit();
				if (!squat.isSquatting/* && headRend.enabled == true*/)
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
