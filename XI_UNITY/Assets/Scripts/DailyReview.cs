using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DailyReview : MonoBehaviour
{
	public GameObject	mainPage;
	public GameObject	eventPage;
	public Text			mainButtonText;
	public Text			eventButtonText;

	List<Commitment> dailyEvents = new List<Commitment>();

	int curDisplaingPage;

	public void StartReview(DayofWeek curDayOfWeek)
	{
		string day = "";
		int done = 0;
		int missed = 0;
		curDisplaingPage = 0;

		switch (curDayOfWeek)
		{
		case DayofWeek.Sunday:
			day = "Sunday";
			break;
		case DayofWeek.Monday:
			day = "Monday";
			break;
		case DayofWeek.Tuesday:
			day = "Tuesday";
			break;
		case DayofWeek.Wednesday:
			day = "Wednesday";
			break;
		case DayofWeek.Thursday:
			day = "Thursday";
			break;
		case DayofWeek.Friday:
			day = "Friday";
			break;
		case DayofWeek.Saturday:
			day = "Saturday";
			break;
		}

		foreach(Commitment com in dailyEvents)
		{
			if(com.scheduled)
			{	done += 1;	}
			else
			{	missed += 1;	}
		}

		GameObject.Find("TItleDEnded").GetComponent<Text>().text = day + " ended";
		GameObject.Find("DescriptionDEnded").GetComponent<Text>().text = " You did " + done + " things\n You missed " + missed + " things";

		if(dailyEvents.Count == 0)
		{	mainButtonText.text = "Go to\nSleep";	}
		else
		{	mainButtonText.text = "Next";	}

		ShowWindow(mainPage);
	}
		
	public void NextPage()
	{
		if(curDisplaingPage == dailyEvents.Count)
		{
			HideWindow(mainPage);
			HideWindow(eventPage);
			GameManager.Calendar.DayEnded();
		}
		else
		{
			if(curDisplaingPage == 0)
			{
				HideWindow(mainPage);
				ShowWindow(eventPage);
			}

			curDisplaingPage += 1;

			if(curDisplaingPage == dailyEvents.Count)
			{	eventButtonText.text = "Go to\nSleep";	}
			else
			{	eventButtonText.text = "Next";	}

			ShowEvent();
		}
	}

	public void PrevPage()
	{
		curDisplaingPage -= 1;

		if(curDisplaingPage == dailyEvents.Count - 1)
		{	eventButtonText.text = "Next";	}
		
		if(curDisplaingPage == 0)
		{
			ShowWindow(mainPage);
			HideWindow(eventPage);
		}
		else
		{	ShowEvent();	}
	}

	public void ShowEvent()
	{
		// GameObject.Find("CreatorDEvent") creator image
		GameObject.Find("NameDEvent").GetComponent<Text>().text = dailyEvents[curDisplaingPage - 1].name;
		GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "Your " + dailyEvents[curDisplaingPage - 1].creator + " is happy with you";
		//    GameObject.Find("AchieveDEvent") done or missed image
		string eventStatus;
		if(dailyEvents[curDisplaingPage - 1].scheduled)
		{	eventStatus = "done";	}
		else
		{	eventStatus = "missed";	}
		GameObject.Find("TextAchievedDEvent").GetComponent<Text>().text = eventStatus;
	}

	public void AddEventToReview(Commitment newCom)
	{	dailyEvents.Add(newCom);	}

	public void ShowWindow(GameObject window)
	{	window.transform.localPosition = new Vector3(62f, -7f);	}

	public void HideWindow(GameObject window)
	{	window.transform.localPosition = new Vector3(-200f, -1000f);	}

	public void ClearDay()
	{
		foreach(Commitment com in dailyEvents)
		{
			if(!com.scheduled)
			{	Destroy(com);	}
		}

		dailyEvents.Clear();
	}
}
