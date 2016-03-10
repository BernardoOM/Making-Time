using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{
//	private RectTransform rectTransform;

	public RectTransform	borderUpTime;
	public RectTransform	borderDownTime;
	public RectTransform	borderLeftDay;
	public RectTransform	borderRightDay;

	private static int	startX = -579;
    private static int	startY = 272;

    private static int startXTimeUpDown = -502;
    private static int startYTimeUp = 272;
    private static int startYTimeDown = 185;

    private static int startXDayLeft = -578;
    private static int startXDayRight = -418;
    private static int startYDayLeftRight = 232;

   
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

        borderUpTime.gameObject.SetActive(false);
        borderDownTime.gameObject.SetActive(false);
        borderLeftDay.gameObject.SetActive(false);
        borderRightDay.gameObject.SetActive(false);

        //		rectTransform = GetComponent(typeof(RectTransform)) as RectTransform;
        //		rectTransform.position = new Vector3(startX, startY, 0);
        //		rectTransform.sizeDelta = Vector2.zero;
    }
		
	void UI_OnDragAreaSet(int maxTotalDay, int minTotalDay, int maxTime, int minTime)
	{
        float difTime = 0;
        float difDay = 0;

		if(minTotalDay <= maxTotalDay && minTime <= maxTime) //minTotalDay <= maxTotalDay && minTime <= maxTime
        {
			difTime = maxTime - minTime;
			difDay = maxTotalDay - minTotalDay;

			borderUpTime.gameObject.SetActive(true);
			borderDownTime.gameObject.SetActive(true);
			borderLeftDay.gameObject.SetActive(true);
			borderRightDay.gameObject.SetActive(true);

			borderLeftDay.localPosition = new Vector3(startX + blockWidth * minTotalDay, startY - blockHeight * ((float)(minTime + maxTime + 1))/2, 0);
			borderLeftDay.localScale = new Vector3(.25f, 0.25f + 0.25f * difTime, .25f);

			borderRightDay.localPosition = new Vector3(startX + blockWidth * (maxTotalDay + 1), startY - blockHeight * ((float)(minTime + maxTime + 1))/2, 0);
			borderRightDay.localScale = new Vector3(.25f,0.25f + 0.25f * difTime, .25f);

			maxTotalDay = Mathf.Clamp(maxTotalDay, GameManager.Calendar.curWeek * 7, GameManager.Calendar.curWeek * 7 + 6);
			minTotalDay = Mathf.Clamp(minTotalDay, GameManager.Calendar.curWeek * 7, GameManager.Calendar.curWeek * 7 + 6);
			difTime = maxTime - minTime;
			difDay = maxTotalDay - minTotalDay;

			borderUpTime.localPosition = new Vector3(startX + blockWidth * ((float)(minTotalDay + maxTotalDay + 1))/2, startY - blockHeight * minTime, 0);
			borderUpTime.localScale = new Vector3(0.25f + 0.25f * difDay, .25f, .25f);

			borderDownTime.localPosition = new Vector3(startX + blockWidth * ((float)(minTotalDay + maxTotalDay + 1))/2, startY - blockHeight * (maxTime + 1), 0);
			borderDownTime.localScale = new Vector3(0.25f + 0.25f * difDay, .25f, .25f);
        }
        else
		{
			borderUpTime.gameObject.SetActive(false);
			borderDownTime.gameObject.SetActive(false);
			borderLeftDay.gameObject.SetActive(false);
			borderRightDay.gameObject.SetActive(false);
        }
    }
}
