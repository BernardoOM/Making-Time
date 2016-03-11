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
	public GameObject border;
	public GameObject Player;
	public GameObject Dialogue;
	public GameObject Bubble;
	private string imagePath = "Sprites/Characters";
	public Sprite[] characters;
	public Sprite[] characters_small;

	public GameObject NPC;
	int rand=2;
	bool stop=false;
	public float rate=0.5f; 
	void Start()
	{
//		Object[] CharactersS = Resources.LoadAll(imagePath, typeof(Sprite));
//		characters = new Sprite[9];
//		for (int i = 0; i < CharactersS.Length; i++)
//		{
//			if (CharactersS[i].name == "Char_00_Front"){characters[0] = (Sprite)CharactersS[i];}
//			else if (CharactersS[i].name == "Char_01_Front") { characters[1] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_02_Front") { characters[2] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_09_Front") { characters[3] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_04_Front") { characters[4] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_05_Front") { characters[5] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_06_Front") { characters[6] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_07_Front") { characters[7] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_08_Front") { characters[8] = (Sprite)CharactersS[i]; }
		}

//		Object[] CharactersS_small = Resources.LoadAll(imagePath, typeof(Sprite));
//		characters_small = new Sprite[9];
//		for (int i = 0; i < characters_small.Length; i++)
//		{
//			if (CharactersS[i].name == "Char_00_Full"){characters[0] = (Sprite)CharactersS[i];}
//			else if (CharactersS[i].name == "Char_01_Full") { characters[1] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_02_Full") { characters[2] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_09_Full") { characters[3] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_04_Full") { characters[4] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_05_Full") { characters[5] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_06_Full") { characters[6] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_07_Front") { characters[7] = (Sprite)CharactersS[i]; }
//			else if (CharactersS[i].name == "Char_08_Full") { characters[8] = (Sprite)CharactersS[i]; }
//		}


	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer - click_time > 1.3f&&timer - click_time < 4f) {
			SceneManager.LoadScene (2);
		}


		random_flash ();
	}

	public void random_flash(){
		if (timer >= rate) {
			if (stop == false) {
				border.SetActive (!border.activeSelf);
				timer = 0;
				//NPC.SetActive (false);
				//rand = Random.Range (0, 9);

			}
		}
	}

	public void pick_one(){
		stop = true;
		Bubble.SetActive (true);
		Dialogue.SetActive (true);
		Player.SetActive (true);

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
			ApplicationModel.tutorialEvent = "Party";

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
			ApplicationModel.tutorialEvent = "Dinner";

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
