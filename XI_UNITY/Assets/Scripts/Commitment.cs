using UnityEngine;
using System.Collections;

public class Commitment : MonoBehaviour
{
	string	name,		creator;
	int		curDay,		curTime,	timeLength,	dbValue;
	int		maxDay,		minDay,		maxTime,	minTime;
	bool	scheduled;
	bool	completed = false;

	// Use this for initialization
	void Start ()
	{	GameManager.Calendar.OnCheckCommitments += Calendar_OnCheckCommitments;	}

	//Constructors
	public void RecordInfo(string aName, string aCreator, int aTimeLength, int theDBValue,
	                       int aMaxDay, int aMinDay, int aMaxTime, int aMinTime)
	{
		name = aName;		creator = aCreator;	timeLength = aTimeLength;	dbValue = theDBValue;
		maxDay = aMaxDay;	minDay = aMinDay;	maxTime = aMaxTime;			minTime = aMinTime;
		scheduled = false;
	}
	public void RecordInfo(string aName, string aCreator, int aCurDay, int aCurTime, int aTimeLength,
	                       int theDBValue,	int aMaxDay, int aMinDay, int aMaxTime, int aMinTime)
	{
		name = aName;				creator = aCreator;		curDay = aCurDay;	curTime = aCurTime;
		timeLength = aTimeLength;	dbValue = theDBValue;	maxDay = aMaxDay;	minDay = aMinDay;
		maxTime = aMaxTime;			minTime = aMinTime;		scheduled = true;
	}

	void Calendar_OnCheckCommitments(int aCurDay, int aCurTime)
	{
		if(scheduled)
		{
			if(curDay == aCurDay && curTime == aCurTime)
			{
				GameManager.Calendar.ActivateCommitment(this);
				completed = true;
			}
			else if(aCurDay > maxDay && aCurTime > maxTime && !completed)
			{	GameManager.Calendar.FailedCommitment(this);	}
		}
	}
}
