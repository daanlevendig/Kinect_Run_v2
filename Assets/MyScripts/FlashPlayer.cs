using UnityEngine;
using System.Collections;

public class FlashPlayer : MonoBehaviour 
{
	public MeshRenderer rend, headRend;
	public GameObject head;
	public Movement movement;
	public Squat squat;

	public bool isColliding;
	
	// Use this for initialization
	void Start () 
	{
		head = GameObject.FindGameObjectWithTag("Head");
		rend = GetComponent<MeshRenderer>();
		headRend = head.GetComponent<MeshRenderer>();
		movement = GetComponent<Movement>();
		squat = GetComponent<Squat>();

		isColliding = false;
	}
	
	public void VisualHit()
	{
		if (!movement.isPaused)
			StartCoroutine(Flash());
	}
	
	IEnumerator Flash()
	{
		isColliding = true;
		rend.enabled = false;
		if (!squat.isSquatting)
			headRend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		if (!squat.isSquatting)
			headRend.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		if (!squat.isSquatting)
			headRend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		if (!squat.isSquatting)
			headRend.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		if (!squat.isSquatting)
			headRend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		if (!squat.isSquatting)
			headRend.enabled = true;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = false;
		if (!squat.isSquatting)
			headRend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;
		if (!squat.isSquatting)
			headRend.enabled = true;
		isColliding = false;
	}
}
