using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum CommitmentType {Work, Leisure, Social, Chore };

public class Commitment : MonoBehaviour
{
	public CommitmentType curType;
	public string	name;
	public string creator;
	public int		curTotalDay,	curTime,		timeLength,	timeLeft;
	public int		maxTotalDay,	minTotalDay,	maxTime,	minTime;
	public bool	scheduled;
	public bool activated;
	public bool	completed;
	bool		activeScene;

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
					if(DBMakingTime.CheckHasScene(name, timeLength))
					{
						GameManager.UI.EnterCurrentScene(curTotalDay % 7, name);
						activeScene = true;
					}
					activated = true;
					GetComponent<Drag>().Activated();
				}
				else if(aCurTotalDay > maxTotalDay && aCurTime > maxTime && !completed)
				{	GameManager.Calendar.FailedCommitment(this);	}
			}
			else
			{
				timeLeft -= 1;
				if(activeScene)
				{	GameManager.UI.LeaveCurrentScne(curTotalDay % 7);	}
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

	public static void GenerateCommitment(int totalDay)
	{
		GameObject commitment;
		string aName = "";	string aCreator = "";
		int aTimeLength = 0; int aMaxTotalDay = 0; int aMinTotalDay = 0; int aMaxTime = 0; int aMinTime = 0;

		CommitmentType newCommitment;

		int randomCommitmentType = Random.Range(0, 10);
		if(randomCommitmentType <= 4)
		{	newCommitment = CommitmentType.Work;	}
		else if(randomCommitmentType <= 7)
		{	newCommitment = CommitmentType.Social;	}
		else
		{	newCommitment = CommitmentType.Chore;	}

		switch(newCommitment)
		{
		case CommitmentType.Work:
			commitment = Instantiate(Resources.Load("WorkButton")) as GameObject;
			DBMakingTime.ReadRandomNewCommitment("Work", ref aName, ref aTimeLength, ref aMaxTime, ref aMinTime);
			break;
		case CommitmentType.Leisure:
			commitment = Instantiate(Resources.Load("LeisureButton")) as GameObject;
			DBMakingTime.ReadRandomNewCommitment("Leisure", ref aName, ref aTimeLength, ref aMaxTime, ref aMinTime);
			break;
		case CommitmentType.Social:
			commitment = Instantiate(Resources.Load("SocialButton")) as GameObject;
			DBMakingTime.ReadRandomNewCommitment("Social", ref aName, ref aTimeLength, ref aMaxTime, ref aMinTime);
			break;
		case CommitmentType.Chore:
			commitment = Instantiate(Resources.Load("ChoreButton")) as GameObject;
			DBMakingTime.ReadRandomNewCommitment("Chore", ref aName, ref aTimeLength, ref aMaxTime, ref aMinTime);
			break;
		default:
			commitment = Instantiate(Resources.Load("Button")) as GameObject;
			break;
		}
		Commitment com = commitment.GetComponent<Commitment>();

		switch((CommitmentType)Random.Range(0, 4))
		{
		case CommitmentType.Work:
			if(aName == "Teach Class")
			{
				aMinTime = Random.Range(0, 2) * 2;
				aMaxTime = aMinTime + 1;
			}
			aMinTotalDay = totalDay + Random.Range(0, 5);
			//Random.Range(2, 5);
			aMaxTotalDay = aMinTotalDay;
			break;
		case CommitmentType.Leisure:
			aMinTotalDay = totalDay +2;
			//+2
			aMaxTotalDay = aMinTotalDay + Random.Range(0, 7);
			break;
		case CommitmentType.Social:
			aMinTotalDay = totalDay + 1;
			aMaxTotalDay = aMinTotalDay + Random.Range(0, 3);
			break;
		case CommitmentType.Chore:
			aMinTotalDay = totalDay + 2;
			aMaxTotalDay = aMinTotalDay + Random.Range(0, 5);
			break;
		}

		com.RecordInfo(aName, aCreator, aTimeLength, aMaxTotalDay,
		               aMinTotalDay, aMaxTime, aMinTime);
		com.curType = newCommitment;

		//To be changed later
		//read from database

		// generate values
		GameManager.Calendar.UnScheduleCommitment(com);
		com.transform.SetParent(GameObject.Find("Deck").transform, false);
//		com.transform.localScale = Vector3.one;
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
	public void readValues()
	{
	// int arrys= db read function 
		int[] array= DBMakingTime.ChangeStatus(name, timeLength);
		GameObject.Find ("Managers").GetComponent<PeopleManager> ().ChangePlayerStatus (array [0], array [1]);
		 //+= array [0];
		//player_happiness += array [1];
	}

}


