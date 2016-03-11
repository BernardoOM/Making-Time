using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//script for display inviatation window, ask player accept or refuse.  binded with "Window_Accept_Social" prefab 

public class Chore_Acceptance : MonoBehaviour {

	private int num;
	private Commitment temp_com;
	private int rand_num;
    private string[] days = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    private string[] times = {"Morning", "Afternoon", "Evening"};


	// Use this for initialization
	void Start () {
		transform.SetParent (GameObject.Find ("Calendar").transform, false);
		transform.localPosition= new Vector3(0, 0, 0);
		if (GameManager.UI.CuSceneBool == true) {
			transform.SetParent (GameObject.Find ("Canvas").transform, false);
			transform.localPosition = new Vector3(0, 0, 0);

		}
		rand_num=Random.Range (1, 10);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Acept_Window(Commitment com){

        string minDay = getDay(com.minTotalDay);
        string maxDay = getDay(com.maxTotalDay);
        string minTime = getTime(com.minTime);
        string maxTime = getTime(com.maxTime);
     
        temp_com = com;
		transform.FindChild("Invitation_Text").gameObject.GetComponent<Text>().text ="A new chore: "+ com.name;

        if (minDay == maxDay)
        {
            transform.FindChild("MinDayText").gameObject.GetComponent<Text>().text = minDay;
            transform.FindChild("ToDay").gameObject.GetComponent<Text>().text = "";
            transform.FindChild("MaxDayText").gameObject.GetComponent<Text>().text = "";
        }
        else
        {
            transform.FindChild("MinDayText").gameObject.GetComponent<Text>().text = minDay;
            transform.FindChild("ToDay").gameObject.GetComponent<Text>().text = "To";
            transform.FindChild("MaxDayText").gameObject.GetComponent<Text>().text = maxDay;
        }
        if(minTime == maxTime)
        {
            transform.FindChild("MinTimeText").gameObject.GetComponent<Text>().text = minTime;
            transform.FindChild("OrTime").gameObject.GetComponent<Text>().text = "";
            transform.FindChild("MaxTimeText").gameObject.GetComponent<Text>().text = "";

        }
        else
        {
            transform.FindChild("MinTimeText").gameObject.GetComponent<Text>().text = minTime;
            transform.FindChild("OrTime").gameObject.GetComponent<Text>().text = "Or";
            transform.FindChild("MaxTimeText").gameObject.GetComponent<Text>().text = maxTime;
        }
        //pause game
        GameManager.Instance.PauseGame ();
	} 

    private string getDay(int day)
    {
        if (day == 0) { return days[0];}
        else if (day == 1) { return days[1]; }
        else if (day == 2) { return days[2]; }
        else if (day == 3) { return days[3]; }
        else if (day == 4) { return days[4]; }
        else if (day == 5) { return days[5]; }
        else if (day >= 6) { return days[6]; }
        return "";
    }

    private string getTime(int time)
    {
        if (time == 0 || time == 1) { return times[0]; }
        else if (time == 2 || time == 3) { return times[1]; }
        else if (time == 4 || time == 5) { return times[2]; }
        return  "";
    }

    // being called by button onclick(), in prefab - acept button.
	public void Acept_Event(){
		GameManager.Instance.StartGame();
		Destroy (gameObject);


//		Destroy (gameObject);
//		//if clicked, resume game
//		GameManager.Instance.StartGame();

	}




}
