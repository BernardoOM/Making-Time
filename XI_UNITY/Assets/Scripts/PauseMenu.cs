using UnityEngine;
using System.Collections;

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
		MuteButton.SetActive(true);
		UnMuteButton.SetActive(false);
		//mute current background. it is in main camera audio source 
		GameObject.Find ("Main Camera").GetComponent<AudioSource> ().mute = true;
	}

	public void UnMute()
	{
		MuteButton.SetActive(false);
		UnMuteButton.SetActive(true);
		GameObject.Find ("Main Camera").GetComponent<AudioSource> ().mute = false;

	}

	public void Play()
	{
		FastForwardButton.SetActive(true);
		PlayButton.SetActive(false);
		GameManager.Calendar.fastforward = 1;
	}

	public void FastForward()
	{
		FastForwardButton.SetActive(false);
		PlayButton.SetActive(true);
		GameManager.Calendar.fastforward = 5;
	}

	public void Close()
	{
		GameManager.Instance.StartGame();
		Destroy(gameObject);
	}
}
