using UnityEngine;
using System.Collections;

public class SceneSetup : MonoBehaviour 
{
	public Camera mainCam;
	public BoxCollider2D topWall, leftWall, rightWall, bottomWall;
	public Transform leftButton, rightButton, topButton, bottomButton;

	public float offset_h = 75f;
	public float offset_v = 75f;

	void Start () {
		//move each wall to its edge position
		bottomWall.size = topWall.size = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (Screen.width * 2f, 0f, 0f)).x, 1f);
		topWall.center = new Vector2 (0f, mainCam.ScreenToWorldPoint (new Vector3 (0f, Screen.height, 0f)).y + 0.5f);
		bottomWall.center = new Vector2 (0f, mainCam.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).y - 0.5f);

		leftWall.size = rightWall.size = new Vector2 (1f, mainCam.ScreenToWorldPoint (new Vector3 (0f, Screen.width * 2f, 0f)).y);
		leftWall.center = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).x - 0.5f, 0f);
		rightWall.center = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f)).x + 0.5f, 0f);

		leftButton.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(offset_h, 0f, 0f)).x, 0f, 0f);
		rightButton.position = new Vector3(mainCam.ScreenToWorldPoint(new Vector3(Screen.width - offset_h, 0f, 0f)).x, 0f, 0f);
		leftButton.localScale = rightButton.localScale = new Vector3(leftButton.localScale.x, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height * 0.775f, 0f)).y, leftButton.localScale.z);
		
		topButton.position = new Vector3(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height - offset_v, 0f)).y);
		bottomButton.position = new Vector3(0f, mainCam.ScreenToWorldPoint(new Vector3(0f, 0f + offset_v, 0f)).y);
		topButton.localScale = bottomButton.localScale = new Vector3(topButton.localScale.x, mainCam.ScreenToWorldPoint(new Vector3(Screen.width * 1f, 0f, 0f)).x, topButton.localScale.z);
	}
}
