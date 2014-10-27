using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static bool created;
	public GameObject ScoreText;
	public GameObject TimeText;

	GameplayData gPlay;

	void Awake()
	{
		if (!created) 
		{
			DontDestroyOnLoad (transform.gameObject);
			created = true;
		} 
		else 
		{
			Destroy(this.gameObject);
		}
	}

	void Start()
	{
		gPlay = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameplayData> ();
		GameSetup ();
	}

	void Update()
	{
		if(gPlay.currentGameState == GameplayData.GameState.GAME_RUNNING)
		{
			ScoreText.GetComponent<Text> ().text = this.GetComponent<GameplayData>().score.ToString();
			TimeText.GetComponent<Text>().text = this.GetComponent<GameplayData>().totalGameTime.ToString("F1");
		}
	}

	public void GameOver()
	{
		gPlay.currentGameState = GameplayData.GameState.GAME_OVER;
		ScoreText.GetComponent<Text> ().text = "GAME OVER";
	}

	public void GameSetup()
	{
		ScoreText.GetComponent<Text> ().text = "COLOR SLIDE";
		gPlay.currentGameState = GameplayData.GameState.GAME_READY;
		TimeText.SetActive (false);
	}

	public void StartGame()
	{
		TimeText.SetActive (true);
		gPlay.currentGameState = GameplayData.GameState.GAME_RUNNING;
	}
}
