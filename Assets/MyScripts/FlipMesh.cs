using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlipMesh : MonoBehaviour
{
	public GameObject[] obstacles;
	public MeshRenderer rend;
	public Movement movement;
	public GameObject player, head;
	public TakeDamage takeDamage;
	public HeadScript headScript;
	public MeshRenderer[] children;
	public Bounds bounds;
	
	// Use this for initialization
	void Start () 
	{
		obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
		takeDamage = player.GetComponent<TakeDamage>();
		head = GameObject.FindGameObjectWithTag("Head");
		headScript = head.GetComponent<HeadScript>();
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
		foreach (GameObject obstacle in obstacles)
		{
			bool isGate;
			
			if (transform.position.y > 1.5f)
				isGate = true;
			else 
				isGate = false;
			
			if (isGate && movement.isCrouching)
			{
//				takeDamage.points += 25;
			}
			else
			{
				if ((Mathf.Abs (transform.position.z - movement.moveForward) < 1.0f)
			    && (movement.playerHeight < (transform.position.y + 1.5f))
			    && (movement.playerHeight >= (transform.position.y - 1.5f))
			    && (Mathf.Abs (transform.position.x - movement.transform.position.x) < (bounds.size.x/2 + 0.5f)))
				{
					takeDamage.VisualHit();
					if (!movement.isCrouching)
						headScript.VisualHit();
					StartCoroutine(Flash());
				}
			}
		}
	}

	IEnumerator Flash()
	{
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		foreach (MeshRenderer child in children)
			child.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		foreach (MeshRenderer child in children)
			child.enabled = true;
	}
}
