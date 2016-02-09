using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{
	private RectTransform rectTransform;

	private static int	startX = -484;
	private static int	startY = 277;
	private static int	blockWidth = 171;
	private static int	blockHeight = 82;

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
