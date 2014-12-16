using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

/* This will be singleton class for accessing iOS Game Center
 * leader board and achievements 
 * https://gist.github.com/NiklasBorglund/c33af4ab377ea289283f
 */
public class GameCenterIntegration {

	#region Singleton variables and functions
	private static GameCenterIntegration instance;
	
	public static GameCenterIntegration Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new GameCenterIntegration();
			}
			return instance;
		}
	}
	#endregion

	public GameCenterIntegration (){}

	public void Initialize()
	{
		if(!IsUserAuthenticated())
		{
			Social.localUser.Authenticate (ProcessAuthentication);
		}
	}

	public bool IsUserAuthenticated()
	{
		if(Social.localUser.authenticated)
		{
			return true;
		}
		else
		{
			Debug.Log("User not Authenticated");
			return false;
		}
	}

	public void ShowLeaderboardUI()
	{
		if (IsUserAuthenticated ()) {
				Social.ShowLeaderboardUI ();
		} else {
			MobileNativeDialog dialog = new MobileNativeDialog("Leaderboard Unavailable", "Click YES to sign in to GameCenter and enable leaderboard.");
			dialog.OnComplete += OnDialogClose;
		}
	}

	private void OnDialogClose(MNDialogResult result) {
		//parsing result
		switch(result) {
		case MNDialogResult.YES:
			Debug.Log ("Yes button pressed");
			Social.localUser.Authenticate (ProcessAuthentication);
			break;
		case MNDialogResult.NO:
			Debug.Log ("No button pressed");
			break;
			
		}
	}

	public void ReportScore(long score, string leaderBoardID)
	{
		if (IsUserAuthenticated())
		{
			Debug.Log("Reporting score " + score + " on leaderboard " + leaderBoardID);
			Social.ReportScore(score, leaderBoardID, success => {
				Debug.Log(success ? "Reported score successfully" : "Failed to report score");
			});
		}
	}

	void ProcessAuthentication(bool success)
	{
		if(success)
		{
			Debug.Log ("Authenticated, checking achievements");

		}
		else
		{
			Debug.Log ("Failed to authenticate");
			//new MobileNativeMessage("Authentication Failed!", "There was a problem with GameCenter authentication. Please close and reopen game");
		}
	}

	public void LoadScore(string LeaderboardID)
	{
		Social.LoadScores(LeaderboardID, scores => {
			if (scores.Length > 0) {
				Debug.Log ("Got " + scores.Length + " scores");
				string myScores = "Leaderboard:\n";
				foreach (IScore score in scores)
					myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
				Debug.Log (myScores);
			}
			else
				Debug.Log ("No scores loaded");
		});
	}
}
