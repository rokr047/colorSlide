using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private GameplayData gPlayData;
	private bool isMoving;
	//AudioManager aManager;
	
	enum Direction
	{
		LEFT,
		RIGHT,
		UP,
		DOWN
	};
	
	public PlayerController()
	{
		
	}
	
	void Start()
	{
		gPlayData = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameplayData>();
		//aManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<AudioManager> ();
	}
	
	void Update()
	{
		if(gPlayData.currentGameState == GameplayData.GameState.GAME_RUNNING)
		{
			#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
			GetInputFromKeyboard();
			GetInputFromTouch();
			#else
			GetInputFromTouch();
			#endif
		}
	}
	
	private void GetInputFromKeyboard()
	{
		if (isMoving) 
		{
			return;
		}

		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			MoveColorCube(Direction.DOWN);
			isMoving = true;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			MoveColorCube(Direction.UP);
			isMoving = true;
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
		{
			MoveColorCube(Direction.LEFT);
			isMoving = true;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
		{
			MoveColorCube(Direction.RIGHT);
			isMoving = true;
		}
	}
	
	private void GetInputFromTouch()
	{
		if (isMoving) 
		{
			return;
		}

		if(SwipeManager.swipeDirection == SwipeManager.Swipe.Down)
		{
			MoveColorCube(Direction.DOWN);
			isMoving = true;
		}
		else if(SwipeManager.swipeDirection == SwipeManager.Swipe.Up)
		{
			MoveColorCube(Direction.UP);
			isMoving = true;
		}
		else if(SwipeManager.swipeDirection == SwipeManager.Swipe.Left)
		{
			MoveColorCube(Direction.LEFT);
			isMoving = true;
		}
		else if(SwipeManager.swipeDirection == SwipeManager.Swipe.Right)
		{
			MoveColorCube(Direction.RIGHT);
			isMoving = true;
		}
		
		SwipeManager.swipeDirection = SwipeManager.Swipe.None;
	}
	
	private void MoveColorCube(Direction d)
	{
		if (d == Direction.RIGHT) 
		{
			this.rigidbody2D.AddForce (new Vector2(gPlayData.cubeMoveSpeed, 0.0f),ForceMode2D.Impulse);
		} 
		else if (d == Direction.LEFT) 
		{
			this.rigidbody2D.AddForce (new Vector2(-gPlayData.cubeMoveSpeed, 0.0f),ForceMode2D.Impulse);
		} 
		else if (d == Direction.UP) 
		{
			this.rigidbody2D.AddForce (new Vector2(0.0f, gPlayData.cubeMoveSpeed),ForceMode2D.Impulse);
		} 
		else if (d == Direction.DOWN) 
		{
			this.rigidbody2D.AddForce (new Vector2(0.0f, -gPlayData.cubeMoveSpeed),ForceMode2D.Impulse);
		}
		/*
		if (false && aManager.toggleBGMusicPlayback) {
			aManager.audio.volume = 0.5f;
			aManager.audio.PlayOneShot (aManager.audioSwipe);
		}
		*/
	}
}
