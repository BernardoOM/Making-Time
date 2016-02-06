using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Calculate : MonoBehaviour {
	private float pos_y;
	private float pos_x;
	private float timer;
	private int speed=50;
	private int height=83;
	private float test;
	// Use this for initialization
	private DBMakingTime db;
	private string text;
	private int[] values=new int[2]; 


	private bool exe=false;

	void Start () {
		db = GameObject.Find ("DataBase").GetComponent<DBMakingTime> ();
		text = GetComponentInChildren<Text> ().text;

		Debug.Log ("energy value:");
		Debug.Log (db.current_val [0]);
		Debug.Log ("happiness value:");
		Debug.Log (db.current_val [1]);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		compare ();



	}



	void compare(){
		if ((int)(timer * speed / height ) == ((237f - transform.localPosition.y) / height)) 
			if(exe==false)
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
