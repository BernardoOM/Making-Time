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

	//To be canged later
	public List<Commitment>	unscheduledCommitments = new List<Commitment>();
	public List<Commitment>	scheduledCommitments = new List<Commitment>();
	public ClickState			curState;
    private List<Commitment> eventsDay = new List<Commitment>();
    public int currentEventDay=0;
   
    public int	viewingWeek { get; private set; }
	public int	curWeek { get; private set; }
	public int	curTotalDay { get; private set; }
	public int	curDayOfWeek { get; private set; }
	public int	curTime { get; private set; }

    public GameObject pop_window;
	//To be canged later
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

		//To be changed later
		for(int generateCount = 0; generateCount < 7; generateCount += 1)
		{	Commitment.GenerateCommitment(curTotalDay - 1);	}
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
		//delete_past_event ();
		//delete past events on deck
	}


	// activated if window - "start new day" button is clicked. 
	public void DayEnded()
	{
        GameObject.Find("DayEnded").transform.localPosition = new Vector3(-200f, -1000f);
        GameObject.Find("DayEvent").transform.localPosition = new Vector3(-200f, -1000f);
        //delete_past_event();
        currentEventDay = 0;
        curTime = 0;
		curDayOfWeek = (curDayOfWeek+1) % 7;
		curTotalDay += 1;
		if(OnDayStarted != null)
		{	OnDayStarted(curDayOfWeek);	}
		if(curDayOfWeek == 0)
		{	WeekEnded();	}
	}


    public void SummaryDay()
    {
        int totalScheduled = scheduledCommitments.Count;
        int totalUnscheduled = unscheduledCommitments.Count;
        int cuTotalScheduled = 0;
        int cuTotalUnscheduled = 0;
        string day = "";
        int done = 0;
        int missed = 0;
      

        switch (curDayOfWeek)
        {
            case (int)DayofWeek.Sunday:
                day = "Sunday";
                break;
            case (int)DayofWeek.Monday:
                day = "Monday";
                break;
            case (int)DayofWeek.Tuesday:
                day = "Tuesday";
                break;
            case (int)DayofWeek.Wednesday:
                day = "Wednesday";
                break;
            case (int)DayofWeek.Thursday:
                day = "Thursday";
                break;
            case (int)DayofWeek.Friday:
                day = "Friday";
                break;
            case (int)DayofWeek.Saturday:
                day = "Saturday";
                break;
            default:
                break;
        }

        while (cuTotalScheduled < totalScheduled || cuTotalUnscheduled < totalUnscheduled)
        {
            if (cuTotalScheduled < totalScheduled)
            {
                Commitment scheDay = scheduledCommitments[cuTotalScheduled];

                if (scheDay.curTotalDay == curDayOfWeek)
                {
                    eventsDay.Add(scheDay);
                    done++;
                }
                cuTotalScheduled++;
            }

            if (cuTotalUnscheduled < totalUnscheduled)
            {
                Commitment unscheDay = unscheduledCommitments[cuTotalUnscheduled];

                if (unscheDay.maxTotalDay == curDayOfWeek)
                {
                    eventsDay.Add(unscheDay);
                    missed++;
                }
                cuTotalUnscheduled++;
            }
        }
        GameObject.Find("TItleDEnded").GetComponent<Text>().text = day + " ended";
        GameObject.Find("DescriptionDEnded").GetComponent<Text>().text = " You did " + done + " things\n You missed " + missed + " things";
        ///GameObject.Find("FaceDEnded").GetComponent<Image>().sprite = GameObject.

        //Debug.Log("Day: " + day + " You did " + done + " things");
        //Debug.Log("Day: " + day + " You missed " + missed + " things");
    }

    public void CheckEventsDay()
    {
        int totalEvents = eventsDay.Count;

        if (totalEvents == 0)
        {
            GameObject.Find("ButtonDayEnded").SetActive(true);
            GameObject.Find("ButtonDayEvent").SetActive(false);
        }
        else if (totalEvents == 1)
        {
            GameObject.Find("ButtonDayEnded").SetActive(false);
            GameObject.Find("ButtonDayEvent").SetActive(true);
            GameObject.Find("ButtonDEnded").SetActive(true);
            GameObject.Find("ButtonDEvent").SetActive(false);
            GameObject.Find("BackBDLastEvent").SetActive(false);
        }
        else if (totalEvents == 2)
        {
            GameObject.Find("ButtonDayEnded").SetActive(false);
            GameObject.Find("ButtonDayEvent").SetActive(true);
            GameObject.Find("ButtonDEnded").SetActive(true);
            GameObject.Find("ButtonDEvent").SetActive(false);
            GameObject.Find("BackBDLastEvent").SetActive(true);
        }
    }

    public void DayEvent()
    {
        GameObject.Find("DayEnded").transform.localPosition = new Vector3(-200f, -1000f);
        GameObject.Find("DayEvent").transform.localPosition = new Vector3(62f, -7f);

        if(currentEventDay < eventsDay.Count)
        {
            int doubleCheck = currentEventDay + 2;

            if (eventsDay[currentEventDay].scheduled){
                ShowNextEvent("Done");
            }
            else{
                ShowNextEvent("Missed");
            }

            if (doubleCheck == eventsDay.Count ) //eventsDay.Count - 1
            {
                GameObject.Find("ButtonDEnded").SetActive(true);
                GameObject.Find("ButtonDEvent").SetActive(false);
                
                if(doubleCheck == 1) {
                   GameObject.Find("BackBDLastEvent").SetActive(false);
                }
                else if(doubleCheck > 1)
                {
                   GameObject.Find("BackBDLastEvent").SetActive(true);
                }
            }

        }
    }

    public void ShowNextEvent(string DoM)
    {
        // GameObject.Find("CreatorDEvent") creator image
         GameObject.Find("NameDEvent").GetComponent<Text>().text = eventsDay[currentEventDay].name;
         GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "Your " + eventsDay[currentEventDay].creator + " is happy with you";
        //    GameObject.Find("AchieveDEvent") done or missed image
         GameObject.Find("TextAchievedDEvent").GetComponent<Text>().text = DoM;
         currentEventDay++;
    }

    public void ShowPreviewEvent(string DoM)
    {
        currentEventDay = currentEventDay - 1;
        // GameObject.Find("CreatorDEvent") creator image
        GameObject.Find("NameDEvent").GetComponent<Text>().text = eventsDay[currentEventDay].name;
        GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "Your " + eventsDay[currentEventDay].creator + " is happy with you";
        //    GameObject.Find("AchieveDEvent") done or missed image
        GameObject.Find("TextAchievedDEvent").GetComponent<Text>().text = DoM;

        if(currentEventDay == 1)
        {
            GameObject.Find("ButtonDEvent").SetActive(true);
            GameObject.Find("BackBDLastEvent").SetActive(false);
        }
        else if (currentEventDay > 1 && currentEventDay != eventsDay.Count - 1)
        {
            GameObject.Find("ButtonDEvent").SetActive(true);
            GameObject.Find("BackBDLastEvent").SetActive(true);
        }
        else if (currentEventDay == eventsDay.Count - 1)
        {
            GameObject.Find("ButtonDEnded").SetActive(true);
            GameObject.Find("ButtonDEvent").SetActive(false);
            GameObject.Find("BackBDLastEvent").SetActive(true);
        }

    }

    //	public void DayEnded(int prevDayOfWeek)
    //	{
    //		if(prevDayOfWeek == curDayOfWeek)
    //		{
    //			curTime = 0;
    //			curDayOfWeek = (curDayOfWeek+1) % 7;
    //			curTotalDay += 1;
    //			if(OnDayStarted != null)
    //			{	OnDayStarted(curDayOfWeek);	}
    //
    //			if(curDayOfWeek == 0)
    //			{	WeekEnded();	}
    //		}
    //
    //
    //	}


    public void WeekEnded()
	{
		curWeek += 1;
		viewingWeek = curWeek;

		delete_lastweek_calendar ();
		//delete_lastweek_calendar
	}

	public void CompleteCommitment(Commitment sender)
	{
		//To be changed later
		//GameManager.People.ChangePlayerStatus(0, 0);
		sender.readValues ();

	}

	public void FailedCommitment(Commitment sender)
	{
		//To be changed later
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


	public void acept_social_event(){
		for (int i = 0; i < unscheduledCommitments.Count; i++) {
			if (unscheduledCommitments [i].curType == CommitmentType.Social) {
				GameManager.UI.Acept_Window(unscheduledCommitments[i],i);
			}
		}
	}

	public void refuse_social(int i){
		Destroy (unscheduledCommitments [i].gameObject);
		unscheduledCommitments.RemoveAt (i);
	}

	//delete past events on deck
	public void delete_past_event(){
	for (int i = 0; i < unscheduledCommitments.Count; i++) {
			while (unscheduledCommitments[i].maxTotalDay <= curDayOfWeek && unscheduledCommitments[i].maxTime<curTime )
		{
				//GameObject.Find ("Bubble_text").GetComponent<Text>().text ="Oh you missed "+ unscheduledCommitments [i].name + " !";

			unscheduledCommitments[i].gameObject.SetActive(false);
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
			scheduledCommitments [i].gameObject.SetActive (false);
			scheduledCommitments.RemoveAt (i);
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
