using UnityEngine;
using System.Collections;

public class HeadScript : MonoBehaviour 
{
	public MeshRenderer rend;
	public Movement movement;

	// Use this for initialization
	void Start () 
	{
		rend = gameObject.GetComponent<MeshRenderer>();
		movement = GetComponentInParent<Movement>();
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public void VisualHit()
	{
		StartCoroutine(Flash());
	}
	
	IEnumerator Flash()
	{
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = false;
		yield return new WaitForSeconds(0.05f);
		rend.enabled = true;
	}
}
