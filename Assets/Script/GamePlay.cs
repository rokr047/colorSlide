using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {

	GameObject[] goPushButton;

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

		colorCube.GetComponent<SpriteRenderer>().color = goPushButton [Random.Range (0, 3)].GetComponent<SpriteRenderer>().color;
	}

	void Start()
	{
		goPushButton = GameObject.FindGameObjectsWithTag ("PushButton");

		if (goPushButton.Length <= 0) {
			Debug.LogError ("Error! There are no pushbuttons available!");
			return;
		}
	}
}
