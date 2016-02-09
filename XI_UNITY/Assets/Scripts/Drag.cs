using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject	itemBeingDragged;
	private static Color		notScheduledColor = new Color(176, 131, 97, 255);
	private static Color		scheduledColor = new Color(214, 110, 31, 255);
	private static Color		completedColor = new Color(120, 72, 36, 255);

	private static int	startX = -482;
	private static int	calendarStartY = 277;
	private static int	deckY = -218;
	private static int	blockWidth = 171;
	private static int	blockHeight = 83;

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
		{	transform.position = eventData.position;	}
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

			transform.localPosition = SnapToBlock(transform.localPosition.x, transform.localPosition.y);	
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

		if(totalDay > maxTotalDay || totalDay < minTotalDay || (totalDay == maxTotalDay && time > maxTime) || (totalDay == minTotalDay && time < minTime))
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