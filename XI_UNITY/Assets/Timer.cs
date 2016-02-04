using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public float timer;
	// Use this for initialization
	private RectTransform rt;
	private int block;
	private bool tues=true;


	void Start () {
		 rt = GetComponent (typeof (RectTransform)) as RectTransform;

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		block = (int)(200 * timer / 83);

		if (block < 6) {
			rt.sizeDelta = new Vector2 (168f, 200 * timer);
		}
		//30*timer
//		if (tues==true) 
//		{
//			Instantiate (rt, new Vector3 (-300, -98, 0), new Quaternion (0, 0, 0, 0));
//			tues = false;
//			Debug.Log (tues);
//
//		}
		//**use TAG 

		//Debug.Log (timer);
		//Debug.Log (block);
		//one day is 16s for timer;

	}
}
  