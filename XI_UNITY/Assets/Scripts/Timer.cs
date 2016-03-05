using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum DayofWeek {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};

public class Timer : MonoBehaviour
{
	public DayofWeek today;

	public float timer = 0;

	private RectTransform rectTransform;

	//size of a block of time in pixels
	private static int width = 160;
	private static int maxHeight = 492;
	//length of an in-game day in seconds
	private static int realTimePerDay = 30;
	//30

	private bool isOn = false;
	private bool commitmentActive = false;

	void Start ()
	{
		GameManager.Calendar.OnDayStarted += Calendar_OnDayStart;
		GameManager.UI.OnActivateCommitment += UI_OnActivateCommitment;

		if((int)today == GameManager.Calendar.curDayOfWeek)
		{	isOn = true;	}

		rectTransform = GetComponent (typeof (RectTransform)) as RectTransform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.curState != GameState.Pause && isOn)
		{
			if(commitmentActive)
			{	timer += Time.deltaTime / 5;	}
			else
			{	timer += Time.deltaTime;	}

			if(timer > (GameManager.Calendar.curTime + 1) * (realTimePerDay / 6))
			{	GameManager.Calendar.TimeBlockStarted();	}

			if(timer >= realTimePerDay)
			{
				timer = realTimePerDay;
				isOn = false;

				DailyReview review = GameObject.Find("Calendar").GetComponent<DailyReview>();
				review.StartReview(today);
                //show daily review window 
            }

			rectTransform.sizeDelta = new Vector2(width, Mathf.Lerp(0, maxHeight, timer / realTimePerDay));
		}
	}


	void Calendar_OnDayStart(int newDay)
	{
		if(newDay == 0)
		{
			timer = 0;
			rectTransform.sizeDelta = new Vector2(width, Mathf.Lerp(0, maxHeight, timer / realTimePerDay));
		}
		if(newDay == (int)today)
		{	isOn = true;	}
	}

	public void UI_OnActivateCommitment(bool active)
	{	commitmentActive = active;	}
}
  
