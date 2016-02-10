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
	private List<Commitment>	unscheduledCommitments = new List<Commitment>();
	private List<Commitment>	scheduledCommitments = new List<Commitment>();
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
		Commitment.GenerateTestCommitments(curDayOfWeek);
	}

	public void TimeBlockStarted()
	{
		curTime += 1;
		OnCheckCommitments(curTotalDay, curTime);
	}

	public void DayEnded(int prevDayOfWeek)
	{
		if(prevDayOfWeek == curDayOfWeek)
		{
			curTime = 0;
			curDayOfWeek = (curDayOfWeek+1) % 7;
			curTotalDay += 1;
			OnDayStarted(curDayOfWeek);

			//To be changed later
			Commitment.GenerateTestCommitments(curDayOfWeek);

			if(curDayOfWeek == 0)
			{	WeekEnded();	}
		}
	}

	public void WeekEnded()
	{
		curWeek += 1;
		viewingWeek = curWeek;
	}

	public void ActivateCommitment(Commitment sender)
	{
		//To be changed later
		//GameManager.People.ChangePlayerStatus(0, 0);
		sender.readvalues ();

		Debug.Log("WE GOT IT");
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
			OnCommitmentClicked();
			break;
		case ClickState.CommitmentFocus:
			OnCommitmentClicked();
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
	}

	public int FindIndexScheduled(Commitment com)
	{	return scheduledCommitments.IndexOf(com);	}

	public int FindIndexUnScheduled(Commitment com)
	{	return unscheduledCommitments.IndexOf(com);	}
}
