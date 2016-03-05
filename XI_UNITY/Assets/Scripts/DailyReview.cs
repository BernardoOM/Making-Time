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

    private string imagePath = "Sprites/Characters";
    private string iconsPath = "Sprites/Icons"; 
    public Sprite[] characters;
    public Sprite[] icons;
    public Sprite badIcon;
    public Sprite goodIcon;

    List<Commitment> dailyEvents = new List<Commitment>();

	int curDisplaingPage;

    void Start()
    {
        Object[] CharactersS = Resources.LoadAll(imagePath, typeof(Sprite));
        Object[] IconsS = Resources.LoadAll(iconsPath, typeof(Sprite));
        characters = new Sprite[10];
        icons = new Sprite[IconsS.Length];

        for (int i = 0; i < CharactersS.Length-1; i++)
        {
            if (CharactersS[i].name == "Char_00_Full"){characters[0] = (Sprite)CharactersS[i];}
            else if (CharactersS[i].name == "Char_01_Full") { characters[1] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_02_Full") { characters[2] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_03_Full") { characters[3] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_04_Full") { characters[4] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_05_Full") { characters[5] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_06_Full") { characters[6] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_07_Full") { characters[7] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_08_Full") { characters[8] = (Sprite)CharactersS[i]; }
            else if (CharactersS[i].name == "Char_09_Full") { characters[9] = (Sprite)CharactersS[i]; }
        }

        for (int j = 0; j < IconsS.Length - 1; j++)
        {
            icons[j] = (Sprite)IconsS[j];

            if(icons[j].name == "badCheck")
            {
                badIcon = icons[j];
            }

            if (icons[j].name == "goodCheck")
            {
                goodIcon = icons[j]; 
            }

        }

        Debug.Log("Textures Loaded: " + characters.Length);
        Debug.Log("Textures Loaded: " + icons.Length);
        Debug.Log("Image Path: " + imagePath + " " + iconsPath);
    }
    public void StartReview(DayofWeek curDayOfWeek)
	{
		string day = "";
		int done = 0;
		int missed = 0;
		curDisplaingPage = 0;
        string textDone = "";
        string textMissed = "";

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
        // Why did we not take in consideration curDayOfWeek to check events done or missed? 
        foreach (Commitment com in dailyEvents)
		{
			if(com.scheduled)
			{	done += 1;	}
			else
			{	missed += 1;	}
		}

		GameObject.Find("TItleDEnded").GetComponent<Text>().text = day + " ended";

        if(done == 0)
        {
            textDone = "You didn't do anything today\n";
        }
        else if(done == 1)
        {
            textDone = "You did 1 thing today\n";
        }
        else if (done >=2)
        {
            textDone = "You did " + done + " things today\n";
        }

        if (missed == 0)
        {
            textMissed = "You didn't miss anything today\n";
        }
        else if (missed == 1)
        {
            textMissed = "You missed 1 thing today\n";
        }
        else if (missed >= 2)
        {
            textMissed = "You missed" + missed + " things today\n";
        }

        GameObject.Find("DescriptionDEnded").GetComponent<Text>().text = textDone + textMissed;

        if (dailyEvents.Count == 0)
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
        GameObject.Find("NameDEvent").GetComponent<Text>().text = dailyEvents[curDisplaingPage - 1].name;
        int imageCreator = Random.Range(0, 9);
        GameObject.Find("CreatorDEvent").GetComponent<Image>().sprite = characters[imageCreator];
        //GameObject.Find("FaceDEnded").GetComponent<Image>().sprite; 
        string eventStatus;
		if(dailyEvents[curDisplaingPage - 1].scheduled)
		{
            eventStatus = "done";
            GameObject.Find("AchieveDEvent").GetComponent<Image>().sprite = goodIcon;
            typeEvent("happy");
        }
        else
		{	eventStatus = "missed";
            GameObject.Find("AchieveDEvent").GetComponent<Image>().sprite = badIcon;
            typeEvent("angry");
        }
        GameObject.Find("TextAchievedDEvent").GetComponent<Text>().text = eventStatus;
	}

    private void typeEvent(string ChkGoodBad)
    {
        if (dailyEvents[curDisplaingPage - 1].name == "Meeting") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "Your Boss is " + ChkGoodBad+ " with you"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "Office Hours") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "Your Boss is " + ChkGoodBad + " with you"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "Plan Classes") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "Your Boss is " + ChkGoodBad + " with you"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "Movie Night") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = dailyEvents[curDisplaingPage - 1].creator+ " is " + ChkGoodBad + " with you"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "Game Night") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = dailyEvents[curDisplaingPage - 1].creator + " is " + ChkGoodBad + " with you"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "Book Club") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = dailyEvents[curDisplaingPage - 1].creator + " is " + ChkGoodBad + " with you"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "Grocery Shopping") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "You are" + ChkGoodBad + " with yourself"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "Dinner") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = dailyEvents[curDisplaingPage - 1].creator + " is " + ChkGoodBad + " with you"; }
        else if (dailyEvents[curDisplaingPage - 1].name == "House Cleaning") { GameObject.Find("DescriptionDEvent").GetComponent<Text>().text = "You are" + ChkGoodBad + " with yourself"; }
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
