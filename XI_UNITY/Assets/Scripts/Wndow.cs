using UnityEngine;
using System.Collections;

public class Wndow : MonoBehaviour
{
	public static void Generate()
	{	GameObject clone = (GameObject)Instantiate(Resources.Load("Window"));	}

	// Use this for initialization
	void Start ()
	{
		//clone.transform.SetParent (renderCanvas.transform, false);
		transform.parent = GameObject.Find("Canvas").transform;
		transform.localPosition = new Vector3 (0, 0, 0);
	}
}
