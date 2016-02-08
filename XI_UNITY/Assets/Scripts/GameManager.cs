﻿using UnityEngine;
using System.Collections;

public enum GameState {	MainMenu, Calendar, Event, Status, Pause	};

//public delegate void OnStateChange();

public class GameManager : MonoBehaviour
{
	// Singletons
	private static GameManager		instance;
	private static CalendarManager	calendar;
	private static PeopleManager	people;

	// Constructor
	private GameManager() {}

//	public event OnStateChange	stateChanger;
	public GameState			curState {	get; private set; }

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

	void Awake()
	{
		if(instance != null)
		{	DestroyImmediate(gameObject);	}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		curState = GameState.Calendar;

		StartGame ();
	}

	void StartGame()
	{

	}
}