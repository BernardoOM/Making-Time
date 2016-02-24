using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public int	viewingWeek { get; private set; }
	public int	curWeek { get; private set; }
	public int	curTotalDay { get; private set; }
	public int	curDayOfWeek { get; private set; }
	public int	curTime { get; private set; }

	//To be canged later
	void Start()
	{
		viewingWeek = 0;
		curWeek = 0;
		curTotalDay = 0;
		curDayOfWeek = 0;
		curTime = 0;

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
	}

	public void DayEnded(int prevDayOfWeek)
	{
		if(prevDayOfWeek == curDayOfWeek)
		{
			curTime = 0;
			curDayOfWeek = (curDayOfWeek+1) % 7;
			curTotalDay += 1;
			if(OnDayStarted != null)
			{	OnDayStarted(curDayOfWeek);	}

			if(curDayOfWeek == 0)
			{	WeekEnded();	}
		}
	}

	public void WeekEnded()
	{
		curWeek += 1;
		viewingWeek = curWeek;
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
		Debug.Log (maxTotalDay);
		Debug.Log ( minTotalDay);
		Debug.Log (maxTime);
		Debug.Log (minTime);

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
		unscheduledCommitments.Add(com);
		scheduledCommitments.Remove(com);

		SortUnScheduled ();
	}

	public int FindIndexScheduled(Commitment com)
	{	return scheduledCommitments.IndexOf(com);	}

	public int FindIndexUnScheduled(Commitment com)
	{	return unscheduledCommitments.IndexOf(com);	}


//	public void swap(List<Commitment>() a[0], List<Commitment>() a[1])
//	{
//		List<Commitment>	temp=new List<Commitment>() ;
//		temp = b;
//		b = a;
//		a = temp;
//
//	}

	public void SortUnScheduled()
	{


		for (int i = 0; i < unscheduledCommitments.Count-1; i++)
			for (int j = 0; j < unscheduledCommitments.Count-1-i; j++) 
			{
				if (unscheduledCommitments [j ].minTotalDay > unscheduledCommitments [j+1].minTotalDay) 
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
				if(unscheduledCommitments [j ].minTotalDay == unscheduledCommitments [j+1].minTotalDay)
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
				if(unscheduledCommitments [j ].minTotalDay == unscheduledCommitments [j+1].minTotalDay)
				if(unscheduledCommitments [j ].maxTotalDay == unscheduledCommitments [j+1].maxTotalDay)
				if (unscheduledCommitments [j ].minTime > unscheduledCommitments [j+1].minTime) 
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
				if(unscheduledCommitments [j ].minTotalDay == unscheduledCommitments [j+1].minTotalDay)
				if(unscheduledCommitments [j ].maxTotalDay == unscheduledCommitments [j+1].maxTotalDay)
				if (unscheduledCommitments [j ].minTime == unscheduledCommitments [j+1].minTime) 
				if (unscheduledCommitments [j ].maxTime > unscheduledCommitments [j+1].maxTime) 
				{
					List<Commitment> temp=new List<Commitment>();
					temp.Add (unscheduledCommitments [j + 1]);

					temp[0]=unscheduledCommitments [j+1];
					unscheduledCommitments [j+1]= unscheduledCommitments [j];
					unscheduledCommitments [j]=temp[0];
				}
			}


		Debug.Log ("sort finished");

		for (int i = 0; i < unscheduledCommitments.Count; i++) {
			//Debug.Log (unscheduledCommitments [i].minTotalDay);
		}


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



	//int maxTotalDay, minTotalDay, maxTime, minTime;
	//com.ReturnTimeRange(out maxTotalDay, out minTotalDay, out maxTime, out minTime);

}
