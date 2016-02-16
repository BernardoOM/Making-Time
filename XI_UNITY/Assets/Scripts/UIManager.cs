using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public delegate void		DragHandler(int maxTotalDay, int minTotalDay, int maxTime, int minTime);
	public delegate void		TimerSlow(bool active);
	public event DragHandler	OnDragAreaSet;
	public event TimerSlow		OnActivateCommitment;

	public GameObject	dialogueBox;
	public GameObject	currentScene;
	public GameObject	calendar;

	public void SetDragArea(int maxTotalDay, int minTotalDay, int maxTime, int minTime)
	{
		if(OnDragAreaSet != null)
		{	OnDragAreaSet(maxTotalDay, minTotalDay, maxTime, minTime);	}
	}

	public void EnterCurrentScene(int dayOfWeek)
	{
		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-410.5f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = new Vector3(513f, 0, 0);
		OnActivateCommitment(true);
	}

	public void LeaveCurrentScne(int dayOfWeek)
	{
		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-921f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = Vector3.zero;
		OnActivateCommitment(false);
	}

	public void StartDialogue()
	{	GameObject.Find("Dialogue").SetActive(true);	}

	public void EndDialogue()
	{	GameObject.Find("Dialogue").SetActive(false);	}
}
