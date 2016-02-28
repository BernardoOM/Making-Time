using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public delegate void		DragHandler(int maxTotalDay, int minTotalDay, int maxTime, int minTime);
	public delegate void		TimerSlow(bool active);
	public event DragHandler	OnDragAreaSet;
	public event TimerSlow		OnActivateCommitment;

	public GameObject	dialogueBox;
	public GameObject	currentScene;
	public GameObject	calendar;

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

	public void SetDragArea(int maxTotalDay, int minTotalDay, int maxTime, int minTime)
	{
		if(OnDragAreaSet != null)
		{	OnDragAreaSet(maxTotalDay, minTotalDay, maxTime, minTime);	}
	}

	public void EnterCurrentScene(int dayOfWeek, string event_name)
	{
		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-410.5f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = new Vector3(513f, 0, 0);
		OnActivateCommitment(true);
		OptionModification (event_name);
	}

	public void LeaveCurrentScne(int dayOfWeek)
	{
		EndDialogue ();
		clicked = false;

		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-923.5f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = Vector3.zero;
		OnActivateCommitment(false);
	}

	public void StartDialogue()
	{	

		if (clicked == false) {
			GameObject.Find ("Dialogue").transform.localPosition = new Vector3 (0f, -251.5f, 0f);
			GameObject.Find ("Bubble").transform.localPosition = new Vector3 (-14.6f, 251.6f);
			GameObject.Find ("Question_A").transform.localPosition = new Vector3 (-3.2f, 247.1f);
			GameObject.Find ("big_NPC").transform.localPosition = new Vector3 (-136f, 47.5f);

			GameObject.Find ("Option_A").transform.localPosition = new Vector3 (3f, -199f);
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(0,150,255,255);
            GameObject.Find ("Option_B").transform.localPosition = new Vector3 (3f, -237f);
			GameObject.Find ("Option_C").transform.localPosition = new Vector3 (3f, -275.4f);
			GameObject.Find ("Option_D").transform.localPosition = new Vector3 (7f, -313.4f);
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
			GameObject.Find("Question_B").transform.localPosition = new Vector3(-8.4f,262.2f);
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
            GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
            optionA = true;
            optionB = false;
            optionC = false;
            optionD = false;
        }
        else
        {
            GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f, 0);
            GameObject.Find("Question_B").transform.localPosition = new Vector3(-8.4f, 262.2f);
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
            GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
            optionA = false;
            optionB = true;
            optionC = false;
            optionD = false;
        }
        else
        {
            GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f, 0);
            GameObject.Find("Question_B").transform.localPosition = new Vector3(-8.4f, 262.2f);
            end = false;
            start_time = timer;
        }

    }
    public void ClickOptionC()
    {
        if (!optionC)
        {
            GameObject.Find("Option_C").GetComponent<Text>().color = new Color(0, 150, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
            optionA = false;
            optionB = false;
            optionC = true;
            optionD = false;
        }
        else
        {
            GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f, 0);
            GameObject.Find("Question_B").transform.localPosition = new Vector3(-8.4f, 262.2f);
            end = false;
            start_time = timer;
        }

    }
    public void ClickOptionD()
    {
        if (!optionD)
        {
            GameObject.Find("Option_D").GetComponent<Text>().color = new Color(0, 150, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
            GameObject.Find("Option_A").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_B").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_C").GetComponent<Text>().enabled = true;
            GameObject.Find("Option_D").GetComponent<Text>().enabled = true;
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
		GameObject.Find ("Option_C").GetComponent<Text>().enabled=true;
		GameObject.Find ("Option_D").GetComponent<Text>().enabled=true;

		GameObject.Find("Dialogue").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Bubble").transform.localPosition =new Vector3(-600f,0);
		GameObject.Find("Question_A").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Question_B").transform.localPosition = new Vector3(-600f,0f);

		GameObject.Find("big_NPC").transform.localPosition =new Vector3(-600f,0);
		GameObject.Find("Option_A").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Option_B").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Option_C").transform.localPosition = new Vector3(-600f,0);
		GameObject.Find("Option_D").transform.localPosition =new Vector3(-600f,0);
		//GameObject.Find("big_NPC").SetActive(false);
		}


	public void OptionModification(string event_name){
		GameObject.Find ("Option_A").GetComponent<Text> ().text = "A.  Nice " + event_name+" !";
		GameObject.Find ("Option_B").GetComponent<Text> ().text = "B.  Poorly designed " + event_name+" !";

		GameObject.Find("Option_A").GetComponent<Text>().color = new Color(0, 150, 255, 255);
		GameObject.Find("Option_B").GetComponent<Text>().color = new Color(255, 255, 255, 255);
		GameObject.Find("Option_C").GetComponent<Text>().color = new Color(255, 255, 255, 255);
		GameObject.Find("Option_D").GetComponent<Text>().color = new Color(255, 255, 255, 255);

		if (event_name == "Party") {
			GameObject.Find("Question_A").GetComponent<Text>().text="How's party?";
		}
		if (event_name == "Meeting") {
			GameObject.Find("Question_A").GetComponent<Text>().text="Meeting time!";
		}		
		if (event_name == "Office Hours") {
			GameObject.Find("Question_A").GetComponent<Text>().text="I didn't do my homework.";
		}		
		if (event_name == "Game Night") {
			GameObject.Find("Question_A").GetComponent<Text>().text="Let's Play!";
		}		
		if (event_name == "Lunch") {
			GameObject.Find("Question_A").GetComponent<Text>().text="Extremely hungry!";
		}		
		if (event_name == "Movie Night") {
			GameObject.Find("Question_A").GetComponent<Text>().text="Star wars?";
		}		

	
	}
}
