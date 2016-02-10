using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PeopleManager : MonoBehaviour
{
	public delegate void		EmotionHandler(int value0, int value1);
	public event EmotionHandler	OnChangeStatus;

	private Player						player;
	private Dictionary<string, Person>	population;
	private List<string>				nameList;

	//To be changed later
	void Start()
	{
		population = new Dictionary<string, Person>();
		nameList = new List<string>();

		player = new Player(RelationType.Player, MoodType.Neutral, "Jena Star", 4, 3);
		population.Add(player.name, player);
		nameList.Add(player.name);
	}

	public void ChangePlayerStatus(int deltaEnergy, int deltaHappiness)
	{
		if(OnChangeStatus != null)
		{	OnChangeStatus(deltaEnergy, deltaHappiness);	}
		GameObject.Find("SideBar").GetComponent<Sidebar>().ChangeValues(player.energy, player.happiness);
	}
}
