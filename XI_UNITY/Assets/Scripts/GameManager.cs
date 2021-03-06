﻿using UnityEngine;
using System.Collections;

public enum GameState {	MainMenu, Calendar, Tutorial, Status, Pause	};

//public delegate void OnStateChange();

public class GameManager : MonoBehaviour
{
	// Singletons
	private static GameManager		instance;
	private static CalendarManager	calendar;
	private static PeopleManager	people;
	private static UIManager		ui;

	// Constructor
	private GameManager() {}

//	public event OnStateChange	stateChanger;
	public  GameState curState {	get; private set; }
	private GameState prevState = GameState.Tutorial;

	public static GameManager Instance
	{
		get
		{
			if(instance == null)
			{	instance = new GameObject("Managers").AddComponent<GameManager>();	}

			return instance;
		}
	}

	public static CalendarManager Calendar
	{
		get
		{
			if(calendar == null)
			{	calendar = Instance.GetComponent<CalendarManager>();	}

			return calendar;
		}
	}

	public static PeopleManager People
	{
		get
		{
			if(people == null)
			{	people = Instance.GetComponent<PeopleManager>();	}

			return people;
		}
	}

	public static UIManager UI
	{
		get
		{
			if(ui == null)
			{	ui = Instance.GetComponent<UIManager>();	}
			return ui;
		}
	}

	void Awake()
	{
		if(instance != null)
		{	DestroyImmediate(gameObject);	}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		curState = GameState.Tutorial;
	}

	public void BeginGame()
	{	instance.curState = GameState.Calendar;	}

	public void StartGame()
	{	instance.curState = prevState;	}

	public void PauseGame()
	{
		if (curState != GameState.Pause) {
			instance.prevState = curState;
			instance.curState = GameState.Pause;
		}
	//call this function when  you need to pause outside of this script.
	}
}