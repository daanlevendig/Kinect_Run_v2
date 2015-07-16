using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour 
{
	public Color lerpedColor;
	public Material newMaterial;
	public MeshRenderer colorRenderer;

	// Use this for initialization
	void Start () 
	{
		colorRenderer = gameObject.GetComponent<MeshRenderer>();
		lerpedColor = Color.white;
		newMaterial.color = lerpedColor;
		colorRenderer.material = newMaterial;
	}
	
	// Update is called once per frame
	void Update () 
	{
		newMaterial.color = lerpedColor;

		lerpedColor = Color.Lerp(Color.red, Color.blue, Time.deltaTime*2f);
//			lerpedColor = Color.Lerp(Color.blue, Color.red, Time.deltaTime);
	}
}
