using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip audioCorrectSlide;
	public AudioClip audioGameOver;
	public AudioClip audioSwipe;

	AudioSource aSource;
	public bool toggleBGMusicPlayback;

	void Start()
	{
		aSource = this.GetComponent<AudioSource> ();
		aSource.playOnAwake = true;
		aSource.loop = true;
		toggleBGMusicPlayback = true;
	}

	void Update()
	{

	}

	public void ToggleBGMusicPlayback()
	{
		if (toggleBGMusicPlayback)
			aSource.audio.Pause ();
		else
			aSource.audio.Play ();

		toggleBGMusicPlayback = !toggleBGMusicPlayback;
	}

	//TODO: Add lisence info for http://www.freesfx.co.uk
}
