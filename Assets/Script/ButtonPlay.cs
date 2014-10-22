using UnityEngine;
using System.Collections;

public class ButtonPlay : MonoBehaviour 
{
	GamePlay gPlay;
	GameplayData gPlayData;

	void Start()
	{
		gPlay = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GamePlay> ();
		gPlayData = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameplayData> ();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.CompareTag ("Respawn")) 
		{
			gPlay.DeleteColorCube(c.gameObject);
			gPlayData.score++;
			gPlay.CreateColorCube();
		}
	}
}
