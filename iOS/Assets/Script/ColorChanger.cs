using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	GameplayData gPlayData;

	void Start () 
	{
		gPlayData = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameplayData> ();
	}

	void Update () 
	{
		if (gPlayData.currentGameState == GameplayData.GameState.GAME_RUNNING) {

		}
	}
}
