using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static bool created;
	public GameObject ScoreText;

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

	void Update()
	{
		ScoreText.GetComponent<Text> ().text = this.GetComponent<GameplayData>().score.ToString();
	}
}
