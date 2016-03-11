using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	public GameObject	eventCircle;
	public GameObject	eventArrow;

	float	timer;
	public bool		selected;

	// Use this for initialization
	void Start ()
	{
		eventCircle.SetActive(true);
		eventArrow.SetActive(false);
		timer = Time.time;
		selected = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!selected)
		{
			if(Time.time - timer >= .5f)
			{
				eventCircle.SetActive(!eventCircle.activeSelf);
				timer = Time.time;
			}
		}
		else
		{
			if(Time.time - timer >= .5f)
			{
				eventArrow.SetActive(!eventArrow.activeSelf);
				timer = Time.time;
			}
		}
	}

	public void TutorialEventSelected()
	{
		selected = true;
		eventCircle.SetActive(false);
		eventArrow.SetActive(true);
	}

	public void TutorialEventUnselected()
	{
		selected = false;
		eventCircle.SetActive(true);
		eventArrow.SetActive(false);
	}

	public void TutorialEnd()
	{
		Destroy(eventCircle);
		Destroy(eventArrow);
		enabled = false;
	}
}
