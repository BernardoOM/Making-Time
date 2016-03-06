using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlayerMood {EH, EN, ES, NH, NN, NS, TH, TN, TS};

public class Sidebar : MonoBehaviour
{
	public Animator	Mood;
	public Text		energy;
	public Text		happiness;

	public PlayerMood currMood;

	void Start()
	{	currMood = PlayerMood.NN;	}

	public void ChangeMood(int deltaEnergy, int deltaHappiness, int newEnergy, int newHappiness)
	{
		Debug.Log(deltaEnergy + " " + deltaHappiness + " " + newEnergy + " " + newHappiness);

		if(newEnergy >= 3)
		{
			if((int)currMood / 3 == 0)
			{
				if(deltaEnergy < 0)
				{	Tired();	}
			}
			else
			{
				if(deltaEnergy > 0)
				{	Energier();	}
			}

			currMood = (PlayerMood)(0 * 3 + (int)currMood % 3);
		}
		else if(newEnergy <= -3)
		{
			if((int)currMood / 3 == 2)
			{
				if(deltaEnergy > 0)
				{	Energetic();	}
			}
			else
			{
				if(deltaEnergy < 0)
				{	Tireder();	}
			}

			currMood = (PlayerMood)(2 * 3 + (int)currMood % 3);
		}
		else
		{
			if((int)currMood / 3 == 1)
			{
				if(deltaEnergy > 0)
				{	Energetic();	}
				else if(deltaEnergy < 0)
				{	Tired();	}
			}
			else
			{
				if(deltaEnergy > 0)
				{	Energier();	}
				else if(deltaEnergy < 0)
				{	Tireder();	}
			}

			currMood = (PlayerMood)(1 * 3 + (int)currMood % 3);
		}

		if(newHappiness >= 3)
		{
			if((int)currMood % 3 == 0)
			{
				if(deltaHappiness < 0)
				{	Stress();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	Happier();	}
			}

			currMood = (PlayerMood)((int)currMood - (int)currMood % 3 + 0);
		}
		else if(newHappiness <= -3)
		{
			if((int)currMood % 3 == 2)
			{
				if(deltaHappiness > 0)
				{	Happy();	}
			}
			else
			{
				if(deltaHappiness < 0)
				{	Stressier();	}
			}

			currMood = (PlayerMood)((int)currMood - (int)currMood % 3 + 2);
		}
		else
		{
			if((int)currMood % 3 == 1)
			{
				if(deltaHappiness > 0)
				{	Happy();	}
				else if(deltaHappiness < 0)
				{	Stress();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	Happier();	}
				else if(deltaHappiness < 0)
				{	Stressier();	}
			}

			currMood = (PlayerMood)((int)currMood - (int)currMood % 3 + 1);
		}
	}
		
	void Happy()
	{	Mood.SetTrigger("happy");	}
	void Stress()
	{	Mood.SetTrigger("stress");	}
	void Tired()
	{	Mood.SetTrigger("tired");	}
	void Energetic()
	{	Mood.SetTrigger("energetic");	}

	void Happier()
	{	Mood.SetTrigger("happier");	}
	void Stressier()
	{	Mood.SetTrigger("stressier");	}
	void Tireder()
	{	Mood.SetTrigger("tireder");	}
	void Energier()
	{	Mood.SetTrigger("energier");	}

//	public void ChangeValues(int newEnergy, int newHappiness)
//	{
//		energy.text = (newEnergy + 5).ToString();
//		happiness.text = (newHappiness + 5).ToString();
//	}

	public void PauseButton()
	{
		if(GameManager.Instance.curState != GameState.Pause)
		{
			GameManager.Instance.PauseGame();
			GameObject go = Instantiate(Resources.Load("PausePhone"),transform.parent.position, transform.parent.rotation) as GameObject;
			go.transform.parent = transform.parent;
			go.GetComponent<RectTransform>().localPosition = new Vector3(85, 0, 0);

			//if in real scene, move the pause window to the center of screen 
			if (GameManager.UI.CuSceneBool == true) {
				go.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				go.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

			}

		}
	}
}
