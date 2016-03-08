using UnityEngine;
using System.Collections;

public class Stats_Page : MonoBehaviour {
	// Use this for initialization
	bool click = true;

	public void onClick() {
		if ( click)
		{
			transform.localPosition = Vector3.zero;
			click =false;
			GameManager.Instance.PauseGame();

		}
		else if (!click)
		{
			transform.localPosition = new Vector3 (0, -750, 0);
			click =true;
			GameManager.Instance.StartGame();

		}
	}
		

}
