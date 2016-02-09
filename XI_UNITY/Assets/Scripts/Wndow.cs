using UnityEngine;
using System.Collections;

public class Wndow : MonoBehaviour {
	public GameObject prefab;
	// Use this for initialization
	void Start () {
		GameObject clone = (GameObject)Instantiate (prefab, transform.localPosition, Quaternion.identity);
		//clone.transform.SetParent (renderCanvas.transform, false);
		clone.transform.parent=GameObject.Find("Canvas").transform;
		clone.transform.localPosition = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
