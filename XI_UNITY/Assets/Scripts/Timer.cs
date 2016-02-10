using UnityEngine;
using System.Collections;

public enum DayofWeek {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};

public class Timer : MonoBehaviour
{
	public DayofWeek today;

	public float timer = 0;

	private RectTransform rectTransform;

	//size of a block of time in pixels
	private static int width = 168;
	private static int maxHeight = 492;
	//length of an in-game day in seconds
	private static int realTimePerDay = 12;

	private bool isOn = false;

	void Start ()
	{
		GameManager.Calendar.OnDayStarted += Calendar_OnDayStart;

		if((int)today == GameManager.Calendar.curDayOfWeek)
		{	isOn = true;	}

		rectTransform = GetComponent (typeof (RectTransform)) as RectTransform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.curState != GameState.Pause && isOn)
		{
			timer += Time.deltaTime;

			if(timer > (GameManager.Calendar.curTime + 1) * (realTimePerDay / 6))
			{	GameManager.Calendar.TimeBlockStarted();	}

			if(timer >= realTimePerDay)
			{
				timer = realTimePerDay;
				isOn = false;
				GameManager.Calendar.DayEnded((int)today);
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
}
  
