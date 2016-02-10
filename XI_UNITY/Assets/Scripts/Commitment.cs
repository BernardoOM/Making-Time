using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Commitment : MonoBehaviour
{
	public string	name { get; private set; }
	public string 	creator { get; private set; }
	int		curTotalDay,	curTime,		timeLength,	timeLeft;
	int		maxTotalDay,	minTotalDay,	maxTime,	minTime;
	public bool	scheduled { get; private set; }
	public bool activated { get; private set; }
	public bool	completed { get; private set; }

	// Use this for initialization
	void Start ()
	{
		GameManager.Calendar.OnCheckCommitments += Calendar_OnCheckCommitments;

		completed = false;
	}

	//Constructors
	public void RecordInfo(string aName, string aCreator, int aTimeLength, int aMaxTotalDay,
	                       int aMinTotalDay, int aMaxTime, int aMinTime)
	{
		name = aName;				creator = aCreator;	timeLength = aTimeLength;	maxTotalDay = aMaxTotalDay;
		minTotalDay = aMinTotalDay;	maxTime = aMaxTime;	minTime = aMinTime;			scheduled = false;

		timeLeft = timeLength;
		Text buttonText = GetComponentInChildren<Text>();
		buttonText.text = name;
	}
	public void RecordInfo(string aName, string aCreator, int aCurTotalDay, int aCurTime, int aTimeLength,
	                       int aMaxTotalDay, int aMinTotalDay, int aMaxTime, int aMinTime)
	{
		name = aName;				creator = aCreator;				curTotalDay = aCurTotalDay;	curTime = aCurTime;
		timeLength = aTimeLength;	maxTotalDay = aMaxTotalDay;		minTotalDay = aMinTotalDay;	maxTime = aMaxTime;
		minTime = aMinTime;			scheduled = true;

		Text buttonText = GetComponentInChildren<Text>();
		buttonText.text = name;
	}

	void Calendar_OnCheckCommitments(int aCurTotalDay, int aCurTime)
	{
		if(scheduled)
		{
			if(!activated)
			{
				if(curTotalDay == aCurTotalDay && curTime == aCurTime)
				{
					if(name == "Party")
					{	GameManager.UI.EnterCurrentScene();	}
					activated = true;
					GetComponent<Drag>().Activated();
				}
				else if(aCurTotalDay > maxTotalDay && aCurTime > maxTime && !completed)
				{	GameManager.Calendar.FailedCommitment(this);	}
			}
			else
			{
				timeLeft -= 1;
				if(name == "Party")
				{	GameManager.UI.LeaveCurrentScne();	}
				GameManager.Calendar.CompleteCommitment(this);
				GetComponent<Drag>().Completed();
				completed = true;
				GameManager.Calendar.OnCheckCommitments -= Calendar_OnCheckCommitments;
			}
		}
	}

	public bool CheckScheduleConflict(int totalDay, int time)
	{
		if(scheduled)
		{
			if(curTotalDay == totalDay && curTime == time)
			{	return true;	}
			else
			{	return false;	}
		}
		else
		{	return false;	}
	}

	public void SetPlaceOnSchedule(int newTime, int newTotalDay)
	{
		curTime = newTime;
		curTotalDay = newTotalDay;
	}

	public void SetScheduled(bool isScheduled)
	{
		scheduled = isScheduled;
		//To be changed later
		//write to database here
	}

	public void ReturnTimeRange(out int theMaxTotalDay, out int theMinTotalDay, out int theMaxTime, out int theMinTime)
	{
		theMaxTotalDay = maxTotalDay;
		theMinTotalDay = minTotalDay;
		theMaxTime = maxTime;
		theMinTime = minTime;
	}

	public static void GenerateTestCommitments(int dayOfWeek)
	{
		switch(dayOfWeek)
		{
		case 0:
			GenerateCommitment("Teach Class", "Boss", 2, 0, 0, 5, 4);
			GenerateCommitment("Meeting", "Boss", 2, 2, 0, 3, 2);
			GenerateCommitment("Dinner", "Sarah", 2, 3, 1, 5, 4);
			GenerateCommitment("Go to Gym", "You", 2, 3, 1, 4, 3);
			GenerateCommitment("Grocery Shopping", "You", 2, 5, 2, 4, 3);
			GenerateCommitment("Party", "Sarah", 2, 1, 1, 5, 4);
			break;
		}
	}

	public static void GenerateCommitment()
	{
		GameObject commitment = Instantiate(Resources.Load("Button"))  as GameObject;
		Commitment com = commitment.GetComponent<Commitment>();

		//To be changed later
		//read from database

		// generate values
		GameManager.Calendar.UnScheduleCommitment(com);
		com.transform.parent = GameObject.Find("Deck").transform;
		com.transform.localScale = Vector3.one;
		Drag.PlaceUnscheduled(com);

		//write to database
	}

	public static void GenerateCommitment(string aName, string aCreator, int aTimeLength, int aMaxTotalDay,
	                                      int aMinTotalDay, int aMaxTime, int aMinTime)
	{
		GameObject commitment = Instantiate(Resources.Load("Button"))  as GameObject;
		Commitment com = commitment.GetComponent<Commitment>();

		com.RecordInfo(aName, aCreator, aTimeLength, aMaxTotalDay,
		               aMinTotalDay, aMaxTime, aMinTime);

		GameManager.Calendar.UnScheduleCommitment(com);
		com.transform.parent = GameObject.Find("Deck").transform;
		com.transform.localScale = Vector3.one;
		Drag.PlaceUnscheduled(com);
	}

	//use out then you can change the values of arguments 
	public void readvalues()
	{
	// int arrys= db read function 
		int[] array= DBMakingTime.Select_DB(name, timeLength);
		GameObject.Find ("Managers").GetComponent<PeopleManager> ().ChangePlayerStatus (array [0], array [1]);
		 //+= array [0];
		//player_happiness += array [1];
	}

}


