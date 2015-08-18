using UnityEngine;
using System.Collections;

public class StoredValues : MonoBehaviour
{
	public float yBottom, lowestFoot;

	// Update is called once per frame
	void Update () 
	{
		DontDestroyOnLoad(gameObject);
	}
}
