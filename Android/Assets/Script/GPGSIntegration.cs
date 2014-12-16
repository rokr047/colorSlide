using UnityEngine;
using System;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

/* This will be singleton class for accessing iOS Game Center
 * leader board and achievements.
 */
public class GPGSIntegration {

	#region Singleton variables and functions
	private static GPGSIntegration instance;
	
	public static GPGSIntegration Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new GPGSIntegration();
			}
			return instance;
		}
	}
	#endregion

	public GPGSIntegration (){}

	public void Initialize()
	{
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

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

	void ProcessAuthentication(bool success)
	{
		if(success)
		{
			Debug.Log ("Authenticated, checking achievements");
			
		}
		else
		{
			Debug.Log ("Failed to authenticate");
		}
	}

	void UnlockAchievement(string achievementID)
	{
		if(!IsUserAuthenticated())
		{
			Debug.Log("User not authenticated!");
			return;
		}

		Social.ReportProgress(achievementID, 100.0f, (bool success) => {
			if(success)
			{
				Debug.Log("Reporting Achievement progress success!");
			} else {
				Debug.Log("Reporting Achievement progress failed!");
			}
		});
	}

	public void ShowLeaderboardUI()
	{
		if(IsUserAuthenticated())
		{
			Social.ShowLeaderboardUI();
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

	public void LoadScore(string LeaderboardID)
	{
		if (!IsUserAuthenticated ())
				return;

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
