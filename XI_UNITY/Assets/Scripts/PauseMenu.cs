using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	public GameObject MuteButton;
	public GameObject UnMuteButton;
	public GameObject PlayButton;
	public GameObject FastForwardButton;

	public void Mute()
	{
		MuteButton.SetActive(true);
		UnMuteButton.SetActive(false);
	}

	public void UnMute()
	{
		MuteButton.SetActive(false);
		UnMuteButton.SetActive(true);
	}

	public void Play()
	{
		FastForwardButton.SetActive(true);
		PlayButton.SetActive(false);
	}

	public void FastForward()
	{
		FastForwardButton.SetActive(false);
		PlayButton.SetActive(true);
	}

	public void Close()
	{
		GameManager.Instance.StartGame();
		Destroy(gameObject);
	}
}
