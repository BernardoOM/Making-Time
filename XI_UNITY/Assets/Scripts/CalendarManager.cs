using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum ClickState { NoFocus, CommitmentFocus, MessageBoxFocus };

public class CalendarManager : MonoBehaviour
{
	public delegate void			TimeHandler(int prevTimeValue);
	public delegate void			CommitmentHandler(int curTotalDay, int curTime);
	public delegate void			ClickHandler();
	public event TimeHandler		OnDayStarted;
	public event CommitmentHandler	OnCheckCommitments;
	public event ClickHandler		OnCommitmentClicked;
	public event ClickHandler		OnWindow;

	//To be canged later
	public List<Commitment>	unscheduledCommitments = new List<Commitment>();
	public List<Commitment>	scheduledCommitments = new List<Commitment>();
	public ClickState			curState;

   
    public int	viewingWeek { get; private set; }
	public int	curWeek { get; private set; }
	public int	curTotalDay { get; private set; }
	public int	curDayOfWeek { get; private set; }
	public int	curTime { get; private set; }

    public GameObject pop_window;
	//To be canged later

	public int fastforward=1;
	//fastforwar is used on pause window fastforward button
	//when clicked, the timer will accelerate 

	public bool tutorial=true;
	public GameObject tutorial_circle;
	//tutorial circle object 

	void Start()
	{
		viewingWeek = 0;
		curWeek = 0;
		curTotalDay = 0;
		curDayOfWeek = 0;
		curTime = 0;
       // hasEventDone = true;
       // hasEventMissed = false;

        curState = ClickState.NoFocus;

		if(ApplicationModel.tutorialEvent == "Party")
		{	Commitment.GenerateCommitment("Party", "", 1, 0, 0, 3, 3);	}
		else
		{	Commitment.GenerateCommitment("Dinner", "", 1, 0, 0, 3, 3);	}

 		//generate work events at begining. 5-15 work events. 1-3 per day. mon to friday. 
		for(int generateCount = 1; generateCount < 6; generateCount += 1)
		{	
			int event_numbs=Random.Range (1, 4);
			for (int i = 0; i < event_numbs; i++) {
				Commitment.Generate_Works (generateCount);
			}

		}
	}

	public void TimeBlockStarted()
	{
		curTime += 1;
		if(OnCheckCommitments != null)
		{	OnCheckCommitments(curTotalDay, curTime);	}
		if(curTime % 2 == 1)
		{
			//To be changed later
			Commitment.GenerateCommitment(curTotalDay);
		}

		if(curTime == 2)
		{
			//change to afternoon
		}
		else if(curTime == 4)
		{
			//change to evening
		}
		//delete_past_event ();
		//delete past events on deck
	}


	// activated if window - "start new day" button is clicked. 
	public void DayEnded()
	{
		GameObject.Find("Calendar").GetComponent<DailyReview>().ClearDay();
		GameManager.People.ChangePlayerStatus(2, 2);
		//delete_past_event();
		curTime = 0;
		curDayOfWeek = (curDayOfWeek+1) % 7;
		curTotalDay += 1;
		if(OnDayStarted != null)
		{	OnDayStarted(curDayOfWeek);	}
		if(curDayOfWeek == 0)
		{	WeekEnded();	}

        if (OnCheckCommitments != null)
        { OnCheckCommitments(curTotalDay, curTime); }

		//change to morning
    }

    public void WeekEnded()
	{
		curWeek += 1;
		viewingWeek = curWeek;

		delete_lastweek_calendar ();
		curDayOfWeek = 0;
		curTotalDay = 0;
		//delete_lastweek_calendar

		//generate work events at begining. 5-15 work events. 1-3 per day. mon to friday. 
		for(int generateCount = 1; generateCount < 6; generateCount += 1)
		{	
			int event_numbs=Random.Range (1, 4);
			for (int i = 0; i < event_numbs; i++) {
				Commitment.Generate_Works (generateCount);
			}

		}
	}

	public void WindowEvent()
	{	OnWindow();	}

	public void CompleteCommitment(Commitment sender)
	{
		//To be changed later
		//GameManager.People.ChangePlayerStatus(0, 0);
		GameObject.Find("Calendar").GetComponent<DailyReview>().AddEventToReview(sender);
		sender.readValues ();
	}

	public void FailedCommitment(Commitment sender)
	{
		RemoveCommitment(sender);
		GameObject.Find("Calendar").GetComponent<DailyReview>().AddEventToReview(sender);
		Drag.ShiftDeck();
	}

	public void RemoveCommitment(Commitment com)
	{
		unscheduledCommitments.Remove(com);
		Drag.ShiftDeck();
	}

	public void CommitmentClicked()
	{
		switch(curState)
		{
		case ClickState.NoFocus:
			curState = ClickState.CommitmentFocus;
			if(OnCommitmentClicked != null)
			{	OnCommitmentClicked();	}
			break;
		case ClickState.CommitmentFocus:
			if(OnCommitmentClicked != null)
			{	OnCommitmentClicked();	}
			break;
		case ClickState.MessageBoxFocus:
			break;
		}
	}

	public void NoFocus()
	{
		curState = ClickState.NoFocus;

		GameManager.UI.SetDragArea(-1, 0, -1, 0);
	}

	public void CommitmentFocus(Commitment com)
	{
		curState = ClickState.CommitmentFocus;

		int maxTotalDay, minTotalDay, maxTime, minTime;
		com.ReturnTimeRange(out maxTotalDay, out minTotalDay, out maxTime, out minTime);
//		Debug.Log (maxTotalDay);
//		Debug.Log ( minTotalDay);
//		Debug.Log (maxTime);
//		Debug.Log (minTime);

		GameManager.UI.SetDragArea(maxTotalDay, minTotalDay, maxTime, minTime);
	}

	public bool CheckCalendarSpace(int totalDay, int time)
	{
		if(curTotalDay > totalDay || (curTotalDay == totalDay && curTime >= time))
		{	return false;	}

		foreach(Commitment com in scheduledCommitments)
		{
			if(com.CheckScheduleConflict(totalDay, time))
			{	return false;	}
		}

		return true;
	}

	public void ScheduleCommitment(Commitment com)
	{
		unscheduledCommitments.Remove(com);
		scheduledCommitments.Add(com);
	}

	public void UnScheduleCommitment(Commitment com)
	{
		//GameManager.PauseGame ();
		unscheduledCommitments.Add(com);

		scheduledCommitments.Remove(com);

		//display a window ask player accept or refuse a social event 
		//acept_social_event ();
		SortUnScheduled ();
	}

	public int FindIndexScheduled(Commitment com)
	{	return scheduledCommitments.IndexOf(com);	}

	public int FindIndexUnScheduled(Commitment com)
	{	return unscheduledCommitments.IndexOf(com);	}

	//display inviattation window 
	public void acept_social_event(Commitment com){
		if (com.curType == CommitmentType.Social) {
			GameObject	acept_prefab =Instantiate(Resources.Load("Window_Accept_Social"),new Vector3(0, 0, 0),Quaternion.identity) as GameObject;
			acept_prefab.GetComponent<Social_Acceptance> ().Acept_Window (com);
		}
	}

	//being called if player clicked on refuse button from the inviation window. 
	public void refuse_social(Commitment com){
		for (int i = 0; i < unscheduledCommitments.Count; i++) {
			if (com == unscheduledCommitments [i]) {
				unscheduledCommitments [i].gameObject.SetActive(false);
				unscheduledCommitments.RemoveAt (i);
				Drag.ShiftDeck ();

			}
		}
	}

	//delete past events on deck
	public void delete_past_event(){
	for (int i = 0; i < unscheduledCommitments.Count; i++) {
			while (unscheduledCommitments[i].maxTotalDay <= curDayOfWeek && unscheduledCommitments[i].maxTime<curTime )
		{
				//GameObject.Find ("Bubble_text").GetComponent<Text>().text ="Oh you missed "+ unscheduledCommitments [i].name + " !";

			unscheduledCommitments[i].gameObject.SetActive(false);
				//scheduledCommitments[i].gameObject.SetActive(false);
			unscheduledCommitments.RemoveAt (i);

		//GameObject.Find ("Bubble_Calendar").transform.localPosition = new Vector3 (-444.3f, 315.7f);

		}
	}

		Drag.ShiftDeck();
	}

	//activated if clicked on bubble 
	public void delete_bubble(){
		GameObject.Find ("Bubble_Calendar").transform.localPosition = new Vector3 (-444.3f, -1000.7f);

	}


	//delete_lastweek_calendar
	public void delete_lastweek_calendar(){

		for (int i = 0; i < scheduledCommitments.Count; i++) {
			//scheduledCommitments [i].gameObject.SetActive (false);
			Destroy (scheduledCommitments [i].gameObject);
			//scheduledCommitments.RemoveAt (i);
		}
		scheduledCommitments.Clear ();

	}

	public void Tutorial(Commitment com)
	{
		if (tutorial == true) {
			 tutorial_circle = Instantiate (Resources.Load ("Sprites/Tutorial_circle")) as GameObject;
			tutorial_circle.transform.SetParent (com.transform,false);
			tutorial_circle.transform.localPosition = new Vector3 (77.1f, -41.5f, 0);
			tutorial = false;

		}

	}

	public void SortUnScheduled()
	{

		for (int i = 0; i < unscheduledCommitments.Count-1; i++)
			for (int j = 0; j < unscheduledCommitments.Count-1-i; j++) 
			{
				if (unscheduledCommitments [j ].maxTotalDay > unscheduledCommitments [j+1].maxTotalDay) 
				{
					List<Commitment> temp=new List<Commitment>();
					temp.Add (unscheduledCommitments [j + 1]);

					temp[0]=unscheduledCommitments [j+1];
					unscheduledCommitments [j+1]= unscheduledCommitments [j];
					unscheduledCommitments [j]=temp[0];

				}
			}		

		for (int i = 0; i < unscheduledCommitments.Count-1; i++)
			for (int j = 0; j < unscheduledCommitments.Count-1-i; j++) 
			{
				if(unscheduledCommitments [j ].maxTotalDay == unscheduledCommitments [j+1].maxTotalDay)
				if (unscheduledCommitments [j ].maxTime > unscheduledCommitments [j+1].maxTime) 
				{
					List<Commitment> temp=new List<Commitment>();
					temp.Add (unscheduledCommitments [j + 1]);

					temp[0]=unscheduledCommitments [j+1];
					unscheduledCommitments [j+1]= unscheduledCommitments [j];
					unscheduledCommitments [j]=temp[0];
				}
			}


		for (int i = 0; i < unscheduledCommitments.Count-1; i++)
			for (int j = 0; j < unscheduledCommitments.Count-1-i; j++) 
			{
				if(unscheduledCommitments [j ].maxTotalDay == unscheduledCommitments [j+1].maxTotalDay)
				if (unscheduledCommitments [j ].maxTime == unscheduledCommitments [j+1].maxTime)
				if (unscheduledCommitments [j].minTotalDay > unscheduledCommitments [j+1].minTotalDay) 
				{
					List<Commitment> temp=new List<Commitment>();
					temp.Add (unscheduledCommitments [j + 1]);
					temp[0]=unscheduledCommitments [j+1];
					unscheduledCommitments [j+1]= unscheduledCommitments [j];
					unscheduledCommitments [j]=temp[0];				}
			}
		


		for (int i = 0; i < unscheduledCommitments.Count-1; i++)
			for (int j = 0; j < unscheduledCommitments.Count-1-i; j++) 
			{
				if(unscheduledCommitments [j ].maxTotalDay == unscheduledCommitments [j+1].maxTotalDay)
				if (unscheduledCommitments [j ].maxTime == unscheduledCommitments [j+1].maxTime)
				if (unscheduledCommitments [j].minTotalDay == unscheduledCommitments [j+1].minTotalDay) 
				if (unscheduledCommitments [j ].minTime > unscheduledCommitments [j+1].minTime) 
				{
					List<Commitment> temp=new List<Commitment>();
					temp.Add (unscheduledCommitments [j + 1]);

					temp[0]=unscheduledCommitments [j+1];
					unscheduledCommitments [j+1]= unscheduledCommitments [j];
					unscheduledCommitments [j]=temp[0];
				}
			} 



        //Debug.Log ("sort finished");
        //Debug.Log (curDayOfWeek);


        //for (int i = 0; i < unscheduledCommitments.Count; i++)
        //{
        //    Debug.Log(unscheduledCommitments[i].minTotalDay);
        //}


        //1
        //new list
        //for each following:

//		int maxTotalDay, minTotalDay, maxTime, minTime;
//		unscheduledCommitments[0].ReturnTimeRange (maxTotalDay, minTotalDay, maxTime, minTime);

// new array = maxTotalDay, minTotalDay, maxTime, minTime ; 


//store them in a new array / list. not decided yet. them sort. 

//2
//quick sort

//3
//display the sorted deck.
//		Debug.Log ("current counts of unschdle:");
//
//		Debug.Log (unscheduledCommitments.Count);

//		for (int i=0;i<unscheduledCommitments.Count;i++) {
//			Debug.Log ("current max time");
//			Debug.Log (unscheduledCommitments[i].name);
//
//			Debug.Log (unscheduledCommitments[i].maxTime);
//
//		}


	}
}
