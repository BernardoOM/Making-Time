using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro_Dialogue : MonoBehaviour {
	public bool optionA=true, optionB=false;
	private float timer;
	private float click_time=-10f;
	// Use this for initialization

	private int key = 0;
	public GameObject opA;
	public GameObject opB;
	public GameObject hpy;
	public GameObject disp;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer - click_time > 1.3f&&timer - click_time < 4f) {
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
			GameObject.Find ("BubbleText").GetComponent<Text> ().text = "Awesome! See you tomorrow!";
			hpy.SetActive (true);

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
			GameObject.Find ("BubbleText").GetComponent<Text> ().text = "Aww, alright. Maybe next time.";
			disp.SetActive (true);
			click_time = timer;
		}
	}

	public void respond2(){
		
		//GameObject.Find ("respond2").gameObject.SetActive (true);
		if (key == 0) {
			GameObject.Find ("respond").GetComponent<Text> ().text = "I’m even about to start a research project soon.>";
			key = 1;
		}else if (key == 1) {
			GameObject.Find ("BubbleText").GetComponent<Text> ().text = "Oh cool! So, would you want to come to a party I’m hosting tomorrow night?";
			key = 2;
		}
		else if (key == 2) {
			GameObject.Find ("respond").SetActive(false);	
			opA.SetActive (true);
			opB.SetActive (true);
			opA.GetComponent<Text>().color = new Color(0, 150, 255, 255);
			opB.GetComponent<Text>().color = new Color(255, 255, 255, 255);
		}
	}

	public void respond3(){

		GameObject.Find ("BubbleText2").gameObject.SetActive (true);

		//GameObject.Find ("BubbleText").GetComponent<Text> ().text = "Oh cool! So, would you want to come to a party I’m hosting tomorrow night?";

	}

}
