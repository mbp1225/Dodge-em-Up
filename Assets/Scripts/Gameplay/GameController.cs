using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
	[SerializeField] List<GameObject> waves;
	[SerializeField] float waveDelay;
	[SerializeField] private float startDelay;

	[SerializeField] TextMeshProUGUI timer;

	[SerializeField] private Transform loadingScreen;
	[SerializeField] private Transform tutorialScreen;
	[SerializeField] private Transform pauseScreen;
	[SerializeField] private TextMeshProUGUI musicLabel;

	[SerializeField] private Transform timerBar;
	[SerializeField] private Transform hullBar;

	[SerializeField] GameObject restartButton;

	[SerializeField] TextMeshProUGUI finalScore;
	[SerializeField] TextMeshProUGUI highScore;
	[SerializeField] GameObject newLabel;

	bool isPlaying = false;

	float startTime = 0;
	float currentTime = 0;

	Transform player;

	void Start ()
	{
		loadingScreen.DOLocalMoveX(-1400f, .35f).SetEase(Ease.OutExpo);
		player = GameObject.FindGameObjectWithTag("Player").transform;
		StartCoroutine(SpawnWaves());
	}
	
	void Update ()
	{
		if (isPlaying)
		{
			currentTime = Time.time - startTime;
			timer.text = string.Format(currentTime.ToString("#.00") + "s");

		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(1.25f);
		loadingScreen.DOLocalMoveX(1400f, 0f);
		tutorialScreen.DOLocalMoveX(-1080f, .4f).SetEase(Ease.OutExpo);
		yield return new WaitForSeconds(.5f);
		timerBar.DOLocalMoveY(960, .3f).SetEase(Ease.OutExpo);
		hullBar.DOMoveY(0, .3f).SetEase(Ease.OutExpo);


		yield return new WaitForSeconds(startDelay);
		isPlaying = true;
		startTime = Time.time;

		/*
		foreach (GameObject wave in waves)
		{
			if (!player) break;
			Instantiate(wave, transform.up * 8, Quaternion.identity);
			yield return new WaitForSeconds(waveDelay);
		}
		*/

		
		
		while(player)
		{
			Instantiate(waves[Random.Range(0, waves.Count)], transform.up * 8, Quaternion.identity);
			yield return new WaitForSeconds(waveDelay);
		}

		restartButton.transform.DOLocalMoveX(0, .25f).SetEase(Ease.OutExpo);
		EndScreen();

		isPlaying = false;
	}

	void EndScreen()
	{
		finalScore.text = string.Format(currentTime.ToString("#.00") + "s");

		if (currentTime > PlayerPrefs.GetFloat("highscore"))
		{
			PlayerPrefs.SetFloat("highscore", currentTime);
			highScore.text = string.Format(currentTime.ToString("#.00") + "s");
			newLabel.SetActive(true);
		}
		else
		{
			highScore.text = string.Format(PlayerPrefs.GetFloat("highscore").ToString("#.00") + "s");
		}

		PlayGamesScript.AddScoreToLeaderboard(GPGSIds.leaderboard_highscores, (long)currentTime);
		PlayGamesScript.ShowLeaderboardsUI();
	}

	public void ReloadScene()
	{
		print("Restarting...");
		Time.timeScale = 1;
		StartCoroutine(Restart());
	}

	public void Pause()
	{
		pauseScreen.transform.DOLocalMoveX(0, .25f).SetEase(Ease.OutExpo).SetUpdate(UpdateType.Late, true);
		DOTween.To(()=> Time.timeScale, x=> Time.timeScale = x, 0, .25f).SetUpdate(UpdateType.Late, true);
	}

	public void Unpause()
	{
		pauseScreen.transform.DOLocalMoveX(1200, .25f).SetEase(Ease.OutExpo).SetUpdate(UpdateType.Late, true);
		DOTween.To(()=> Time.timeScale, x=> Time.timeScale = x, 1, .25f).SetUpdate(UpdateType.Late, true);
	}

	public void UpdateMusic(string musicName)
	{
		musicLabel.text = "Now Playing: " + musicName;
	}

	IEnumerator Restart()
	{
		loadingScreen.DOLocalMoveX(0f, .35f).SetEase(Ease.OutExpo);
		yield return new WaitForSeconds(.4f);
		print("Reloading Scene");
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
}
