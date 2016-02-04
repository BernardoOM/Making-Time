using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public float timer;
	// Use this for initialization
	private RectTransform rt;
	private int block;
	void Start () {
		 rt = GetComponent (typeof (RectTransform)) as RectTransform;

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		rt.sizeDelta = new Vector2 (168f, 30*timer);
		//Debug.Log (timer);
		block = (int)(30 * timer / 83);
		Debug.Log (block);
		//one day is 16s for timer;
	}
}
  