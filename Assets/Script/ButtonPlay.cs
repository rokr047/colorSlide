using UnityEngine;
using System.Collections;

public class ButtonPlay : MonoBehaviour 
{
	GamePlay gPlay;
	GameplayData gPlayData;
	GameManager gManager;

	void Start()
	{
		gPlay = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GamePlay> ();
		gPlayData = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameplayData> ();
		gManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.CompareTag ("Respawn")) {
			if (c.gameObject.GetComponent<SpriteRenderer> ().color == this.gameObject.GetComponent<SpriteRenderer> ().color) {

				gPlay.DeleteColorCube (c.gameObject);
				gPlayData.score++; 
				gPlay.CreateColorCube ();
			} else {

				//Possible alternate Game Mode based on total time and negative scores.
				gPlay.DeleteColorCube (c.gameObject);
				gPlayData.finalScore = gPlayData.score;
				gManager.GameOver();
			}
		}
	}
}
