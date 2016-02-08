using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
	private float pos_y;
	private float pos_x;
	public float timer;
	public int speed=50;
	private int height=83;
	private float anchor_x=-397f;
	private float gap_x=171.7f;

	private float test;
	// Use this for initialization
	private DBMakingTime db;
	private string text;
	private int[] values=new int[2];

	private bool exe = false;

	void Start ()
	{
		db = GameObject.Find ("DataBase").GetComponent<DBMakingTime> ();
		text = GetComponentInChildren<Text> ().text;

		Debug.Log ("energy value:");
		Debug.Log (db.current_val [0]);
		Debug.Log ("happiness value:");
		Debug.Log (db.current_val [1]);
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		compare ();
	}

	void compare()
	{

		int loc = (int) ((timer*speed) / (height * 6));
		// loc represents which colomn currently 

//		Debug.Log(anchor_x+gap_x*loc);
//		Debug.Log(transform.localPosition.x);

		if ( ((int)( (timer * speed) % (height*6) )) == (237f - transform.localPosition.y)) 
			if(exe==false)
		if(transform.localPosition.x-anchor_x-gap_x*loc<2f && transform.localPosition.x-anchor_x-gap_x*loc>-2f)
		{
//			Debug.Log ("hit");
			values = db.Select_DB (text);

			db.current_val [0] += values[0];
			db.current_val [1] += values[1];

			Debug.Log ("energy value:");
			Debug.Log (db.current_val [0]);
			Debug.Log ("happiness value:");
			Debug.Log (db.current_val [1]);
			exe = true;
		}
	}
}
