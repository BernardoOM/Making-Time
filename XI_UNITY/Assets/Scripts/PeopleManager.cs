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

	public AudioClip neutral_music;
	public AudioClip stress_music;
	public AudioClip tired_music;

	private bool cur_egy_bgm = false;
	private bool cur_hpy_bgm = false;
	private bool cur_ntl_bgm = true;
	private int cur_bgm=0;

	//To be changed later
	void Start()
	{
		population = new Dictionary<string, Person>();
		nameList = new List<string>();

		player = new Player(RelationType.Player, MoodType.Neutral, "Jena Balton", 4, 3);
		population.Add(player.name, player);
		nameList.Add(player.name);
	}

	public void ChangePlayerStatus(int deltaEnergy, int deltaHappiness)
	{
		if(OnChangeStatus != null)
		{	OnChangeStatus(deltaEnergy, deltaHappiness);
			//Debug.Log (OnChangeStatus);
		}
		//GameObject.Find("SideBar").GetComponent<Sidebar>().ChangeValues(player.energy, player.happiness);
	}


	//switch background music when two values changed 
	public void switch_bgm(){
		int current_egy =  player.energy;
		int current_hpy = player.happiness;
		Debug.Log (current_egy);
		Debug.Log (current_hpy);

		if (current_egy <= -3  ) 
		if(cur_bgm!=1) {
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().clip = tired_music;
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().Play ();
			cur_bgm = 1;		}
		
		if (current_hpy  <= -3 && current_egy  > -3)
		if(cur_bgm!=2) {
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().clip = stress_music;
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().Play ();
			cur_bgm = 2;

		}

		if (current_egy > -3 && current_hpy  > -3)
		if(cur_bgm!=0) {
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().clip = tired_music;
			GameObject.Find ("Main Camera").GetComponent<AudioSource> ().Play ();
			cur_bgm = 0;	}

	}
}
