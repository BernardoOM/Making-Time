using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{
	private RectTransform rectTransform;

	private static int	startX = -483;
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
		
	void UI_OnDragAreaSet(int minDay, int maxDay, int minTime, int maxTime)
	{
		rectTransform.localPosition = new Vector3(minDay * blockWidth + startX,startY - minTime * blockHeight, 0);
		rectTransform.sizeDelta = new Vector2((maxDay - minDay + 1) * blockWidth, (maxTime - minTime + 1) * blockHeight);
		Debug.Log(rectTransform.localPosition.ToString());
	}
}
