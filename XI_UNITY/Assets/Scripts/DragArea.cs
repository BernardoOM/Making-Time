﻿using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{
	private RectTransform rectTransform;

	private static int	startX = -579;
	private static int	startY = 272;
//	private static int	startX = -581;
//	private static int	startY = 246;
//	private static int	blockWidth = 163;
//	private static int	blockHeight = 82;

//	private static int	startX = -494;

	private static int	blockWidth = 163;
	private static int	blockHeight = 87;

	void Start()
	{
		GameManager.UI.OnDragAreaSet += UI_OnDragAreaSet;

		rectTransform = GetComponent(typeof(RectTransform)) as RectTransform;
		rectTransform.position = new Vector3(startX, startY, 0);
		rectTransform.sizeDelta = Vector2.zero;
	}
		
	void UI_OnDragAreaSet(int maxTotalDay, int minTotalDay, int maxTime, int minTime)
	{
		if(minTotalDay <= maxTotalDay && minTime <= maxTime)
		{
			gameObject.SetActive(true);
			maxTotalDay = Mathf.FloorToInt(Mathf.Min(GameManager.Calendar.viewingWeek * 7 + 6, maxTotalDay));
			minTotalDay = Mathf.FloorToInt(Mathf.Max(GameManager.Calendar.viewingWeek * 7, minTotalDay));
			rectTransform.localPosition = new Vector3(minTotalDay * blockWidth + startX, startY - minTime * blockHeight, 0);
			rectTransform.sizeDelta = new Vector2((maxTotalDay - minTotalDay + 1) * blockWidth,
			                                      (maxTime - minTime + 1) * blockHeight);
		}
		else
		{	gameObject.SetActive(false);	}
	}
}
