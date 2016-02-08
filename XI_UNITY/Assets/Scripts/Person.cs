using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RelationType {	Player, Family, Work, Friend	};
public enum MoodType { Exstatic, Happy, Pleased, Neutral, Dissapointed, Pissed, Furious };

public class Person
{
	private List<int>	acquaintances;

	public RelationType	relation { get; private set; }
	public MoodType		curMood { get; private set; }
	public MoodType		prevMood { get; private set; }
	public string		name { get; private set; }
	public int			playerAffection { get; private set; }
	public int			playerTrust { get; private set; }

	//Constructor
	public Person(RelationType theirRelation, MoodType theirMood,
	              string aName, int theirAffection, int theirTrust)
	{
		acquaintances = new List<int>();

		relation = theirRelation;			curMood = theirMood;		name = aName;
		playerAffection = theirAffection;	playerTrust = theirTrust;
	}

	public void AddAcquaintance(int newAcquaintance)
	{	acquaintances.Add(newAcquaintance);	}
}
