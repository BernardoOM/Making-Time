using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public  AudioClip[] pickup;
	public  AudioClip[] putdown;
	public  AudioClip[] bubble;
	public AudioClip neg_mood_sfx;
	public AudioClip pos_mood_sfx;
	public AudioClip neu_mood_sfx;
	public AudioClip click;
	public  AudioSource sfx;
	public AudioClip event_start;

	public AudioSource office;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public  void pickup_play(){
		//sfx.clip= pickup;
		int i=Random.Range(0,pickup.Length-1);
		sfx.clip= pickup[i];
		sfx.Play ();
	}

	public  void putdown_play(){
		//sfx.clip = putdown;
		int i=Random.Range(0,putdown.Length-1);
		sfx.clip= putdown[i];
		sfx.Play ();
	}

	public  void bubble_play(){
		//sfx.clip = bubble;
		int i=Random.Range(0,bubble.Length-1);
		sfx.clip= bubble[i];
		sfx.Play ();
	}

	public  void neg_sfx_play(){
		sfx.clip= neg_mood_sfx;
		//sfx.Play ();
	}

	public  void pos_sfx_play(){
		sfx.clip= pos_mood_sfx;
		//sfx.Play ();
	}

	public  void neu_sfx_play(){
		sfx.clip= neu_mood_sfx;
		//sfx.Play ();
	}

	public  void office_sfx_play(){
		office.Play ();
	}
	public  void office_sfx_stop(){
		office.Stop ();
	}
	public  void click_play(){
		//sfx.clip= pickup;
		sfx.clip= click;
		sfx.Play ();
	}

	public  void event_play(){
		//sfx.clip= pickup;
		sfx.clip= event_start;
		sfx.Play ();
	}
}
