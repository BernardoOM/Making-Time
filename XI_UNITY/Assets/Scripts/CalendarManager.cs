using UnityEngine;
using System.Collections;

public class CalendarManager : MonoBehaviour
{
	public delegate void			TimeEvent(int prevTimeValue);
	public delegate void			CommitmentEvent(int curDay, int curTime);
	public event TimeEvent			OnDayStarted;
	public event CommitmentEvent	OnCheckCommitments;

	//To be canged later
	public Timer[]		timers;
	public Commitment[]	unscheduledCommitments;
	public Commitment[]	scheduledCommitments;

	public int	viewingWeek { get; private set; }
	public int	curWeek { get; private set; }
	public int	curDay { get; private set; }
	public int	curTime { get; private set; }

	//To be canged later
	void Start()
	{
		viewingWeek = 0;
		curWeek = 0;
		curDay = 0;
		curTime = 0;
	}

	public void TimeBlockStarted()
	{
		curTime += 1;
//		OnCheckCommitments(curWeek * 7 + curDay, curTime);
	}

	public void DayEnded(int prevDay)
	{
		if(prevDay == curDay)
		{
			curDay = (curDay+1) % 7;
			OnDayStarted(curDay);

			if(curDay == 0)
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
		GameManager.People.ChangePlayerStatus(0, 0);
	}

	public void FailedCommitment(Commitment sender)
	{
		//To be changed later
	}
}
