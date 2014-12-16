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
	public GameObject MusicButton;
	public GameObject CurrentScoreLabel, HighScoreLabel;
	public GameObject TwitterButton, FacebookButton;

	GameplayData gPlayData;
	GamePlay gPlay;
	GameDataManager gDataManager;
	AudioManager aManager;
	AdManager adManager;

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
		aManager = this.GetComponent<AudioManager> ();
		adManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AdManager> ();
		GameSetup ();
	}

	void Update()
	{
		if(gPlayData.currentGameState == GameplayData.GameState.GAME_RUNNING)
		{
			ScoreText.GetComponent<Text> ().text = this.GetComponent<GameplayData>().score.ToString();
			TimeText.GetComponent<Text>().text = this.GetComponent<GameplayData>().totalGameTime.ToString("F1");

		/* Revert to old code for iOS build update.
			ScoreText.GetComponent<Text> ().text = "COLOR ARCADE"; //this.GetComponent<GameplayData>().score.ToString();
			//TimeText.GetComponent<Text>().text = this.GetComponent<GameplayData>().totalGameTime.ToString("F1");

			TimeText.GetComponent<Text>().text = Mathf.FloorToInt(((this.GetComponent<GameplayData>().score) * 1 * (this.GetComponent<GameplayData>().score / this.GetComponent<GameplayData>().totalGameTime))).ToString();
		*/
		}
	}

	public void GameOver()
	{
		gPlayData.currentGameState = GameplayData.GameState.GAME_OVER;
		ScoreText.GetComponent<Text> ().text = "GAME OVER";
		RestartButton.SetActive (true);
		TimeText.SetActive (false);
		gPlayData.incorrectScore++;

		gDataManager.SavePlayerStats ();
		//gDataManager.DisplayPlayerData ();

		StatsButton.SetActive (true);
		MusicButton.SetActive (true);

		CurrentScoreLabel.SetActive (true);
		HighScoreLabel.SetActive (true);

		/*
		TwitterButton.SetActive (true);
		FacebookButton.SetActive (true);
		*/

		#region display score
		CurrentScoreText.SetActive (true);
		HighScoreText.SetActive (true);

		int score = Mathf.FloorToInt(((gPlayData.score + gPlayData.totalGameTime) * 1 * (gPlayData.score / gPlayData.totalGameTime)));
		//int score = Mathf.FloorToInt(((gPlayData.score) * 1 * (gPlayData.score / gPlayData.totalGameTime)));
		CurrentScoreText.GetComponent<Text>().text = score.ToString();

		if(score > GetHighScore())
			ScoreText.GetComponent<Text> ().text = "HIGH SCORE";

		SaveScore (score);

		HighScoreText.GetComponent<Text>().text = GetHighScore().ToString();
		#endregion

		#region GameCenter Report Score
		#if UNITY_IPHONE
		//Add iOS Gamecenter service leaderboard integration.
		GameCenterIntegration.Instance.ReportScore(score ,"lb_ColorArcade_01");
		#elif UNITY_ANDROID
		//Add Android Game Service leaderboard integration.
		GPGSIntegration.Instance.ReportScore(score,"CgkI47Ot-4EQEAIQBg");
		#else
		Debug.LogError("GameOver() :: Leaderboard Not Supported in Current Platform!!");
		#endif
		#endregion

		adManager.ShowInterstitialAfter--;
		adManager.ShowInterstitialAfterGameOver ();
	}

	public void GameSetup()
	{
		ScoreText.GetComponent<Text> ().text = "COLOR ARCADE";
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
		CurrentScoreLabel.SetActive (false);
		HighScoreLabel.SetActive (false);
		TwitterButton.SetActive (false);
		FacebookButton.SetActive (false);

		#if UNITY_IPHONE
			//iOS Gamecenter Initialize.
			GameCenterIntegration.Instance.Initialize ();
		#elif UNITY_ANDROID
			//Google Play Games Initialize.
			GPGSIntegration.Instance.Initialize();
		#else
		Debug.LogError("GameSetup() :: Unsupported Platform!");
		#endif
	}

	public void StartGame()
	{
		TimeText.SetActive (true);
		gPlayData.currentGameState = GameplayData.GameState.GAME_RUNNING;
		StartButton.SetActive (false);
		gPlay.CreateColorCube ();

		HighScoreText.SetActive (false);
		CurrentScoreText.SetActive (false);

		CurrentScoreLabel.SetActive (false);
		HighScoreLabel.SetActive (false);

		StatsButton.SetActive (false);
		MusicButton.SetActive (false);

		TwitterButton.SetActive (false);
		FacebookButton.SetActive (false);
	}

	public void RestartGame()
	{
		RestartButton.SetActive (false);
		gPlayData.finalScore = gPlayData.score = gPlayData.incorrectScore = 0;
		gPlayData.totalGameTime = 0f;
		StatsButton.SetActive (false);
		StartGame ();
	}

	public void SaveScore(int score)
	{
		if (PlayerPrefs.HasKey ("highScore")) {

			if(PlayerPrefs.GetInt("highScore") < score)
			{
				PlayerPrefs.SetInt("highScore", score);
			}
		} else {
			PlayerPrefs.SetInt("highScore" , score);
		}
	}

	public int GetHighScore()
	{
		if (PlayerPrefs.HasKey ("highScore")) {
			return PlayerPrefs.GetInt ("highScore");
		} else {
			return 0;
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

	public void ToggleMusicPlayback()
	{
		aManager.ToggleBGMusicPlayback ();

		if (aManager.toggleBGMusicPlayback) {
			MusicButton.GetComponent<Button> ().image.color = Color.green;
			ScoreText.GetComponent<Text> ().text = "MUSIC ON";
		} else {
			MusicButton.GetComponent<Button> ().image.color = Color.red;
			ScoreText.GetComponent<Text> ().text = "MUSIC OFF";
		}
	}

	public void ShowLeaderBoard()
	{
		ScoreText.GetComponent<Text> ().text = "LEADERBOARD";
		#if UNITY_IPHONE
			GameCenterIntegration.Instance.LoadScore ("lb_ColorArcade_01");
			GameCenterIntegration.Instance.ShowLeaderboardUI ();
		#elif UNITY_ANDROID
			GPGSIntegration.Instance.LoadScore("CgkI47Ot-4EQEAIQBg");
			GPGSIntegration.Instance.ShowLeaderboardUI();
		#else
		Debug.LogError("ShowLeaderBoard() :: Unsupported Platform");
		#endif
	}

	#region Twitter Share
	private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
	private const string TWEET_LANGUAGE = "en"; 

	public void SubmitScoreToTwitter()
	{
		Debug.Log ("Submitting score to twitter.");
		string textToDisplay = "Test : Just scored " + CurrentScoreText.GetComponent<Text>().text + "in #ColorAracde! Try to beat that!" + "[link to app store]";

		ShareToTwitter (textToDisplay);
	}
	
	private void ShareToTwitter (string textToDisplay)
	{
		Application.OpenURL(TWITTER_ADDRESS +
		                    "?text=" + WWW.EscapeURL(textToDisplay) +
		                    "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
	}
	#endregion
}
