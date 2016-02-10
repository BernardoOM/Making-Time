using UnityEngine;
using System.Collections;

public class Stats_Page : MonoBehaviour {
	// Use this for initialization
	bool click = true;

	public void onClick() {
		if ( click)
		{
			transform.position = new Vector3 (1334 / 2f, 750 / 2f, 0);
			click =false;
			Debug.Log (click);
		}
		else if (!click)
		{
			transform.position = new Vector3 (-1334 / 2f, -750 / 2f, 0);
			click =true;
		}
	}
		

}
