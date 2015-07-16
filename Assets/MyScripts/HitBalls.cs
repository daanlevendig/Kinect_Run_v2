using UnityEngine;
using System.Collections;

public class HitBalls : MonoBehaviour 
{
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
			ballSpeed = 0.05f;
			startMoving = 1.0f;
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
		if (transform.position.x > (initX + 5f) || transform.position.x < (initX - 5f))
			gameObject.SetActive(false);
		else if (transform.position.y > (initY + 5f) || transform.position.y < (initY - 1f))
			gameObject.SetActive(false);
		else if (transform.position.z > (initZ + 15f) || transform.position.z < (initZ - 1f))
		    gameObject.SetActive(false);
	}
}
