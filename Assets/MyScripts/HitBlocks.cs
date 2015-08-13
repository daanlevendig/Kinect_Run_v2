using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitBlocks : MonoBehaviour 
{
	public MeshRenderer rend;
	public HUD hud;
	public GameObject player;

	public Vector3 randomDirection;

	public float blockSpeed;
	public float sideMove;
	public float upMove; 
	public float forwardMove;
	public float startMoving;
	public float initX, initY, initZ;

	public bool wallPunch;

	// Use this for initialization
	void Start () 
	{
		rend = gameObject.GetComponent<MeshRenderer>();
		player = GameObject.FindGameObjectWithTag("Player");
		hud = player.GetComponent<HUD>();

		randomDirection = new Vector3(Random.Range(0f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
		transform.Rotate(randomDirection);

		startMoving = 0.0f;
		sideMove = transform.position.x;
		upMove = transform.position.y;
		forwardMove = transform.position.z;

		wallPunch = false;
		
		initX = sideMove;
		initY = upMove;
		initZ = forwardMove;
	}

	void FixedUpdate()
	{
		if (wallPunch)
		{
			startMoving = 1.0f;
			hud.points += 1.0f;
		}


		MoveBlocks();
		Destroy();
	}

	void MoveBlocks()
	{
		transform.Translate (Vector3.right * startMoving);
		transform.Translate (Vector3.up * startMoving);
		transform.Translate (Vector3.forward * startMoving);
	}

	void Destroy()
	{
		if (transform.position.x > (initX + 5.0f) || transform.position.x < (initX - 5.0f))
			gameObject.SetActive(false);
		else if (transform.position.y > (initY + 5.0f) || transform.position.y < (initY - 2.0f))
			gameObject.SetActive(false);
		else if (transform.position.z > (initZ + 15.0f) || transform.position.z < (initZ - 2.0f))
		    gameObject.SetActive(false);
	}

	public void VisualHit()
	{
		StartCoroutine(Flash());
	}

	IEnumerator Flash()
	{
		rend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
	}
}
