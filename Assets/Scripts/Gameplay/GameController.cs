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
		loadingScreen.DOLocalMoveX(-1400f, .35f);
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

		yield return new WaitForSeconds(startDelay);
		
		while(player)
		{
			Instantiate(waves[Random.Range(0, waves.Count)], transform.up * 8, Quaternion.identity);
			yield return new WaitForSeconds(waveDelay);
		}

		restartButton.SetActive(true);
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
	}

	public void ReloadScene()
	{
		print("Reloading Scene");
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}
}
