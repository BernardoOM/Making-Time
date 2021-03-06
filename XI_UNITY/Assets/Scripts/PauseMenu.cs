﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject MuteButton;
	public GameObject UnMuteButton;
	public GameObject PlayButton;
	public GameObject FastForwardButton;

	void Start(){
		//if fastforward button was clicked, show normal speed play button on pause window, vice versa 
		if (GameManager.Calendar.fastforward == 1) {
			FastForwardButton.SetActive (true);
			PlayButton.SetActive(false);
		}
		else{
			PlayButton.SetActive(true);
			FastForwardButton.SetActive (false);
		}
	}

	public void Mute()
	{
		GameObject.Find ("Main Camera").GetComponent<AudioManager> ().click_play ();

		MuteButton.SetActive(true);
		UnMuteButton.SetActive(false);
        GameObject.Find("SoundText").GetComponent<Text>().text = "Sound: Off";
        //mute current background. it is in main camera audio source
        GameObject.Find ("Main Camera").GetComponent<AudioSource> ().mute = true;
	}

	public void UnMute()
	{
		GameObject.Find ("Main Camera").GetComponent<AudioManager> ().click_play ();

		MuteButton.SetActive(false);
		UnMuteButton.SetActive(true);
        GameObject.Find("SoundText").GetComponent<Text>().text = "Sound: On";
        GameObject.Find ("Main Camera").GetComponent<AudioSource> ().mute = false;
	}

	public void Play()
	{
		GameObject.Find ("Main Camera").GetComponent<AudioManager> ().click_play ();

		FastForwardButton.SetActive(true);
		PlayButton.SetActive(false);
        GameObject.Find("FastTimeText").GetComponent<Text>().text = "Fast Forward: Off";
        GameManager.Calendar.fastforward = 1;
	}

	public void FastForward()
	{
		GameObject.Find ("Main Camera").GetComponent<AudioManager> ().click_play ();

		FastForwardButton.SetActive(false);
		PlayButton.SetActive(true);
        GameObject.Find("FastTimeText").GetComponent<Text>().text = "Fast Forward: On";
        GameManager.Calendar.fastforward = 5;
	}

	public void Close()
	{
		GameObject.Find ("Main Camera").GetComponent<AudioManager> ().click_play ();

		GameManager.Calendar.WindowEvent();
		GameManager.Instance.StartGame();
		gameObject.SetActive (false);
		//Destroy(gameObject);
	}
}
