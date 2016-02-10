using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject	itemBeingDragged;
	private static Color		notScheduledColor = new Color(.69f, .51f, .38f);
	private static Color		scheduledColor = new Color(.84f, .43f, .12f);
	private static Color		completedColor = new Color(.47f, .28f, .14f);

	private static int	startX = -496;
	private static int	calendarStartY = 247;
	private static int	deckY = -269;
	private static int	blockWidth = 163;
	private static int	blockHeight = 82;

	private Image		buttonImage;
	private Vector3		startPosition;
	private Commitment	com;

	private bool	wasClicked = false;
	private bool	isFocus = false;

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
		if(isFocus && !com.completed)
		{
			itemBeingDragged = gameObject;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	#endregion

	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		if(isFocus && !com.completed)
		{	transform.position = new Vector3(eventData.position.x - blockWidth/2, eventData.position.y + blockHeight/2, 0);	}
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{
		if(isFocus && !com.completed)
		{
			itemBeingDragged = null;
			GetComponent<CanvasGroup>().blocksRaycasts = true;
			isFocus = false;
			GameManager.Calendar.NoFocus();

			transform.localPosition = SnapToBlock(transform.localPosition.x + blockWidth/2, transform.localPosition.y - blockHeight/2);
			ShiftDeck();
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
					buttonImage.color = scheduledColor;
					com.SetPlaceOnSchedule(time, GameManager.Calendar.viewingWeek * 7 + dayOfWeek);
					com.SetScheduled(true);
					GameManager.Calendar.ScheduleCommitment(com);
					transform.parent = GameObject.Find("Scheduled").transform;
				}

				return new Vector3(startX + (dayOfWeek * blockWidth), calendarStartY - (time * blockHeight), 0);
			}
			else
			{	return startPosition;	}
		}
		else
		{
			if(com.scheduled)
			{
				buttonImage.color = notScheduledColor;
				com.SetScheduled(false);
				GameManager.Calendar.UnScheduleCommitment(com);
				transform.parent = GameObject.Find("Deck").transform;
			}

			return new Vector3(startX + (GameManager.Calendar.FindIndexUnScheduled(com) * blockWidth), deckY, 0);
		}
	}

	void ShiftDeck()
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
		}
		else if(wasClicked && !isFocus)
		{
			isFocus = true;
			GameManager.Calendar.CommitmentFocus(com);
		}
		else if(!wasClicked && isFocus)
		{	isFocus = false;	}

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

		return true;
	}

	public static void PlaceUnscheduled(Commitment com)
	{	com.transform.localPosition = new Vector3(startX + (GameManager.Calendar.FindIndexUnScheduled(com) * blockWidth), deckY, 0);	}

	public void Completed()
	{	buttonImage.color = completedColor;	}
}