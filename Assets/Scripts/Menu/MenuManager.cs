using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private Image initialOverlay;
	[SerializeField] private CanvasGroup gdp;

	[SerializeField] private GameObject startupScreen;
	[SerializeField] private GameObject menuScreen;
	[SerializeField] private Transform LoadingScreen;
	[SerializeField] private Transform ConfigurationScreen;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider sfxSlider;
	[SerializeField] private Transform CreditsScreen;
	[SerializeField] private Transform quitScreen;
	[SerializeField] private CanvasGroup darkOverlay;
	[SerializeField] private Transform bg1;
	[SerializeField] private Transform bg2;

	[SerializeField] private float startupTransitionTime;

	[SerializeField] private Ease easeType;

	[SerializeField] private AudioSource music;
	[SerializeField] private AudioSource sound;

	//[SerializeField] private TextMeshProUGUI loadingText;

	void Start ()
	{
		//DOTween.Init();
		//DOTween.defaultEaseType = Ease.InOutQuint;
		//loadingText.text = "Projeto de Produto";
		//DOTween.To(()=> loadingText.text, x=> loadingText.text = x, "Projeto de Processo", 2f);
		//StartCoroutine(loadGame());

		if (!PlayerPrefs.HasKey("musicVolume")) PlayerPrefs.SetFloat("musicVolume", 1.0f);
		if (!PlayerPrefs.HasKey("sfxVolume")) PlayerPrefs.SetFloat("sfxVolume", 1.0f);

		//music = GetComponent<AudioSource>();
		music.volume = PlayerPrefs.GetFloat("musicVolume");
		sound.volume = PlayerPrefs.GetFloat("sfxVolume");

		if (PlayGamesScript.firstStart) StartCoroutine(Initialize());
		else Quickstart();

	}
	
	void Update ()
	{
		
	}

	IEnumerator Initialize()
	{
		PlayGamesScript.firstStart = false;
		initialOverlay.DOFade(0, .25f);
		yield return new WaitForSeconds(2f);
		Destroy(initialOverlay.gameObject);
		gdp.DOFade(0,.25f);
		yield return new WaitForSeconds(.25f);
		Destroy(gdp.gameObject);
	}

	void Quickstart()
	{
		LoadingScreen.DOLocalMoveX(0, 0f);
		Destroy(initialOverlay.gameObject);
		Destroy(gdp.gameObject);
		LoadingScreen.DOLocalMoveX(-1400f, .25f).SetEase(easeType);
	}

	public void GoToMenu()
	{
		startupScreen.transform.DOLocalMoveX(-1080f, startupTransitionTime).SetEase(easeType);
		menuScreen.transform.DOLocalMoveX(0f, startupTransitionTime).SetEase(easeType);
		bg1.transform.DOLocalMoveX(-420f, startupTransitionTime).SetEase(easeType);
		bg2.transform.DOLocalMoveX(-310f, startupTransitionTime).SetEase(easeType);
	}

	public void StartGame()
	{
		LoadingScreen.DOLocalMoveX(1400f, 0f);
		StartCoroutine(loadGame());
	}

	IEnumerator loadGame()
	{
		LoadingScreen.DOLocalMoveX(0, .25f).SetEase(easeType);
		
		yield return new WaitForSeconds(.35f);
		
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
		
		while (!asyncLoad.isDone)
		{
			/*
			loadingText.text = "Loading";
			DOTween.To(()=> loadingText.text, x=> loadingText.text = x, "Loading.", .25f);
			yield return new WaitForSeconds(.25f);
			DOTween.To(()=> loadingText.text, x=> loadingText.text = x, "Loading..", .25f);
			yield return new WaitForSeconds(.25f);
			DOTween.To(()=> loadingText.text, x=> loadingText.text = x, "Loading...", .25f);
			yield return new WaitForSeconds(1f);
			*/
			yield return null;
		}
	}

	public void ToggleConfirmQuit(bool on)
	{
		if (on)
		{
			darkOverlay.blocksRaycasts = true;
			DOTween.To(()=> darkOverlay.alpha, x=> darkOverlay.alpha = x, 1, .5f);
			quitScreen.DOLocalMoveY(-873f, .5f).SetEase(easeType);

		}
		else
		{
			darkOverlay.blocksRaycasts = false;
			DOTween.To(()=> darkOverlay.alpha, x=> darkOverlay.alpha = x, 0, .5f);
			quitScreen.DOLocalMoveY(-1250f, .5f).SetEase(easeType);
		}
	}

	public void ToggleConfiguration(bool on)
	{
		if (on)
		{
			musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
			sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
			darkOverlay.blocksRaycasts = true;
			DOTween.To(()=> darkOverlay.alpha, x=> darkOverlay.alpha = x, 1, .5f);
			ConfigurationScreen.DOLocalMoveX(0, .5f).SetEase(easeType);

		}
		else
		{
			PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
			PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
			//music.volume = PlayerPrefs.GetFloat("musicVolume");
			DOTween.To(()=> music.volume, x=> music.volume = x, PlayerPrefs.GetFloat("musicVolume"), .25f);
			DOTween.To(()=> sound.volume, x=> sound.volume = x, PlayerPrefs.GetFloat("sfxVolume"), .25f);
			darkOverlay.blocksRaycasts = false;
			DOTween.To(()=> darkOverlay.alpha, x=> darkOverlay.alpha = x, 0, .5f);
			ConfigurationScreen.DOLocalMoveX(-1080f, .5f).SetEase(easeType);
		}
	}

	public void ToggleCredits(bool on)
	{
		if (on)
		{
			darkOverlay.blocksRaycasts = true;
			DOTween.To(()=> darkOverlay.alpha, x=> darkOverlay.alpha = x, 1, .5f);
			CreditsScreen.DOLocalMoveX(0, .5f).SetEase(easeType);

		}
		else
		{
			darkOverlay.blocksRaycasts = false;
			DOTween.To(()=> darkOverlay.alpha, x=> darkOverlay.alpha = x, 0, .5f);
			CreditsScreen.DOLocalMoveX(-1080f, .5f).SetEase(easeType);
		}
	}

	public void ShowLeaderboards()
	{
		PlayGamesScript.ShowLeaderboardsUI();
		print("Show Leaderboards");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void ButtonPress()
	{
		//Handheld.Vibrate();
		Vibration.Vibrate(25);
		//if (Application.platform != RuntimePlatform.WindowsPlayer) Vibration.Vibrate(25);
		sound.Play();
	}
}
