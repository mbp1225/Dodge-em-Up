using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject startupScreen;
	[SerializeField] private GameObject menuScreen;
	[SerializeField] private Transform LoadingScreen;
	[SerializeField] private Transform quitScreen;
	[SerializeField] private CanvasGroup darkOverlay;
	[SerializeField] private Transform bg1;
	[SerializeField] private Transform bg2;

	[SerializeField] private float startupTransitionTime;

	[SerializeField] private Ease easeType;

	//[SerializeField] private TextMeshProUGUI loadingText;

	void Start ()
	{
		//DOTween.Init();
		//DOTween.defaultEaseType = Ease.InOutQuint;
		//loadingText.text = "Projeto de Produto";
		//DOTween.To(()=> loadingText.text, x=> loadingText.text = x, "Projeto de Processo", 2f);
		//StartCoroutine(loadGame());
	}
	
	void Update ()
	{
		
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

	public void QuitGame()
	{
		Application.Quit();
	}
}
