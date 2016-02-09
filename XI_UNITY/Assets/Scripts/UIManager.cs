using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public delegate void		DragHandler(int maxTotalDay, int minTotalDay, int maxTime, int minTime);
	public event DragHandler	OnDragAreaSet;

	public void SetDragArea(int maxTotalDay, int minTotalDay, int maxTime, int minTime)
	{	OnDragAreaSet(maxTotalDay, minTotalDay, maxTime, minTime);		}
}
