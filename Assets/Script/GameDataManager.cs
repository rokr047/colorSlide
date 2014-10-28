using UnityEngine;
using System.Collections;

public class GameDataManager : MonoBehaviour {

	GameplayData gPlayData;

	void Start()
	{
		gPlayData = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameplayData> ();
	}

	public void SavePlayerStats()
	{
		SaveTotalGameTimeStat ();
		SaveTotalCorrectSlides ();
		SaveTotalIncorrectSlides ();
	}

	private void SaveTotalGameTimeStat()
	{
		if (PlayerPrefs.HasKey ("totalGameTime")) {

			PlayerPrefs.SetFloat("totalGameTime", gPlayData.totalGameTime + PlayerPrefs.GetFloat("totalGameTime"));
		} else {
			PlayerPrefs.SetFloat("totalGameTime" , gPlayData.totalGameTime);
		}
	}

	private void SaveTotalCorrectSlides()
	{
		if (PlayerPrefs.HasKey ("totalCorrectSlides")) {
			
			PlayerPrefs.SetInt("totalCorrectSlides", gPlayData.score + PlayerPrefs.GetInt("totalCorrectSlides"));
		} else {
			PlayerPrefs.SetInt("totalCorrectSlides" , gPlayData.score);
		}
	}

	private void SaveTotalIncorrectSlides()
	{
		if (PlayerPrefs.HasKey ("totalIncorrectSlides")) {
			
			PlayerPrefs.SetInt("totalIncorrectSlides", gPlayData.incorrectScore + PlayerPrefs.GetInt("totalIncorrectSlides"));
		} else {
			PlayerPrefs.SetInt("totalIncorrectSlides" , gPlayData.incorrectScore);
		}
	}

	public void DisplayPlayerData()
	{
		Debug.Log ("totalGameTime : " + PlayerPrefs.GetFloat("totalGameTime").ToString("F2"));
		Debug.Log ("totalCorrectSlides : " + PlayerPrefs.GetInt("totalCorrectSlides").ToString());
		Debug.Log ("totalIncorrectSlides : " + PlayerPrefs.GetInt("totalIncorrectSlides").ToString());
	}
}