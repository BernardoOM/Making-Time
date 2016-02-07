using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public float timer;
	// Use this for initialization
	private RectTransform rt;
	private int block;
	private bool tues=true;
	private int speed=50;

	void Start ()
	{
		 rt = GetComponent (typeof (RectTransform)) as RectTransform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		block = (int)(speed * timer / 83);

		if (block < 6) {
			rt.sizeDelta = new Vector2 (168f, speed * timer);
		}
	}
}
  