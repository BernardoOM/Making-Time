﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject	itemBeingDragged;
	private static Color[] notScheduledColors = { new Color(.90f, .63f, .45f),
												  new Color(.45f, .73f, .90f),
												  new Color(.48f, .78f, .80f),
												  new Color(1.0f, .89f, .45f)};
	
	private static Color[] scheduledColors = { new Color(1f,1f,1f,1f),
												new Color(1f,1f,1f,1f),
											   new Color(.9f, 1f, 1f),
											   new Color(1f, .60f, 1f)};
//	private static Color[] scheduledColors = { new Color(.90f, .49f, .22f),
//		new Color(.22f, .64f, .90f),
//		new Color(.28f, .78f, .80f),
//		new Color(.95f, .80f, .19f)};
	private static Color[] activatedColors = { new Color(1.0f, .61f, .35f),
											   new Color(.35f, .75f, 1.0f),
											   new Color(.40f, .88f, .90f),
											   new Color(1.0f, .86f, .30f)};
	private static Color[] completedColors = { new Color(.80f, .39f, .12f),
											   new Color(.12f, .53f, .80f),
											   new Color(.18f, .67f, .70f),
											   new Color(.85f, .69f, .09f)};
	
	private static Color orginal_color = new Color (1f, 1f, 1f, 1f);



	private static int	startX = -494;
	private static int	calendarStartY = 272;
	private static int	deckY = -290;
	private static int	blockWidth = 163;
	private static int	blockHeight = 88;

//	private static int	startX = -496;
//	private static int	calendarStartY = 247;
//	private static int	deckY = -269;
//	private static int	blockWidth = 163;
//	private static int	blockHeight = 82;

	private Image		buttonImage;
	private Vector3		startPosition;
	private Commitment	com;

	private bool	wasClicked = false;
	private bool	isFocus = false;


	private string timePath = "Sprites/Postits";
	private Object[] events;
	//variables for change colors of each event button. unsched and sched


//	private Calculate calculate;

	void Start()
	{
		GameManager.Calendar.OnCommitmentClicked += Calendar_OnCommitmentClicked;

		buttonImage = GetComponent<Image>();
		startPosition = transform.localPosition;
		com = GetComponent<Commitment>();
	}

	#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		if(!isFocus)
		{	ButtonClicked();	}
		if(isFocus && !com.activated)
		{
			itemBeingDragged = gameObject;
			GetComponent<CanvasGroup>().blocksRaycasts = false;

			GameObject.Find ("Main Camera").GetComponent<AudioManager> ().pickup_play();
		}
	}

	#endregion

	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		if(isFocus && !com.activated)
		{	transform.position = new Vector3(eventData.position.x - blockWidth/2, eventData.position.y + blockHeight/2, 0);	}
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{
		if(isFocus && !com.activated)
		{
			itemBeingDragged = null;
			GetComponent<CanvasGroup>().blocksRaycasts = true;
			isFocus = false;
			GameManager.Calendar.NoFocus();


			transform.localPosition = SnapToBlock(transform.localPosition.x + blockWidth/2, transform.localPosition.y - blockHeight/2);
			startPosition = transform.localPosition;
			ShiftDeck();

			GameObject.Find ("Main Camera").GetComponent<AudioManager> ().putdown_play ();

			if(GameManager.Instance.curState == GameState.Tutorial)
			{	GameObject.Find("Calendar").GetComponent<Tutorial>().TutorialEventUnselected();	}

		}
	}
	
	#endregion


	public Vector3 SnapToBlock(float x, float y)
	{
		int positionX = Mathf.FloorToInt(x);	int positionY = Mathf.FloorToInt(y);

		if(positionX < startX || positionX > startX + (blockWidth * 7) || positionY < deckY - blockHeight || positionY > calendarStartY)
		{	return startPosition;	}

		if(positionY > deckY)
		{
			int dayOfWeek = (positionX - startX) / blockWidth;
			int time = (calendarStartY - positionY) / blockHeight;

			if(CheckSchedulePlacement(GameManager.Calendar.viewingWeek * 7 + dayOfWeek, time))
			{
				if(!com.scheduled)
				{
					//change color of buttons, if drag back to calendar 
					//buttonImage.color = scheduledColors[(int)com.curType];
					Change_sch_color ();

					com.SetPlaceOnSchedule(time, GameManager.Calendar.viewingWeek * 7 + dayOfWeek);
					com.SetScheduled(true);
					GameManager.Calendar.ScheduleCommitment(com);
					transform.SetParent(GameObject.Find("Scheduled").transform);
				}
				else
				{	com.SetPlaceOnSchedule(time, GameManager.Calendar.viewingWeek * 7 + dayOfWeek);	}

				return new Vector3(startX + (dayOfWeek * blockWidth), calendarStartY - (time * blockHeight), 0);
			}
			else
			{	return startPosition;	}
		}
		else
		{
			if(com.scheduled)
			{
				//change color of buttons to original color, if drag back to deck
				//buttonImage.color=orginal_color;
				Change_unsch_color ();
					
				com.SetScheduled(false);
				GameManager.Calendar.UnScheduleCommitment(com);
				transform.SetParent(GameObject.Find("Deck").transform);
			}

			return new Vector3(startX + (GameManager.Calendar.FindIndexUnScheduled(com) * blockWidth), deckY, 0);
		}
	}

	public static void ShiftDeck()
	{
		foreach(Commitment listCom in GameManager.Calendar.unscheduledCommitments)
		{	listCom.transform.localPosition = new Vector3(startX + (GameManager.Calendar.unscheduledCommitments.IndexOf(listCom) * blockWidth), deckY, 0);	}
	}

	void Calendar_OnCommitmentClicked ()
	{
		if(wasClicked && isFocus)
		{
			isFocus = false;
			GameManager.Calendar.NoFocus();
			if(GameManager.Instance.curState == GameState.Tutorial)
			{	GameObject.Find("Calendar").GetComponent<Tutorial>().TutorialEventUnselected();	}
		}
		else if(wasClicked && !isFocus)
		{
			isFocus = true;
			GameManager.Calendar.CommitmentFocus(com);
			if(GameManager.Instance.curState == GameState.Tutorial)
			{	GameObject.Find("Calendar").GetComponent<Tutorial>().TutorialEventSelected();	}
		}
		else if(!wasClicked && isFocus)
		{
			isFocus = false;
			if(GameManager.Instance.curState == GameState.Tutorial)
			{	GameObject.Find("Calendar").GetComponent<Tutorial>().TutorialEventUnselected();	}
		}

		wasClicked = false;
	}

	public void ButtonClicked()
	{
		wasClicked = true;
		GameManager.Calendar.CommitmentClicked();
	}

	bool CheckSchedulePlacement(int totalDay, int time)
	{
		int maxTotalDay, minTotalDay, maxTime, minTime;
		com.ReturnTimeRange(out maxTotalDay, out minTotalDay, out maxTime, out minTime);

		if(totalDay > maxTotalDay || totalDay < minTotalDay || time > maxTime || time < minTime)
		{	return false;	}

		if(!GameManager.Calendar.CheckCalendarSpace(totalDay, time))
		{	return false;	}

		if(GameManager.Instance.curState == GameState.Tutorial)
		{
			GameObject.Find("Calendar").GetComponent<Tutorial>().TutorialEnd();
			GameManager.Instance.BeginGame();
		}

		return true;
	}

	public static void PlaceUnscheduled(Commitment com)
	{	
		//place every event button's place 
		com.transform.localPosition = new Vector3(startX + (GameManager.Calendar.FindIndexUnScheduled(com) * blockWidth), deckY, 0);
		ShiftDeck();

		}

	public void Activated()
	{	
		buttonImage.color = activatedColors[(int)com.curType];	
	}

	public void Completed()
	{	
		buttonImage.color = completedColors[(int)com.curType];	
	}

	//change color of event buttons on calendar. 
	void Change_sch_color ()
	{
		events = Resources.LoadAll(timePath, typeof(Sprite));
		for (int i = 0; i < events.Length; i++)
		{
			if ((int)com.curType==3 && events[i].name == "Chore_Postit_sched") { buttonImage.sprite = (Sprite)events[i]; }
			else if ((int)com.curType==2 &&events[i].name == "Social_Postit_sched") { buttonImage.sprite = (Sprite)events[i]; }
			else if ((int)com.curType==0 &&events[i].name == "Work_Postit_sched") { buttonImage.sprite = (Sprite)events[i]; }
		}
	}


	void Change_unsch_color ()
	{
		events = Resources.LoadAll(timePath, typeof(Sprite));
		for (int i = 0; i < events.Length; i++)
		{
			if ((int)com.curType==3 && events[i].name == "Chore_Postit_unsched") { buttonImage.sprite = (Sprite)events[i]; }
			else if ((int)com.curType==2 &&events[i].name == "Social_Postit_unsched") { buttonImage.sprite = (Sprite)events[i]; }
			else if ((int)com.curType==0 &&events[i].name == "Work_Postit_unsched") { buttonImage.sprite = (Sprite)events[i]; }
		}
	}


}



