using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitBalls : MonoBehaviour 
{
	public MeshRenderer rend;
	public HUD hud;
	public GameObject player;

	public Vector3 randomDirection;

	public float ballSpeed;
	public float sideMove;
	public float upMove; 
	public float forwardMove;
	public float startMoving;
	public float initX, initY, initZ;

	public bool ballPunch;

	// Use this for initialization
	void Start () 
	{
		rend = gameObject.GetComponent<MeshRenderer>();
		player = GameObject.FindGameObjectWithTag("Player");
		hud = player.GetComponent<HUD>();

		randomDirection = new Vector3(Random.Range(0f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
		transform.Rotate(randomDirection);

		ballSpeed = 0.0f;
		startMoving = 0.0f;
		sideMove = transform.position.x;
		upMove = transform.position.y;
		forwardMove = transform.position.z;

		ballPunch = false;
		
		initX = sideMove;
		initY = upMove;
		initZ = forwardMove;
	}

	void Update()
	{
		if (ballPunch)
		{
			ballSpeed = 0.01f;
			startMoving = 1.0f;
			hud.points += 100.0f;
		}

		sideMove += ballSpeed;
		upMove += ballSpeed;
		forwardMove += ballSpeed;

		MoveBall();
		Destroy();
	}

	void MoveBall()
	{
		transform.Translate (Vector3.right * sideMove * startMoving * Time.deltaTime);
		transform.Translate (Vector3.up * upMove * startMoving * Time.deltaTime);
		transform.Translate (Vector3.forward * forwardMove * startMoving * Time.deltaTime);
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
