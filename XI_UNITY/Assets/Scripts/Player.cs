﻿using UnityEngine;
using System.Collections;

public class Player : Person
{
	public int energy { get; private set; }
	public int happiness { get; private set; }

	public Player(RelationType theirRelation, MoodType theirMood,
	              string aName, int theirAffection, int theirTrust)
		   : base(theirRelation, theirMood, aName, theirAffection, theirTrust)
	{
		energy = 0;
		happiness = 0;

		GameManager.People.OnChangeStatus += People_OnChangeStatus;
	}

	void People_OnChangeStatus (int deltaEnergy, int deltaHappiness)
	{
		energy += deltaEnergy;
		Mathf.Clamp(energy, -10, 10);
		happiness += deltaHappiness;
		Mathf.Clamp(happiness, -10, 10);

		GameObject.Find("SideBar").GetComponent<Sidebar>().ChangeMood(deltaEnergy,deltaHappiness, energy, happiness);
	}
}
