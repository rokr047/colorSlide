using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {

	GameObject[] goPushButton;
	public float changeColorTimeInSeconds;

	public void DeleteColorCube(GameObject _ColorCube)
	{
		if (_ColorCube == null) {

			Debug.LogError ("Error! Cannot destroy color Cube!");
			return;
		}

		Destroy(_ColorCube);
	}

	public void CreateColorCube()
	{
		GameObject colorCube = (GameObject)Instantiate(Resources.Load("Prefab/respawn"));

		if (colorCube == null) {
			Debug.LogError ("Error! Cannot Instantiate color Cube!");
			return;
		}

		colorCube.GetComponent<SpriteRenderer>().color = goPushButton [Random.Range (0, 4)].GetComponent<SpriteRenderer>().color;
	}

	void Start()
	{
		goPushButton = GameObject.FindGameObjectsWithTag ("PushButton");

		if (goPushButton.Length <= 0) {
			Debug.LogError ("Error! There are no pushbuttons available!");
			return;
		}

		Invoke ("CallChangeWallColor", 1.5f);
	}

	public void CallChangeWallColor()
	{
		StartCoroutine ("ChangeWallColor");
	}
	
	public void StopChangeWallColor()
	{
		StopCoroutine ("ChangeWallColor");
	}

	IEnumerator ChangeWallColor()
	{
		while (true) 
		{
			yield return new WaitForSeconds (Random.Range(5,20));
			
			Color tempColor = goPushButton[0].GetComponent<SpriteRenderer>().color;
			if(GameObject.FindGameObjectWithTag("Respawn") != null && GameObject.FindGameObjectWithTag("Respawn").rigidbody2D.velocity.sqrMagnitude <= 0.0f)
			{
				goPushButton[0].GetComponent<SpriteRenderer>().color = goPushButton[1].GetComponent<SpriteRenderer>().color;
				goPushButton[1].GetComponent<SpriteRenderer>().color = goPushButton[2].GetComponent<SpriteRenderer>().color;
				goPushButton[2].GetComponent<SpriteRenderer>().color = goPushButton[3].GetComponent<SpriteRenderer>().color;
				goPushButton[3].GetComponent<SpriteRenderer>().color = tempColor;
			}
		}
	}
}
