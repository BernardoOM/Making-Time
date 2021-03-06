﻿using UnityEngine;
//using System;

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public enum CommitmentType {Work, Leisure, Social, Chore };

public class Commitment : MonoBehaviour
{
	public CommitmentType curType;
	public string	name;
	public string	creator;
	public int		curTotalDay,	curTime,		timeLength,	timeLeft;
	public int		maxTotalDay,	minTotalDay,	maxTime,	minTime;
	public bool		scheduled;
	public bool		activated;
	public bool		completed;

	private static int	startX = -494;
	private static int	calendarStartY = 272;
	private static int	deckY = -290;
	private static int	blockWidth = 163;
	private static int	blockHeight = 88;

	private Object[]	events;
	private string		timePath = "Sprites/Postits";
	private bool		dragSave = false;
	//variables for change colors of each event button. acctivated and done.

	// Use this for initialization
	void Start ()
	{
		GameManager.Calendar.OnCheckCommitments += Calendar_OnCheckCommitments;
		GameManager.Calendar.OnWindow += GameManager_Calendar_OnWindow;

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
					{	GameManager.UI.EnterCurrentScene(curTotalDay % 7, name);	}
					activated = true;
					if (curType != CommitmentType.Work) {
						GetComponent<Drag> ().Activated ();
					}

					//change event color to activated. 
						events = Resources.LoadAll(timePath, typeof(Sprite));
						for (int i = 0; i < events.Length; i++)
						{
						if ((int)curType==3 && events[i].name == "Chore_Postit_active") { GetComponent<Image>().sprite = (Sprite)events[i]; }
						else if ((int)curType==2 &&events[i].name == "Social_Postit_active") { GetComponent<Image>().sprite = (Sprite)events[i]; }
						else if ((int)curType==0 &&events[i].name == "Work_Postit_active") { GetComponent<Image>().sprite= (Sprite)events[i]; }
						}

					GameObject.Find ("Main Camera").GetComponent<AudioManager> ().event_play();

					//activation sound
				}


			}
			else
			{
				timeLeft -= 1;
				GameManager.Calendar.CompleteCommitment(this);
				if (curType != CommitmentType.Work) {
					GetComponent<Drag> ().Completed ();
				}
				completed = true;
				GameManager.Calendar.OnCheckCommitments -= Calendar_OnCheckCommitments;
				GameManager.Calendar.OnWindow -= GameManager_Calendar_OnWindow;

				//change event color to done. 
				events = Resources.LoadAll(timePath, typeof(Sprite));
				for (int i = 0; i < events.Length; i++)
				{
					if ((int)curType==3 && events[i].name == "Chore_Postit_done") { GetComponent<Image>().sprite = (Sprite)events[i]; }
					else if ((int)curType==2 &&events[i].name == "Social_Postit_done") { GetComponent<Image>().sprite = (Sprite)events[i]; }
					else if ((int)curType==0 &&events[i].name == "Work_Postit_done") { GetComponent<Image>().sprite= (Sprite)events[i]; }
				}

			}

		}
		else
		{
			if (aCurTotalDay >= maxTotalDay && aCurTime >= maxTime)
			{
				Debug.Log(aCurTotalDay + " " + maxTotalDay + " " + aCurTime + " " + maxTime);
				GameManager.Calendar.FailedCommitment(this);
				gameObject.SetActive(false);
				GameManager.Calendar.OnCheckCommitments -= Calendar_OnCheckCommitments;
				GameManager.Calendar.OnWindow -= GameManager_Calendar_OnWindow;

				//do delete 
				//do feedback
			}
		}
	}

	void GameManager_Calendar_OnWindow()
	{
		Drag dragScript = GetComponent<Drag>();
		if(dragScript.enabled)
		{
			dragSave = dragScript.enabled;
			dragScript.enabled = false;
		}
		else
		{	dragScript.enabled = dragSave;	}
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

		// no work events generated 
		int randomCommitmentType = Random.Range(0, 10);
         if(randomCommitmentType <=4)
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

		//Debug.Log ((CommitmentType)Random.Range (0, 4));
		//switch((CommitmentType)Random.Range(0, 4))
		switch(newCommitment)
		{
		case CommitmentType.Work:
			if(aName == "Teach Class")
			{
				aMinTime = Random.Range(0, 2) * 2;
				aMaxTime = aMinTime + 1;
			}
			aMinTotalDay = totalDay + Random.Range(2, 5);
			//Random.Range(2, 5);
			aMaxTotalDay = aMinTotalDay;
			break;
		case CommitmentType.Leisure:
			aMinTotalDay = totalDay +2;
			//+2
			aMaxTotalDay = aMinTotalDay + Random.Range(0, 7);
			break;
		case CommitmentType.Social:
			//			aMinTotalDay = totalDay + 1;
			aMinTotalDay = totalDay;
			aMaxTotalDay = aMinTotalDay + Random.Range(0, 3);
			break;
		case CommitmentType.Chore:
			//			aMinTotalDay = totalDay + 2;
			aMinTotalDay = totalDay;
			aMaxTotalDay = aMinTotalDay + Random.Range(0, 5);
			break;
		}

		com.RecordInfo(aName, aCreator, aTimeLength, aMaxTotalDay,
		               aMinTotalDay, aMaxTime, aMinTime);
		com.curType = newCommitment;

		//To be changed later
		//read from database

		GameManager.Calendar.UnScheduleCommitment (com);
		com.transform.SetParent (GameObject.Find ("Deck").transform, false);
		//		com.transform.localScale = Vector3.one;
		Drag.PlaceUnscheduled (com);
		//write to database

		GameManager.Calendar.acept_social_event (com);
		GameManager.Calendar.acept_chore_event (com);

		//display inviatation window for social events. code is in calendar.cs

		//GameManager.Calendar.Tutorial (com);
		//generate  tutorial circle for first event 
	}


	public static void Generate_Works(int totalDay)
	{
		GameObject commitment;
		string aName = "";	string aCreator = "";
		int aTimeLength = 0; int aMaxTotalDay = 0; int aMinTotalDay = 0; int aMaxTime = 0; int aMinTime = 0;

		CommitmentType newCommitment;
	newCommitment = CommitmentType.Work;

		switch(newCommitment)
		{
		case CommitmentType.Work:
			commitment = Instantiate(Resources.Load("WorkButton")) as GameObject;
			DBMakingTime.ReadRandomNewCommitment("Work", ref aName, ref aTimeLength, ref aMaxTime, ref aMinTime);
			break;
		
		default:
			commitment = Instantiate(Resources.Load("Button")) as GameObject;
			break;
		}
		Commitment com = commitment.GetComponent<Commitment>();

//			if(aName == "Teach Class")
//			{
//				aMinTime = Random.Range(0, 2) * 2;
//				aMaxTime = aMinTime + 1;
//			}
		aMinTotalDay = 0 ;
		aMaxTotalDay = 6;


		com.RecordInfo(aName, aCreator, aTimeLength, aMaxTotalDay,
			aMinTotalDay, aMaxTime, aMinTime);
		com.curType = newCommitment;

		//com.GetComponent<Image> ().color = new Color (.90f, .49f, .22f);

		int random_time = Random.Range (aMinTime, aMaxTime + 1);
		int random_day = totalDay; 
		// not random day. monday to friday. based on cldr.cs start();


		//####### OVERLAP AVOIDANCE function.#do not delete#. not perfect.
//		List<int> pool=new List<int>();
//		for (int i = 0; i < aMaxTime - aMinTime+1; i++) {
//			pool.Add (aMinTime + i);
//		}
//
//		for (int i = 0; i < GameManager.Calendar.scheduledCommitments.Count; i++) {
//			//while (GameManager.Calendar.scheduledCommitments [i].curTime == random_time)
//			if (GameManager.Calendar.scheduledCommitments [i].curTotalDay == random_day) {
//				while (GameManager.Calendar.scheduledCommitments [i].curTime == random_time) {
//					int temp_time = GameManager.Calendar.scheduledCommitments [i].curTime;
//					pool.Remove (temp_time);
//					random_time = Random.Range (pool[0],pool[pool.Count-1]+1);
//				}
//			}
//		}
//

		//not perfect too 
		//events overlap avoidance 
		for (int i = 0; i < GameManager.Calendar.scheduledCommitments.Count; i++) {
					if (GameManager.Calendar.scheduledCommitments [i].curTotalDay == random_day) {
						while (GameManager.Calendar.scheduledCommitments [i].curTime == random_time) {
							int temp_time = GameManager.Calendar.scheduledCommitments [i].curTime;
					random_time = Random.Range (aMinTime,aMaxTime+1);
						}
					}
				}
		



		com.SetPlaceOnSchedule(random_time, random_day);
		com.SetScheduled(true);
		GameManager.Calendar.ScheduleCommitment(com);
		com.transform.SetParent(GameObject.Find("Scheduled").transform,false);

		com.transform.localPosition
		= new Vector3(startX + (random_day* blockWidth), calendarStartY - (random_time * blockHeight), 0);
		// Random.Range(aMinTime,aMaxTime+1), Random.Range(aMinTotalDay,aMaxTotalDay+1)
	}

	public static void GenerateCommitment(string aName, string aCreator, int aTimeLength, int aMaxTotalDay,
	                                      int aMinTotalDay, int aMaxTime, int aMinTime)
	{
		GameObject commitment;
		CommitmentType aType = CommitmentType.Social;
		string type = DBMakingTime.CheckEventType(aName);

		switch(type)
		{
		case "Work":
			commitment = Instantiate(Resources.Load("WorkButton")) as GameObject;
			aType = CommitmentType.Work;
			break;
		case "Leisure":
			commitment = Instantiate(Resources.Load("LeisureButton")) as GameObject;
			aType = CommitmentType.Leisure;
			break;
		case "Social":
			commitment = Instantiate(Resources.Load("SocialButton")) as GameObject;
			aType = CommitmentType.Social;
			break;
		case "Chore":
			commitment = Instantiate(Resources.Load("ChoreButton")) as GameObject;
			aType = CommitmentType.Chore;
			break;
		default:
			commitment = Instantiate(Resources.Load("Button")) as GameObject;
			break;
		}
		Commitment com = commitment.GetComponent<Commitment>();

		com.RecordInfo(aName, aCreator, aTimeLength, aMaxTotalDay,
		               aMinTotalDay, aMaxTime, aMinTime);
		com.curType = aType;

		GameManager.Calendar.UnScheduleCommitment(com);
		com.transform.parent = GameObject.Find("Deck").transform;
		Drag.PlaceUnscheduled(com);
	}

	//use out then you can change the values of arguments 
	public void readValues()
	{
		// int arrys= db read function 
		int[] array= DBMakingTime.ChangeStatus(name, timeLength);
		GameManager.People.ChangePlayerStatus (array [0], array [1]);
		//+= array [0];
		//player_happiness += array [1];

		//change bgm based on current values 
		GameManager.People.switch_bgm();

		//below code is to show face on each event buttons. based on Database energy, happy values. 
		string path="Faces_on_events/face00";

		if (array [0] == 0) {
			if (array [1] == 0) {
				 path="Faces_on_events/face00";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().neu_sfx_play ();
			}
			if (array [1] > 0) {
				 path="Faces_on_events/face30";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().pos_sfx_play ();

			}
			if (array [1] < 0) {
				 path="Faces_on_events/face-30";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().neg_sfx_play ();

			}
		}

		if (array [0] > 0) {
			if (array [1] == 0) {
				 path="Faces_on_events/face03";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().pos_sfx_play ();

			}
			if (array [1] > 0) {
				 path="Faces_on_events/face33";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().pos_sfx_play ();

			}
			if (array [1] < 0) {
				 path="Faces_on_events/face-33";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().neu_sfx_play ();

			}
		}	
		if (array [0] < 0) {
			if (array [1] == 0) {
				 path="Faces_on_events/face0-3";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().neg_sfx_play ();

			}
			if (array [1] > 0) {
				 path="Faces_on_events/face03";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().neu_sfx_play ();

			}
			if (array [1] < 0) {
				 path="Faces_on_events/face-3-3";
				GameObject.Find ("Main Camera").GetComponent<AudioManager> ().neg_sfx_play ();

			}
		}


		GameObject face = Instantiate(Resources.Load(path),transform.parent.position, transform.parent.rotation) as GameObject;
		face.transform.parent = transform.parent;
		face.transform.localPosition 
		= new Vector3(startX + (curTotalDay* blockWidth)+blockWidth/2, calendarStartY - (curTime * blockHeight)-blockHeight/2, 0);
		face.transform.parent = transform;
	}

}


