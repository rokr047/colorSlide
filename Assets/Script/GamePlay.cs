using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {
	
	public void DeleteColorCube(GameObject _ColorCube)
	{
		Destroy(_ColorCube);
	}
	
	public void CreateColorCube()
	{
		GameObject colorCube = (GameObject)Instantiate(Resources.Load("Prefab/respawn"));
		colorCube.renderer.material.color = Color.red;
	}

	public void Start()
	{
		CreateColorCube ();
	}
}
