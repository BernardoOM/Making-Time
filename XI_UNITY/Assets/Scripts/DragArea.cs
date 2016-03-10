using UnityEngine;
using System.Collections;

public class DragArea : MonoBehaviour
{
//	private RectTransform rectTransform;

	public RectTransform	borderUpTime;
	public RectTransform	borderDownTime;
	public RectTransform	borderLeftDay;
	public RectTransform	borderRightDay;

    //private static int	startX = -579
    //   private static int	startY = 272;

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

		if(minTotalDay == maxTotalDay && minTime == maxTime) //minTotalDay <= maxTotalDay && minTime <= maxTime
        {
			gameObject.SetActive(true);
			maxTotalDay = Mathf.FloorToInt(Mathf.Min(GameManager.Calendar.viewingWeek * 7 + 6, maxTotalDay));
			minTotalDay = Mathf.FloorToInt(Mathf.Max(GameManager.Calendar.viewingWeek * 7, minTotalDay));
            //			rectTransform.localPosition = new Vector3(minTotalDay * blockWidth + startX, startY - minTime * blockHeight, 0);
            //			rectTransform.sizeDelta = new Vector2((maxTotalDay - minTotalDay + 1) * blockWidth,
            //			                                      (maxTime - minTime + 1) * blockHeight);

            //difTime = maxTime - minTime;
            //difDay = maxTotalDay - minTotalDay;

            //borderUpTime.gameObject.SetActive(true);
            //borderDownTime.gameObject.SetActive(true);
            //borderLeftDay.gameObject.SetActive(true);
            //borderRightDay.gameObject.SetActive(true);

            //borderUpTime.localPosition = new Vector3(startXTimeUpDown + blockWidth * ((minTotalDay + maxTotalDay) / 2), startYTimeUp - blockHeight * minTime, 0);
            //borderUpTime.localScale = new Vector3(0.25f + 0.25f * difDay, 0, 0);

            //borderDownTime.localPosition = new Vector3(startXTimeUpDown + blockWidth * ((minTotalDay + maxTotalDay) / 2), startYTimeDown - blockHeight * maxTime, 0);
            //borderDownTime.localScale = new Vector3(0.25f + 0.25f * difDay, 0, 0);

            //borderLeftDay.localPosition = new Vector3(startXDayLeft + blockWidth * minTotalDay, startYDayLeftRight - blockHeight * ((minTime + maxTime) / 2), 0);
            //borderLeftDay.localScale = new Vector3(0, 0.25f + 0.25f * difTime, 0);

            //borderRightDay.localPosition = new Vector3(startXDayRight + blockWidth * maxTotalDay, startYDayLeftRight - blockHeight * ((minTime + maxTime) / 2), 0);
            //borderRightDay.localScale = new Vector3(0, 0.25f + 0.25f * difTime, 0);

            Debug.Log("Enter in IF Condition\n");
            Debug.Log("MinTotalDay: " + minTotalDay);
            Debug.Log("MaxTotalDay: " + maxTotalDay);
            Debug.Log("MinTime: " + minTime);
            Debug.Log("MaxTime: " + maxTime);
            Debug.Log("\n");
        }
        else
		{
            difTime = maxTime - minTime;
            difDay = maxTotalDay - minTotalDay;

            borderUpTime.gameObject.SetActive(true);
            borderDownTime.gameObject.SetActive(true);
            borderLeftDay.gameObject.SetActive(true);
            borderRightDay.gameObject.SetActive(true);

            Debug.Log("Enter in ELSE Condition\n");
            Debug.Log("MinTotalDay: " + minTotalDay);
            Debug.Log("MaxTotalDay: " + maxTotalDay);
            Debug.Log("MinTime: " + minTime);
            Debug.Log("MaxTime: " + maxTime);
            Debug.Log("\n");



            borderLeftDay.localPosition = new Vector3(startXDayLeft + blockWidth * minTotalDay, startYDayLeftRight - blockHeight * ((minTime + maxTime)/2), 0);
            //borderLeftDay.localScale = new Vector3(0, 0.25f + 0.25f * difTime, 0);

            borderRightDay.localPosition = new Vector3(startXDayRight + blockWidth * maxTotalDay, startYDayLeftRight - blockHeight * ((minTime + maxTime)/2), 0);
            //borderRightDay.localScale = new Vector3(0,0.25f + 0.25f * difTime, 0);


            maxTotalDay = Mathf.Clamp(maxTotalDay, GameManager.Calendar.curWeek * 7, GameManager.Calendar.curWeek * 7 + 6);
            minTotalDay = Mathf.Clamp(maxTotalDay, GameManager.Calendar.curWeek * 7, GameManager.Calendar.curWeek * 7 + 6);

            borderUpTime.localPosition = new Vector3(startXTimeUpDown + blockWidth * ((minTotalDay + maxTotalDay)/2), startYTimeUp - blockHeight * minTime, 0);
            //borderUpTime.localScale = new Vector3(0.25f + 0.25f * difDay, 0, 0);

            borderDownTime.localPosition = new Vector3(startXTimeUpDown + blockWidth * ((minTotalDay + maxTotalDay)/2), startYTimeDown - blockHeight * maxTime, 0);
            //borderDownTime.localScale = new Vector3(0.25f + 0.25f * difDay, 0, 0);

        }
    }
}
