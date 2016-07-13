using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour 
{
	static MusicPlayer instance = null;
	
	public AudioClip StartSong;
	public AudioClip GameSong;
	public AudioClip EndSong;
	
	private AudioSource music;
	
	void Start () 
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			
			music.clip = StartSong;
			music.loop = true;
			music.Play();
		}
	}
	
	void OnLevelWasLoaded(int Level)
	{
		music.Stop();
		if(Level == 0) music.clip = StartSong;
		if(Level == 1) music.clip = GameSong;
		if(Level == 2) music.clip = EndSong;
		
		music.loop = true;
		music.Play();
	}
}
