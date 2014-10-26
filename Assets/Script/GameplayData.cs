﻿using UnityEngine;
using System.Collections;

public class GameplayData : MonoBehaviour {

	public float colorChangeTimer;
	public float cubeMoveSpeed;

	public enum GameState
	{
		GAME_RUNNING,
		GAME_PAUSED,
		GAME_STARTING,
		GAME_READY,
		GAME_OVER,
		GAME_RESTARTING,
		GAME_RESTARTED
	};

	[HideInInspector] public GameState currentGameState;
	[HideInInspector] public int score = 0;
	[HideInInspector] public int finalScore = 0;

	void Start()
	{
		currentGameState = GameState.GAME_RUNNING;
	}

}