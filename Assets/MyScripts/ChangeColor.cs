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
		lerpedColor = Color.magenta;
		newMaterial.color = lerpedColor;
		colorRenderer.material = newMaterial;
	}
	
	// Update is called once per frame
	void Update () 
	{
		newMaterial.color = lerpedColor;

		if (newMaterial.color == Color.red)
			lerpedColor = Color.Lerp(Color.red, Color.blue, Time.deltaTime);
		else if (newMaterial.color == Color.blue)
			lerpedColor = Color.Lerp(Color.blue, Color.red, Time.deltaTime);
	}
}
