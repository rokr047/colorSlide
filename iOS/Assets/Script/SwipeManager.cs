using UnityEngine;
using System.Collections;

public class SwipeManager : MonoBehaviour 
{
	public enum Swipe
	{
		None,
		Up,
		Down,
		Left,
		Right
	};

	public float minSwipeLength = 100.0f;
	public float scaleValue;

	//float SPEEDMULTIPLIER = 10.0f;

	Vector2 swipeStartPos;
	Vector2 swipeEndPos;
	Vector2 swipeVector;

	GameObject respawnObj;

	public static Swipe swipeDirection;

	private bool userTouched;

	GameplayData gPlayData;

	void Start () 
	{
		swipeDirection = Swipe.None;
		userTouched = false;
		gPlayData = this.GetComponent<GameplayData> ();
	}

	void Update () 
	{
		if (gPlayData.currentGameState == GameplayData.GameState.GAME_RUNNING) {
			#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
			DetectSwipeFromMouse ();
			#else
			DetectSwipeFromTouch();
			#endif
		}
	}

	void DetectSwipeFromTouch()
	{
		if (Input.touches.Length > 0) 
		{
			Touch t = Input.GetTouch (0);

			if (t.phase == TouchPhase.Began && !userTouched) 
			{
				Debug.Log("Scaling forward.");

				userTouched = true;
				swipeStartPos = new Vector2 (t.position.x, t.position.y);

				if((respawnObj = GameObject.FindGameObjectWithTag("Respawn")) != null)
					respawnObj.transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("Respawn").transform.localScale.x * scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.y * scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.z * scaleValue);
			}

			if(t.phase == TouchPhase.Ended && userTouched)
			{
				Debug.Log("Scaling backward.");

				userTouched = false;
				swipeEndPos = new Vector2(t.position.x,t.position.y);

				if((respawnObj = GameObject.FindGameObjectWithTag("Respawn")) != null)
					respawnObj.transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("Respawn").transform.localScale.x / scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.y / scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.z / scaleValue);

				ComputeSwipeDirection();
			}
		} 
		else 
		{
			swipeDirection = Swipe.None;
		}
	}

	void DetectSwipeFromMouse()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			swipeStartPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

			if((respawnObj = GameObject.FindGameObjectWithTag("Respawn")) != null)
				respawnObj.transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("Respawn").transform.localScale.x * scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.y * scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.z * scaleValue);
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			swipeEndPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

			if((respawnObj = GameObject.FindGameObjectWithTag("Respawn")) != null)
				respawnObj.transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("Respawn").transform.localScale.x / scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.y / scaleValue, GameObject.FindGameObjectWithTag("Respawn").transform.localScale.z / scaleValue);

			ComputeSwipeDirection();
		}
	}

	void ComputeSwipeDirection()
	{
		swipeVector = swipeEndPos - swipeStartPos;
		
		if(swipeVector.sqrMagnitude < minSwipeLength * minSwipeLength)
		{
			//We will not consider this as swipe.
			swipeDirection = Swipe.None;
			return;
		}

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		//Do not change the value of cube movespeed.
		#else
		//gManager.cubeMoveSpeed = swipeVector.magnitude * SPEEDMULTIPLIER;
		//gManager.cubeMoveSpeed = SPEEDMULTIPLIER;
		#endif

		swipeVector.Normalize();

		if (swipeVector.y > 0 &&  swipeVector.x > -0.5f && swipeVector.x < 0.5f) 
		{
			swipeDirection = Swipe.Up;
		} 
		else if (swipeVector.y < 0 && swipeVector.x > -0.5f && swipeVector.x < 0.5f) 
		{
			swipeDirection = Swipe.Down;
		} 
		else if (swipeVector.x < 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f) 
		{
			swipeDirection = Swipe.Left;
		} 
		else if (swipeVector.x > 0 && swipeVector.y > -0.5f && swipeVector.y < 0.5f) 
		{
			swipeDirection = Swipe.Right;
		}
	}
}
