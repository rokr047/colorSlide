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
	public GameObject TotalCorrectSlidesText;
	public GameObject TotalGameTimeText;
	public GameObject TotalIncorrectSlideText;
	public GameObject PlayButton, ReplayButton;
	public GameObject BackButton;
	public GameObject TotalHighScoreText, TotalAverageScoreText;

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
		//gDataManager.DisplayPlayerData ();

		StatsButton.SetActive (true);

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

		TotalCorrectSlidesText.SetActive (false);
		TotalGameTimeText.SetActive (false);
		TotalIncorrectSlideText.SetActive (false);
		BackButton.SetActive (false);
		TotalHighScoreText.SetActive(false);
		TotalAverageScoreText.SetActive(false);
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
		StatsButton.SetActive (false);
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

	public void EnableStats(bool t)
	{
		TotalCorrectSlidesText.SetActive (t);
		TotalGameTimeText.SetActive (t);
		TotalIncorrectSlideText.SetActive (t);
		TotalHighScoreText.SetActive(t);
		TotalAverageScoreText.SetActive(t);

		TotalCorrectSlidesText.GetComponent<Text> ().text = "Correct Slides : " + PlayerPrefs.GetInt ("totalCorrectSlides");
		TotalGameTimeText.GetComponent<Text> ().text = "Game Time : " + PlayerPrefs.GetFloat ("totalGameTime").ToString("F2");
		TotalIncorrectSlideText.GetComponent<Text> ().text = "Incorrect Slides : " + PlayerPrefs.GetInt ("totalIncorrectSlides");
		TotalHighScoreText.GetComponent<Text>().text = "High Score : " + (PlayerPrefs.GetInt("highScore") / PlayerPrefs.GetFloat("highScoreTime")).ToString("F1");
		TotalAverageScoreText.GetComponent<Text> ().text = "Average Score : " + (PlayerPrefs.GetInt ("totalCorrectSlides") / PlayerPrefs.GetFloat ("totalGameTime")).ToString ("F1");

		StatsButton.SetActive (!t);
		PlayButton.SetActive (!t);
		BackButton.SetActive (t);

		ReplayButton.SetActive (false);
		HighScoreText.SetActive(false);
		CurrentScoreText.SetActive (false);

		ScoreText.GetComponent<Text> ().text = t ? "STATS" : "COLOR SLIDE";
	}
}
