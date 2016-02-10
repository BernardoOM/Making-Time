using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
	public delegate void		DragHandler(int maxTotalDay, int minTotalDay, int maxTime, int minTime);
	public event DragHandler	OnDragAreaSet;

	public GameObject	dialogueBox;
	public GameObject	currentScene;
	public GameObject	calendar;

	public void SetDragArea(int maxTotalDay, int minTotalDay, int maxTime, int minTime)
	{
		if(OnDragAreaSet != null)
		{	OnDragAreaSet(maxTotalDay, minTotalDay, maxTime, minTime);	}
	}

	public void EnterCurrentScene()
	{
		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-410.5f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = new Vector3(513f, 0, 0);
		GameObject.Find("TimeBar1").GetComponent<Timer>().ActivateCommitment();
	}

	public void LeaveCurrentScne()
	{
		GameObject.Find("Current Scene").GetComponent<RectTransform>().localPosition = new Vector3(-921f, 0, 0);
		GameObject.Find("Calendar").GetComponent<RectTransform>().localPosition = Vector3.zero;
		GameObject.Find("TimeBar1").GetComponent<Timer>().CompleteCommitment();
	}

	public void StartDialogue()
	{	GameObject.Find("Dialogue").SetActive(true);	}

	public void EndDialogue()
	{	GameObject.Find("Dialogue").SetActive(false);	}
}
