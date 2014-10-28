using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static bool created;
	public GameObject ScoreText;
	public GameObject TimeText;
	public GameObject StartButton;
	public GameObject RestartButton;
	public GameObject HighScoreText;
	public GameObject CurrentScoreText;
	public GameObject StatsButton;

	GameplayData gPlayData;
	GamePlay gPlay;
	GameDataManager gDataManager;

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
		gPlayData = this.GetComponent<GameplayData> ();
		gPlay = this.GetComponent<GamePlay> ();
		gDataManager = this.GetComponent<GameDataManager> ();
		GameSetup ();
	}

	void Update()
	{
		if(gPlayData.currentGameState == GameplayData.GameState.GAME_RUNNING)
		{
			ScoreText.GetComponent<Text> ().text = this.GetComponent<GameplayData>().score.ToString();
			TimeText.GetComponent<Text>().text = this.GetComponent<GameplayData>().totalGameTime.ToString("F1");
		}
	}

	public void GameOver()
	{
		gPlayData.currentGameState = GameplayData.GameState.GAME_OVER;
		ScoreText.GetComponent<Text> ().text = "GAME OVER";
		RestartButton.SetActive (true);
		TimeText.SetActive (false);
		gPlayData.incorrectScore++;

		SaveScore ();
		gDataManager.SavePlayerStats ();
		gDataManager.DisplayPlayerData ();

		#region display score
		CurrentScoreText.SetActive (true);

		if(PlayerPrefs.HasKey("highScore") && PlayerPrefs.HasKey("highScoreTime"))
		{
			HighScoreText.SetActive (true);
			HighScoreText.GetComponent<Text>().text = "High : " + (PlayerPrefs.GetInt("highScore") / PlayerPrefs.GetFloat("highScoreTime")).ToString("F1");
		}

		CurrentScoreText.GetComponent<Text>().text = "Score : " + (gPlayData.score / gPlayData.totalGameTime).ToString("F4");
		#endregion
	}

	public void GameSetup()
	{
		ScoreText.GetComponent<Text> ().text = "COLOR SLIDE";
		gPlayData.currentGameState = GameplayData.GameState.GAME_READY;
		TimeText.SetActive (false);
		RestartButton.SetActive (false);
		HighScoreText.SetActive (false);
		CurrentScoreText.SetActive (false);
	}

	public void StartGame()
	{
		TimeText.SetActive (true);
		gPlayData.currentGameState = GameplayData.GameState.GAME_RUNNING;
		StartButton.SetActive (false);
		gPlay.CreateColorCube ();

		HighScoreText.SetActive (false);
		CurrentScoreText.SetActive (false);

		StatsButton.SetActive (false);
	}

	public void RestartGame()
	{
		RestartButton.SetActive (false);
		gPlayData.finalScore = gPlayData.score = gPlayData.incorrectScore = 0;
		gPlayData.totalGameTime = 0f;
		StartGame ();
	}

	public void SaveScore()
	{
		if (PlayerPrefs.HasKey ("highScore")) {

			if(PlayerPrefs.GetInt("highScore") < gPlayData.score)
			{
				PlayerPrefs.SetInt("highScore", gPlayData.score);
			}
		} else {
			PlayerPrefs.SetInt("highScore" , gPlayData.score);
		}

		if (PlayerPrefs.HasKey ("highScoreTime")) {
			
			if(PlayerPrefs.GetFloat("highScoreTime") < gPlayData.totalGameTime)
			{
				PlayerPrefs.SetFloat("highScoreTime", gPlayData.totalGameTime);
			}
		} else {
			PlayerPrefs.SetFloat("highScoreTime" , gPlayData.totalGameTime);
		}
	}
}
