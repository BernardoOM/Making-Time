using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum DayofWeek {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday};

public class Timer : MonoBehaviour
{
	public GameObject blackOut;
	public DayofWeek today;

	public float timer = 0;
	public float eventTimer = 0;

	private RectTransform rectTransform;

	//size of a block of time in pixels
	private static int width = 160;
	private static int maxHeight = 520;
	//length of an in-game day in seconds
	private static int realTimePerDay = 42;
	//30

	private bool isOn = false;
	private static bool startingEvent = false;
	private static bool leavingEvent = false;
	private static bool commitmentActive = false;

    private string timePath = "Sprites/TimeIcon";
    public Sprite[] timeIcon;


    void Start ()
	{
        Object[] TimeS = Resources.LoadAll(timePath, typeof(Sprite));
        timeIcon = new Sprite[TimeS.Length];
        for (int i = 0; i < TimeS.Length; i++)
        {
            if (TimeS[i].name == "Morning") { timeIcon[0] = (Sprite)TimeS[i]; }
            else if (TimeS[i].name == "Afternoon") { timeIcon[1] = (Sprite)TimeS[i]; }
            else if (TimeS[i].name == "Evening") { timeIcon[2] = (Sprite)TimeS[i]; }
        }
        GameManager.Calendar.OnDayStarted += Calendar_OnDayStart;

		if((int)today == GameManager.Calendar.curDayOfWeek)
		{	isOn = true;	}

		rectTransform = GetComponent (typeof (RectTransform)) as RectTransform;
	}

	// Update is called once per frame
	void Update ()
	{
		if(startingEvent && isOn)
		{
			commitmentActive = true;
			blackOut.SetActive(true);
			eventTimer = 0;
			startingEvent = false;
		}
		if(leavingEvent && isOn)
		{
			commitmentActive = false;
			blackOut.SetActive(false);
			timer += Mathf.Min(eventTimer, 6);
			leavingEvent = false;
		}

		if (GameManager.Instance.curState != GameState.Pause && isOn)
		{
			if(commitmentActive)
			{	eventTimer += Time.deltaTime*GameManager.Calendar.fastforward / 5;	}
			else
			{	timer += Time.deltaTime*GameManager.Calendar.fastforward;	}
			//fastforwar is used on pause window fastforward button
			//when clicked, the timer will accelerate 

			if(timer > (GameManager.Calendar.curTime + 1) * (realTimePerDay / 6))
			{	GameManager.Calendar.TimeBlockStarted();	}

            if (timer >= 0 && timer < 10) { GameObject.Find("TimeIcon").GetComponent<Image>().sprite = timeIcon[0];
                GameObject.Find("DayTextIcon").GetComponent<Text>().text = "Morning";
            }
            else if (timer >= 10 && timer < 20) { GameObject.Find("TimeIcon").GetComponent<Image>().sprite = timeIcon[1];
                GameObject.Find("DayTextIcon").GetComponent<Text>().text = "Afternoon";
            }
            else if (timer >= 20 && timer <= 30) { GameObject.Find("TimeIcon").GetComponent<Image>().sprite = timeIcon[2];
                GameObject.Find("DayTextIcon").GetComponent<Text>().text = "Evening";
            }

            if (timer >= realTimePerDay)
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

	public static void EnterScene()
	{	startingEvent = true;	}

	public static void ExitScene()
	{
		leavingEvent = true;
		GameManager.UI.LeaveCurrentScne();
	}
}

