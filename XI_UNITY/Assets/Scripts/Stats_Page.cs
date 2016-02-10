using UnityEngine;
using System.Collections;

public class Stats_Page : MonoBehaviour {
	// Use this for initialization
	bool click = true;

	public void onClick() {
		if ( click)
		{
			transform.localPosition = new Vector3 (0, 0, 0);
			click =false;
			Debug.Log (click);
		}
		else if (!click)
		{
			transform.localPosition = new Vector3 (0, -750, 0);
			click =true;
		}
	}
		

}
