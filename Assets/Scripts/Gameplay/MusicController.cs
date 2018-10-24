using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
	[SerializeField] private  List<AudioClip> music;
	[SerializeField] private List<AudioClip> musicList;
	private AudioSource audioSource;

	public static MusicController instance;

	int current = 0;

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

	void Start ()
	{
		print("Start");
		//StartupMusic();
		if (!audioSource.isPlaying) audioSource.Play();
		UpdateLabel();
	}

	void OnEnabled()
	{
		print("OnEnable");
	}
	
	void Update ()
	{
		
	}

	void StartupMusic()
	{
		int x = music.Count;
		for (int i = 0; i < x; i++)
		{
			int j = Random.Range(0,music.Count);
			musicList.Add(music[j]);
			music.Remove(music[j]);
		}

		PlayNext();
	}

	void PlayNext()
	{
		audioSource.clip = musicList[current];
		print(audioSource.clip.name);
		UpdateLabel();
		if (current == musicList.Count) current = 0;
		else current++;

		Invoke("PlayNext", audioSource.clip.length);
	}

	void UpdateLabel()
	{
		GameObject controller = GameObject.Find("Controller");
		controller.GetComponent<GameController>().UpdateMusic(audioSource.clip.name);
	}
}
