using UnityEngine;
using System.Collections;

public class ButtonPlay : MonoBehaviour 
{
	GamePlay gPlay;
	GameplayData gPlayData;
	GameManager gManager;
	AudioManager aManager;

	void Start()
	{
		gPlay = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GamePlay> ();
		gPlayData = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameplayData> ();
		gManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		aManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<AudioManager> ();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.CompareTag ("Respawn")) {
			if (c.gameObject.GetComponent<SpriteRenderer> ().color == this.gameObject.GetComponent<SpriteRenderer> ().color) {

				//aManager.audio.PlayOneShot (aManager.audioCorrectSlide);
				gPlay.DeleteColorCube (c.gameObject);
				gPlayData.score++; 
				gPlay.CreateColorCube ();
			} else {

				//Possible alternate Game Mode based on total time and negative scores.
				gPlay.DeleteColorCube (c.gameObject);
				gPlayData.finalScore = gPlayData.score;
				gManager.GameOver();

				#region Audio GameOver Music
				if(aManager.toggleBGMusicPlayback) {
					aManager.audio.volume = 1.0f;
					aManager.audio.PlayOneShot (aManager.audioGameOver); 
				}
				#endregion
			}
		}
	}
}
