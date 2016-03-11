using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
	public delegate void		DragHandler(int maxTotalDay, int minTotalDay, int maxTime, int minTime);
	public event DragHandler	OnDragAreaSet;

	public GameObject	dialogueBox;
	public GameObject	currentScene;
	public GameObject	calendar;
	private GameObject   acept_prefab;  
	//acecept or refuse window prefab. Instantiated each time a social event is generated. 

	private float start_time=99999f;
	private bool end=false;
	private bool clicked=false;
	private float end_time;
	//private Timer timer;
	private float timer;
    //OPCTIONS A,B,C,D
    private bool optionA;
    private bool optionB;
    private bool optionC;
    private bool optionD;
  
	private List<int> social_nub=new List<int>();

	public int acept_status=0;
	private Commitment temp_com;
	//0 unchosen 1 acpte 2 refuse

	public bool CuSceneBool =false;
	//creat this bool for invitation and ause window when current scene on the left
	//move those two windows to the middle of the screen 

	public GameObject RealScene_NPC_Face_A;
	public GameObject RealScene_NPC_Face_B;


    void Update(){
		timer += Time.deltaTime;
//		Debug.Log (timer - start_time);
//		Debug.Log (end);

		//10s later disappear dialogue box 
		if (timer - start_time   > 5f && end == false) {
			EndDialogue ();
			end = true;
			start_time = 99999f;
		}

	}

	//display accept/refuse window for social events. being called in calen.mager in unschdle();
	//use instantiate to display multiple windows 
//	public void Acept_Window(Commitment com, int i){
//		
//		social_nub.Add (i);
//		//GameObject.Find("Window_Accept_Social").transform.localPosition = new Vector3(0, 0, 0);
//		acept_prefab =Instantiate(Resources.Load("Window_Accept_Social"),new Vector3(0, 0, 0),Quaternion.identity) as GameObject;
//		
//		acept_prefab.transform.SetParent (GameObject.Find ("Calendar").transform, false);
//		acept_prefab.transform.localPosition= new Vector3(0, 0, 0);
//
//		//GameObject.Find ("Inviatation_Text").GetComponent<Text> ().text ="A new inviatation to "+ com.name;
//		//acept_prefab.GetComponentInChildren<Text> ().text ="A new inviatation to "+ com.name;
//
//		acept_prefab.transform.FindChild("Inviatation_Text").gameObject.GetComponent<Text>().text
//		="A new inviatation to "+ com.name;
//
//		acept_prefab.transform.FindChild ("Accept_Button").gameObject.GetComponent<Button> ().onClick.AddListener (Acept_Event);
//		acept_prefab.transform.FindChild ("Refuse_Button").gameObject.GetComponent<Button> ().onClick.AddListener (Refuse_Event);
//
//
//
//	} 
//
//	//acept / refuse event are being called by acept/refuse button in window_acept_social window
//	public void Acept_Event(){
//
//		//acept_prefab.transform.localPosition = new Vector3(0, 1000, 0);
//		//GameObject.Find("Window_Accept_Social").transform.localPosition = new Vector3(0, 1000, 0);
//		//close window
//	
//		social_nub.Remove (social_nub.Count - 1);
//		Destroy (acept_prefab);
//
//	}
//
//	public void Refuse_Event(){
//		Destroy (acept_prefab);
//		int num = social_nub.Count - 1;
//		GameManager.Calendar.refuse_social (social_nub[num]);
//		social_nub.Remove (social_nub.Count - 1);
//
//
//		//acept_prefab.transform.localPosition = new Vector3(0, 1000, 0);
//		//close window 
//		
//	}



	public void SetDragArea(int maxTotalDay, int minTotalDay, int maxTime, int minTime)
	{
		if(OnDragAreaSet != null)
		{	OnDragAreaSet(maxTotalDay, minTotalDay, maxTime, minTime);	}
	}

	public void EnterCurrentScene(int dayOfWeek, string event_name)
	{
		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-410.5f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = new Vector3(513f, 0, 0);
		Timer.EnterScene();
		OptionModification (event_name);
		GameObject.Find ("RealScene_Label_Text").GetComponent<Text> ().text = event_name;
		CuSceneBool = true;

//		events = Resources.LoadAll(timePath, typeof(Sprite));
//		for (int i = 0; i < events.Length; i++)
//		{
//			if ((int)curType==3 && events[i].name == "Chore_Postit_active") { GetComponent<Image>().sprite = (Sprite)events[i]; }
//			else if ((int)curType==2 &&events[i].name == "Social_Postit_active") { GetComponent<Image>().sprite = (Sprite)events[i]; }
//			else if ((int)curType==0 &&events[i].name == "Work_Postit_active") { GetComponent<Image>().sprite= (Sprite)events[i]; }
//		}
		if (event_name == "Office Hours") {
			GameObject.Find ("Current Scene").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Sprites/RealScene/RealScene_Office", typeof(Sprite));
			GameObject.Find ("RealSceneLabel").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Office_Label", typeof(Sprite));

			GameObject.Find ("small_NPC").transform.localPosition = new Vector3 (-155f, 131f, 0f);

			GameObject.Find ("Main Camera").GetComponent<AudioManager> ().office_sfx_play ();


		}	
		if (event_name == "Dinner") {
			GameObject.Find ("Current Scene").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Sprites/RealScene/RealScene_Lunch", typeof(Sprite));
			GameObject.Find ("small_NPC").transform.localPosition = new Vector3 (-135f, 154f, 0f);
			GameObject.Find ("RealSceneLabel").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Restaurant_Label", typeof(Sprite));

		}		
		if (event_name == "Party") {
			GameObject.Find ("Current Scene").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Sprites/RealScene/RealScene_Party", typeof(Sprite));
			GameObject.Find ("small_NPC").transform.localPosition = new Vector3 (-135f, 71f, 0f);
			GameObject.Find ("RealSceneLabel").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Party_Label", typeof(Sprite));

		}
		if (event_name == "Movie Night") {
			GameObject.Find ("Current Scene").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Sprites/RealScene/RealScene_MovieNight", typeof(Sprite));
			GameObject.Find ("small_NPC").transform.localPosition = new Vector3 (-135f, 71f, 0f);
			GameObject.Find ("RealSceneLabel").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Party_Label", typeof(Sprite));

		}
		if (event_name == "Game Night") {
			GameObject.Find ("Current Scene").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Sprites/RealScene/RealScene_MovieNight", typeof(Sprite));
			GameObject.Find ("small_NPC").transform.localPosition = new Vector3 (-135f, 71f, 0f);
			GameObject.Find ("RealSceneLabel").GetComponent<Image> ().sprite = (Sprite)Resources.Load ("Party_Label", typeof(Sprite));

		}
		//creat this bool for invitation and ause window when current scene on the left
		//move those two windows to the middle of the screen 

	}

	public void LeaveCurrentScne()
	{
		EndDialogue ();
		clicked = false;

		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-923.5f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = Vector3.zero;

		GameObject.Find ("Main Camera").GetComponent<AudioManager> ().office_sfx_stop ();
		RealScene_NPC_Face_A.SetActive (false);
		RealScene_NPC_Face_B.SetActive (false);

		CuSceneBool = false;
	}

	public void StartDialogue()
	{	
		GameObject.Find ("Main Camera").GetComponent<AudioManager> ().bubble_play();

		if (clicked == false) {
			GameObject.Find ("Dialogue").transform.localPosition = new Vector3 (0f, -251.5f, 0f);
			GameObject.Find ("Bubble").transform.localPosition = new Vector3 (-14.6f, 251.6f);
			GameObject.Find ("Question_A").transform.localPosition = new Vector3 (-3f, 254.7f);
			GameObject.Find ("big_NPC").transform.localPosition = new Vector3 (-136f, 47.5f);

			GameObject.Find ("Option_A").transform.localPosition = new Vector3 (3f, -199f);
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(0,150,255,255);
            GameObject.Find ("Option_B").transform.localPosition = new Vector3 (3f, -237f);
//			GameObject.Find ("Option_C").transform.localPosition = new Vector3 (3f, -275.4f);
//			GameObject.Find ("Option_D").transform.localPosition = new Vector3 (7f, -313.4f);
//			GameObject.Find ("Option_A").transform.localPosition = new Vector3 (-19f, -199f);
//			GameObject.Find("Option_A").GetComponent<Text>().color = new Color(0,150,255,255);
//			GameObject.Find ("Option_B").transform.localPosition = new Vector3 (-19f, -237f);
//			GameObject.Find ("Option_C").transform.localPosition = new Vector3 (-19f, -275.4f);
//			GameObject.Find ("Option_D").transform.localPosition = new Vector3 (-15.7f, -313.4f);
//
            optionA = true;
            optionB = false;
            optionC = false;
            optionD = false;
            clicked = true;
		} else if (clicked == true) {
			GameObject.Find ("Bubble").transform.localPosition = new Vector3 (-14.6f, 251.6f);
			if (optionA) {
				GameObject.Find ("Question_B1").transform.localPosition = new Vector3 (-44f, 262.2f);
			}
			if (optionB) {
				GameObject.Find ("Question_B2").transform.localPosition = new Vector3 (-44f, 262.2f);

			}
			end=false;
			start_time = timer;
		}

//		end=false;
//		start_time = timer;
		//float a = Time.deltaTime;
		//GameObject.Find("big_NPC").SetActive(true);
	}

	public void ClickOptionA(){

        if (!optionA)
        {
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(0, 150, 255, 255);
            GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//            GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//            GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
            optionA = true;
            optionB = false;
            optionC = false;
            optionD = false;
        }
        else
        {
			RealScene_NPC_Face_A.SetActive (true);
            GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f, 0);
			GameObject.Find("Question_B1").transform.localPosition = new Vector3(-44f, 262.2f);
            end = false;
            start_time = timer;
        }
	}
    public void ClickOptionB()
    {
        if (!optionB)
        {
            GameObject.Find("Option_B").GetComponent<Text>().color = new Color(0, 150, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//            GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//            GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
            optionA = false;
            optionB = true;
            optionC = false;
            optionD = false;
        }
        else
        {
			RealScene_NPC_Face_B.SetActive (true);
            GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f, 0);
			GameObject.Find("Question_B2").transform.localPosition = new Vector3(-44f, 262.2f);
            end = false;
            start_time = timer;
        }

    }
    public void ClickOptionC()
    {
        if (!optionC)
        {
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//			GameObject.Find("Option_C").GetComponent<Text>().color = new Color(0, 150, 255, 255);
//            GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
            optionA = false;
            optionB = false;
            optionC = true;
            optionD = false;
        }
        else
        {
            GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f, 0);
			GameObject.Find("Question_B").transform.localPosition = new Vector3(-44f, 262.2f);
            end = false;
            start_time = timer;
        }

    }
    public void ClickOptionD()
    {
        if (!optionD)
        {
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//            GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//			GameObject.Find("Option_D").GetComponent<Text>().color = new Color(0, 150, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
//            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
            optionA = false;
            optionB = false;
            optionC = false;
            optionD = true;
        }
        else
        {
            GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f, 0);
            GameObject.Find("Question_B").transform.localPosition = new Vector3(-8.4f, 262.2f);
            end = false;
            start_time = timer;
        }

    }

    public void EndDialogue()
	{	
		GameObject.Find ("Option_A").GetComponent<Text>().enabled=true;
		GameObject.Find ("Option_B").GetComponent<Text>().enabled=true;
//		GameObject.Find ("Option_C").GetComponent<Text>().enabled=true;
//		GameObject.Find ("Option_D").GetComponent<Text>().enabled=true;

		GameObject.Find("Dialogue").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Bubble").transform.localPosition =new Vector3(-600f,0);
		GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Question_B1").transform.localPosition = new Vector3(-600f,0f);
		GameObject.Find("Question_B2").transform.localPosition = new Vector3(-600f,0f);

		GameObject.Find("big_NPC").transform.localPosition =new Vector3(-600f,0);
		GameObject.Find("Option_A").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Option_B").transform.localPosition = new Vector3(-600f,0);
//		GameObject.Find("Option_C").transform.localPosition = new Vector3(-600f,0);
//		GameObject.Find("Option_D").transform.localPosition =new Vector3(-600f,0);
		//GameObject.Find("big_NPC").SetActive(false);
		}


	public void OptionModification(string event_name){


	

		GameObject.Find("Option_A").GetComponent<Text>().color = new Color(0, 150, 255, 255);
		GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//		GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
//		GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);

		if (event_name == "Party") {
			GameObject.Find("Question_A").GetComponent<Text>().text="What have you been up to?";
			GameObject.Find ("Option_A").GetComponent<Text> ().text = "A. Not much.";
			GameObject.Find ("Option_B").GetComponent<Text> ().text = "B. I’m starting a research project soon.";
			//B1 B2
			GameObject.Find ("Question_B1").GetComponent<Text> ().text = "Oh, ok.";
			GameObject.Find ("Question_B2").GetComponent<Text> ().text = "Oh, cool!";
			RealScene_NPC_Face_A.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("NPC Faces/NPC_04", typeof(Sprite));
			RealScene_NPC_Face_B.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("NPC Faces/NPC_03", typeof(Sprite));

		}
			
		if (event_name == "Office Hours") {
			GameObject.Find("Question_A").GetComponent<Text>().text="Can you please help me with the homework?";
			GameObject.Find ("Option_A").GetComponent<Text> ().text = "A. Sure, I’d be happy to.";
			GameObject.Find ("Option_B").GetComponent<Text> ().text = "B. You should be able to do the homework yourself.";
			//B1 B2
			GameObject.Find ("Question_B1").GetComponent<Text> ().text = "Yay! Thank you so much!";
			GameObject.Find ("Question_B2").GetComponent<Text> ().text = "Augh!";	
			RealScene_NPC_Face_A.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("NPC Faces/NPC_01", typeof(Sprite));
			RealScene_NPC_Face_B.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("NPC Faces/NPC_05", typeof(Sprite));
			}	
		
			
		if (event_name == "Dinner") {
			GameObject.Find("Question_A").GetComponent<Text>().text="You know, there’s a concert happening this week. Want to go to it with me?";
			GameObject.Find ("Option_A").GetComponent<Text> ().text = "A. Yeah, that sounds awesome!";
			GameObject.Find ("Option_B").GetComponent<Text> ().text = "B. No, sorry, I’m really busy this week.";
			//B1 B2
			GameObject.Find ("Question_B1").GetComponent<Text> ().text = "Sweet! Can’t wait";
			GameObject.Find ("Question_B2").GetComponent<Text> ().text = "Aww, maybe next time, then.";	
		
			RealScene_NPC_Face_A.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("NPC Faces/NPC_01", typeof(Sprite));
			RealScene_NPC_Face_B.GetComponent<Image> ().sprite = (Sprite)Resources.Load ("NPC Faces/NPC_04", typeof(Sprite));


		}		
		if (event_name == "Movie Night") {
			GameObject.Find("Question_A").GetComponent<Text>().text="Star wars?";
		}		

	
	}
}
