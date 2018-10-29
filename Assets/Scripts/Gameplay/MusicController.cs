using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MusicController : MonoBehaviour
{
	[SerializeField] private  List<AudioClip> music;
	[SerializeField] private List<AudioClip> musicList;
	[SerializeField] private AudioSource audioSource;

	//public static MusicController instance;

	int current = 0;

	/* 
	void Awake()
    {
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			
		}
		else if(instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
		StartupMusic();
		//UpdateLabel();
		print("Awake");
		SceneManager.sceneLoaded += (a, b) => UpdateLabel();
    }
	*/

	void Start ()
	{
		print("Start");
		//audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
		//StartupMusic();
		//if (!audioSource.isPlaying) audioSource.Play();
		StartupMusic();
		UpdateLabel();
	}
	
	void Update ()
	{
		
	}

	public void Stop()
	{
		//audioSource.Stop();
		audioSource.DOFade(0,.5f);
	}

	void StartupMusic()
	{
		/*
		int x = music.Count;
		for (int i = 0; i < x; i++)
		{
			int j = Random.Range(0,music.Count);
			musicList.Add(music[j]);
			music.Remove(music[j]);
		}
		*/
		PlayNext();
	}

	void PlayNext()
	{
		audioSource.clip = music[Random.Range(0, music.Count)];
		audioSource.Play();
		//audioSource.PlayOneShot(musicList[Random.Range(0, musicList.Count)]);
		print(audioSource.clip.name);
		UpdateLabel();
		//if (current == musicList.Count) current = 0;
		//else current++;

		Invoke("PlayNext", audioSource.clip.length);
	}

	void UpdateLabel()
	{
		GameObject controller = GameObject.Find("Controller");
		controller.GetComponent<GameController>().UpdateMusic(audioSource.clip.name);
	}
}
