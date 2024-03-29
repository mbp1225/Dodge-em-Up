﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private int maxHp;
	[SerializeField] private int currentHp;

	bool canTakeDamage = true;

	[SerializeField] GameObject[] healthBars;
	[SerializeField] private float speed;
	[SerializeField] private float maxHorizontalDistance;
	[SerializeField] private Camera cam;
	

	void Start ()
	{
		currentHp = maxHp;
		//transform.position = new Vector3(0, -cam.orthographicSize, 0);
	}
	
	void Update ()
	{
		if (Input.GetAxis("Horizontal") < -0.1f) Move("left");
		if (Input.GetAxis("Horizontal") > 0.1f) Move("right");
	}

	public void Move(string dir)
	{
		if (dir == "left" && transform.position.x > -maxHorizontalDistance)
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (dir == "right" && transform.position.x < maxHorizontalDistance)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
	}

	public void TakeDamage()
	{
		if (canTakeDamage)
		{
			canTakeDamage = false;
			currentHp--;
			StartCoroutine(InvincibilityFrames(.25f));

			for (int i = currentHp; i < maxHp; i++)
			{
				healthBars[i].SetActive(false);
			}

			cam.DOShakePosition(.1f, .4f, 2, 90, false);

			if (currentHp <= 0) Die();
		}
	}

	IEnumerator  InvincibilityFrames(float duration)
	{
		yield return new WaitForSeconds(duration);
		canTakeDamage = true;
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
