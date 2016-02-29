using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro_Dialogue : MonoBehaviour {
	public bool optionA=true, optionB=false;
	private float timer;
	private float click_time=-10f;
	// Use this for initialization
	void Start () {
		GameObject.Find("Option_A").GetComponent<Text>().color = new Color(0, 150, 255, 255);
		GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer - click_time > 3f&&timer - click_time < 4f) {
			SceneManager.LoadScene (2);
		}
	}

	public void click_A(){
		if (!optionA) {
			GameObject.Find ("Option_A").GetComponent<Text> ().color = new Color (0, 150, 255, 255);
			GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);

			optionA = true;
			optionB = false;
		} else {
			GameObject.Find ("Option_B").transform.localPosition = new Vector3 (0, 1500f, 0);
			click_time = timer;
		}
	}


	public void click_B(){
		if (!optionB) {
			GameObject.Find ("Option_B").GetComponent<Text> ().color = new Color (0, 150, 255, 255);
			GameObject.Find("Option_A").GetComponent<Text>().color = new Color(255, 255, 255, 255);

			optionB = true;
			optionA = false;
		} else {
			GameObject.Find ("Option_A").transform.localPosition = new Vector3 (0, 1500f, 0);
			click_time = timer;
		}
	}
}
