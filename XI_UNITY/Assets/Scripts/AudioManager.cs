using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public  AudioClip[] pickup;
	public  AudioClip[] putdown;
	public  AudioClip[] bubble;

	public  AudioSource sfx;
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
}
