using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public delegate void		DragHandler(int minDay, int maxDay, int minTime, int maxTime);
	public event DragHandler	OnDragAreaSet;

	public void SetDragArea(int minDay, int maxDay, int minTime, int maxTime)
	{
		if(minDay < maxDay && minTime < maxTime)
		{	OnDragAreaSet(minDay, maxDay, minTime, maxTime);	}
	}
}
