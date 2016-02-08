using UnityEngine;
using System.Collections;

public enum DayofWeek {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};

public class Timer : MonoBehaviour
{
	public DayofWeek today;

	public float timer = 0;

	private RectTransform rt;

	//size of a block of time in pixels
	private static int width = 168;
	private static int maxHeight = 492;
	//length of an in-game day in seconds
	private static int realTimePerDay = 2;

	private bool isOn = false;

	void Start ()
	{
		GameManager.Calendar.OnDayStarted += Calendar_OnDayStart;
		if((int)today == GameManager.Calendar.curDay)
		{	isOn = true;	}

		rt = GetComponent (typeof (RectTransform)) as RectTransform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.Instance.curState != GameState.Pause && isOn)
		{
			timer += Time.deltaTime;

			if(timer / 6 > GameManager.Calendar.curTime)
			{	GameManager.Calendar.TimeBlockStarted();	}

			if(timer >= realTimePerDay)
			{
				timer = realTimePerDay;
				isOn = false;
				GameManager.Calendar.DayEnded((int)today);
			}

			rt.sizeDelta = new Vector2(width, Mathf.Lerp(0, maxHeight, timer / realTimePerDay));
		}
	}

	void Calendar_OnDayStart(int newDay)
	{
		if(newDay == 0)
		{
			timer = 0;
			rt.sizeDelta = new Vector2(width, Mathf.Lerp(0, maxHeight, timer / realTimePerDay));
		}
		if(newDay == (int)today)
		{	isOn = true;	}
	}
}
  
