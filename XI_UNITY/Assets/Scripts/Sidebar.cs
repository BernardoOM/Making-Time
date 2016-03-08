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
//	public PlayerMood prevMood;

	void Start()
	{	currMood = PlayerMood.NN;	}

	public void ChangeMood(int deltaEnergy, int deltaHappiness, int newEnergy, int newHappiness)
	{
		switch(currMood)
		{
		case PlayerMood.EH:
			if(deltaEnergy < 0)
			{
				if(deltaHappiness < 0)
				{	NeutralNeutral();	}
				else
				{	HappyNeutral();	}
			}
			else
			{
				if(deltaHappiness < 0)
				{	NeutralEnergized();	}
			}
			break;
		case PlayerMood.EN:
			if(deltaEnergy < 0)
			{
				if(deltaHappiness > 0)
				{	HappyNeutral();	}
				else if(deltaHappiness < 0)
				{	StressedNeutral();	}
				else
				{	NeutralNeutral();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	HappyEnergized();	}
				else if(deltaHappiness < 0)
				{	StressedEnergized();	}
			}
			break;
		case PlayerMood.ES:
			if(deltaEnergy < 0)
			{
				if(deltaHappiness > 0)
				{	NeutralNeutral();	}
				else
				{	StressedNeutral();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	NeutralEnergized();	}
			}
			break;
		case PlayerMood.NH:
			if(deltaEnergy > 0)
			{
				if(deltaHappiness < 0)
				{	NeutralEnergized();	}
				else
				{	HappyEnergized();	}
			}
			else if(deltaEnergy < 0)
			{
				if(deltaHappiness < 0)
				{	NeutralTired();	}
				else
				{	StressedNeutral();	}
			}
			else
			{
				if(deltaHappiness < 0)
				{	NeutralNeutral();	}
			}
			break;
		case PlayerMood.NN:
			if(deltaEnergy > 0)
			{
				if(deltaHappiness > 0)
				{	HappyEnergized();	}
				else if(deltaHappiness < 0)
				{	StressedEnergized();	}
				else
				{	NeutralEnergized();	}
			}
			else if(deltaEnergy < 0)
			{
				if(deltaHappiness > 0)
				{	HappyTired();	}
				else if(deltaHappiness < 0)
				{	StressedTired();	}
				else
				{	NeutralTired();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	HappyNeutral();	}
				else if(deltaHappiness < 0)
				{	StressedNeutral();	}
			}
			break;
		case PlayerMood.NS:
			if(deltaEnergy > 0)
			{
				if(deltaHappiness > 0)
				{	NeutralEnergized();	}
				else
				{	StressedEnergized();	}
			}
			else if(deltaEnergy < 0)
			{
				if(deltaHappiness > 0)
				{	NeutralTired();	}
				else
				{	StressedTired();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	NeutralNeutral();	}
			}
			break;
		case PlayerMood.TH:
			if(deltaEnergy > 0)
			{
				if(deltaHappiness < 0)
				{	NeutralNeutral();	}
				else
				{	HappyNeutral();	}
			}
			else
			{
				if(deltaHappiness < 0)
				{	NeutralTired();	}
			}
			break;
		case PlayerMood.TN:
			if(deltaEnergy > 0)
			{
				if(deltaHappiness > 0)
				{	HappyNeutral();	}
				else if(deltaHappiness < 0)
				{	StressedNeutral();	}
				else
				{	NeutralNeutral();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	HappyTired();	}
				else if(deltaHappiness < 0)
				{	StressedTired();	}
			}
			break;
		case PlayerMood.TS:
			if(deltaEnergy > 0)
			{
				if(deltaHappiness > 0)
				{	NeutralNeutral();	}
				else
				{	StressedNeutral();	}
			}
			else
			{
				if(deltaHappiness > 0)
				{	NeutralTired();	}
			}
			break;
		}

		if(newEnergy >= 3)
		{
			if(newHappiness >= 3)
			{
				currMood = PlayerMood.EH;
				StartCoroutine("RevHappyEnergized");
			}
			else if(newHappiness <= -3)
			{
				currMood = PlayerMood.ES;
				StartCoroutine("RevStressedEnergized");
			}
			else
			{
				currMood = PlayerMood.EN;
				StartCoroutine("RevNeutralEnergized");
			}
		}
		else if(newEnergy <= -3)
		{
			if(newHappiness >= 3)
			{
				currMood = PlayerMood.TH;
				StartCoroutine("RevHappyTired");
			}
			else if(newHappiness <= -3)
			{
				currMood = PlayerMood.TS;
				StartCoroutine("RevStressedTired");
			}
			else
			{
				currMood = PlayerMood.TN;
				StartCoroutine("RevNeutralTired");
			}
		}
		else
		{
			if(newHappiness >= 3)
			{
				currMood = PlayerMood.NH;
				StartCoroutine("RevHappyNeutral");
			}
			else if(newHappiness <= -3)
			{
				currMood = PlayerMood.NS;
				StartCoroutine("RevStressedNeutral");
			}
			else
			{
				currMood = PlayerMood.NN;
				StartCoroutine("RevNeutralNeutral");
			}
		}
	}

	void NeutralNeutral()
	{	Mood.SetTrigger("Move 0-0");	}
	void HappyNeutral()
	{	Mood.SetTrigger("Move H-0");	}
	void StressedNeutral()
	{	Mood.SetTrigger("Move S-0");	}
	void NeutralTired()
	{	Mood.SetTrigger("Move 0-T");	}
	void NeutralEnergized()
	{	Mood.SetTrigger("Move 0-E");	}
	void HappyTired()
	{	Mood.SetTrigger("Move H-T");	}
	void HappyEnergized()
	{	Mood.SetTrigger("Move H-E");	}
	void StressedTired()
	{	Mood.SetTrigger("Move S-T");	}
	void StressedEnergized()
	{	Mood.SetTrigger("Move S-E");	}

	IEnumerator RevNeutralNeutral()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev 0-0");
	}
	IEnumerator RevHappyNeutral()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev H-0");
	}
	IEnumerator RevStressedNeutral()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev S-0");
	}
	IEnumerator RevNeutralTired()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev 0-T");
	}
	IEnumerator RevNeutralEnergized()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev 0-E");
	}
	IEnumerator RevHappyTired()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev H-T");
	}
	IEnumerator RevHappyEnergized()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev H-E");
	}
	IEnumerator RevStressedTired()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev S-T");
	}
	IEnumerator RevStressedEnergized()
	{
		yield return new WaitForSeconds(.5f);
		Mood.SetTrigger("Rev S-E");
	}

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
